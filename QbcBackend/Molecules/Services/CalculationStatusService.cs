using Microsoft.Extensions.Configuration;
using QbcBackend.Molecules.Model.CalculationStatus;
using QbcBackend.Molecules.Model.GmsInput;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Model.MoleculeModel;
using QbcBackend.Molecules.Model.Parser;
using QbcBackend.Molecules.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class CalculationStatusService : ICalculationStatusService
    {

        #region private properties

        public IParserService Parser
        {
            get;
        }

        public IChemicalModelService ModelService
        {
            get;
        }

        public IMoleculeService MoleculeService
        {
            get;
        }


        private string GmsInputFileDirectory
        {
            get;
            set;
        }


        private string GmsOutputfileDirectory
        {
            get;
            set;
        }


        #endregion

        public CalculationStatusService(IParserService parser,
                                            IConfiguration configuration,
                                             IChemicalModelService modelService, IMoleculeService moleculeService)
        {
            this.Parser = parser;
            this.ModelService = modelService;
            this.MoleculeService = moleculeService;


            var statusConfig = configuration.GetSection("calculationstatus");
            GmsInputFileDirectory = statusConfig["gmsinput"];
            GmsOutputfileDirectory = statusConfig["gmsoutput"];
        }

        public async Task<CalculationStatusTransferResult> TransferCalculation(ExecutionStatus statusBefore, ChemicalCalculation calculationAfter)
        {
            CalculationStatusTransferResult retval = new CalculationStatusTransferResult();
            retval.StatusAfterTransfer = calculationAfter.Status;

            ChemicalModel model = await this.ModelService.Get(calculationAfter.ModelID);
            model.Molecules = new List<MoleculeInfo>();
            model.Molecules.AddRange( await this.MoleculeService.GetByModelID(calculationAfter.ModelID));

           // ChemicalCalculation optimizationCalc = model.Calculations.FirstOrDefault(i => i.Type == CalculationType.Optimization && i.BasisSet.Id == calculationAfter.BasisSet.Id);

            if ( statusBefore == ExecutionStatus.Created)
            {
               if( calculationAfter.Status == ExecutionStatus.Ready )
               {
                    try
                    {
                        if ( calculationAfter.Type == CalculationType.Fukui)
                        {
                            File.AppendAllText(Path.Combine(GmsInputFileDirectory, $"{calculationAfter.Code}_{calculationAfter.Id}_{FukuiInputType.neutral}.inp".Replace(" ", "_")),
                                                CalcFukuiGmsInput(FukuiInputType.neutral,calculationAfter.GmsInput));
                            File.AppendAllText(Path.Combine(GmsInputFileDirectory, $"{calculationAfter.Code}_{calculationAfter.Id}_{FukuiInputType.lewisbase}.inp".Replace(" ", "_")),
                                                CalcFukuiGmsInput(FukuiInputType.lewisbase, calculationAfter.GmsInput));
                            File.AppendAllText(Path.Combine(GmsInputFileDirectory, $"{calculationAfter.Code}_{calculationAfter.Id}_{FukuiInputType.lewisacid}.inp".Replace(" ", "_")),
                                                CalcFukuiGmsInput(FukuiInputType.lewisacid, calculationAfter.GmsInput));
                        }
                        else
                        {
                            File.AppendAllText(Path.Combine(GmsInputFileDirectory, $"{calculationAfter.Code}_{calculationAfter.Id}.inp".Replace(" ", "_")), calculationAfter.GmsInput);
                        }
                        
                    }
                    catch(Exception e)
                    {
                        retval.ErrorMessage = e.Message;
                        retval.StatusAfterTransfer = ExecutionStatus.Error;
                    }
               }
            }

            if ( statusBefore == ExecutionStatus.Ready)
            {
                if ( calculationAfter.Status == ExecutionStatus.Executed)
                {
                    try
                    {
                        if (calculationAfter.Type == CalculationType.Fukui)
                        {
                            string neutralcontent = string.Empty;
                            string acidcontent = string.Empty;
                            string basecontent = string.Empty;

                            foreach (var f in Directory.EnumerateFiles(this.GmsOutputfileDirectory, $"*{calculationAfter.Name}_{calculationAfter.Id}*.log"))
                            {
                                if ( f.Contains($"{FukuiInputType.neutral}"))
                                {
                                    neutralcontent = File.ReadAllText(f);
                                }
                                else if ( f.Contains($"{FukuiInputType.lewisacid}"))
                                {
                                    acidcontent = File.ReadAllText(f);
                                }
                                else if (f.Contains($"{FukuiInputType.lewisbase}"))
                                {
                                    basecontent = File.ReadAllText(f);
                                }
                            }

                            ParseResult parseresult = null;
                            MoleculeInfo existingMolecule = null;
                            var result = this.Parser.ParseGmsInputForFukui(calculationAfter.GmsInput);
                            existingMolecule = result.Molecule;

                            if (existingMolecule != null)
                            {
                                parseresult = await this.Parser.ParseFukuiAsync(neutralcontent, basecontent, acidcontent, existingMolecule);
                            }
                            else
                            {
                                parseresult.Error = true;
                            }

                            if (parseresult.Error)
                            {
                                retval.StatusAfterTransfer = ExecutionStatus.Error;
                                retval.ErrorMessage = "Unable to parse calculation";
                            }
                            else
                            {
                                parseresult.Molecule.Charge = model.Charge;
                                parseresult.Molecule.ModelId = calculationAfter.ModelID;
                                parseresult.Molecule.ParentCalculationId = calculationAfter.Id;
                                retval.Molecules.Add(parseresult.Molecule);
                            }
                        }
                        else
                        {
                            foreach (var f in Directory.EnumerateFiles(this.GmsOutputfileDirectory, $"*{calculationAfter.Name}_{calculationAfter.Id}*.log"))
                            {
                                ParseResult parseresult = null;
                                if (calculationAfter.Type == CalculationType.Optimization)
                                {
                                    var toparse = File.ReadAllText(f);
                                    parseresult = await this.Parser.ParseOptimizationAsync(toparse);
                                }
                                else if (calculationAfter.Type == CalculationType.CHelpGCharges
                                            || calculationAfter.Type == CalculationType .GeoDiskCharges)
                                {
                                    var toparse = File.ReadAllText(f);
                                    MoleculeInfo existingMolecule = null;
                                    var result = this.Parser.ParseGmsInput(calculationAfter.GmsInput);
                                    existingMolecule = result.Molecule;

                                    if ( existingMolecule != null)
                                    {
                                        parseresult = await this.Parser.ParseChargeAsync(toparse, existingMolecule);
                                        parseresult.Error = false;
                                    }
                                    else
                                    {
                                        parseresult.Error = true;
                                    }                           
                                }
                                else
                                {
                                    parseresult.Error = true;
                                }

                                if (parseresult.Error)
                                {
                                    retval.StatusAfterTransfer = ExecutionStatus.Error;
                                    retval.ErrorMessage = "Unable to parse calculation";
                                }
                                else
                                {
                                    parseresult.Molecule.Charge = model.Charge;
                                    parseresult.Molecule.ModelId = calculationAfter.ModelID;
                                    parseresult.Molecule.ParentCalculationId = calculationAfter.Id;
                                    retval.Molecules.Add(parseresult.Molecule);
                                }
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        retval.ErrorMessage = e.Message;
                        retval.StatusAfterTransfer = ExecutionStatus.Error;
                    }
                }
            }
            return await Task.FromResult(retval);
        }


        #region private helpers



        private string CalcFukuiGmsInput(FukuiInputType type, string gmsinput)
        {
            string retval = string.Empty;

            foreach(var item in gmsinput.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var result = item.Replace("(", "").Replace(")", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if ( result[0] == type.ToString())
                {
                    retval = result[1];
                    break;
                }
            }
            return retval;
        }



        #endregion


    }
}
