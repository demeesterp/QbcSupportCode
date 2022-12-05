using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.GmsInput;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.StringConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{

    public class GmsInputService : IGmsInputService
    {

        #region services


        private IAtomInfoRepository AtomInfoRepo { get; }


        private IBasisSetRepository BasisSetRepo { get;  }


        #endregion

        public GmsInputService(IAtomInfoRepository atomInfoRepo, 
                                        IBasisSetRepository basisSetRepo)
        {
            this.AtomInfoRepo = atomInfoRepo;
            this.BasisSetRepo = basisSetRepo;
        }

        public async Task<string> GenCHelpGChargeInput(string geometryxyz, int charge, int basisSetId)
        {
            StringBuilder retval = new StringBuilder();

            var atomInfoList = await this.AtomInfoRepo.GetAllAsync();
            var basisSet = await this.BasisSetRepo.GetByIdAsync(basisSetId);


            retval.AppendLine($" {basisSet.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=CHELPG CONSTR=CHARGE $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            AtomInfo current = null;
            foreach(var atom in CalculateInitialGeometry(geometryxyz))
            {
                current = atomInfoList.ToList().Find((i) => i.Symbol == atom.Symbol);
                retval.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        public async Task<string> GenGeoDiskChargeInput(string geometryxyz, int charge, int basisSetId)
        {
            StringBuilder retval = new StringBuilder();

            var atomInfoList = await this.AtomInfoRepo.GetAllAsync();
            var basisSet = await this.BasisSetRepo.GetByIdAsync(basisSetId);


            retval.AppendLine($" {basisSet.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=GEODESIC CONSTR=CHARGE $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            AtomInfo current = null;
            foreach (var atom in CalculateInitialGeometry(geometryxyz))
            {
                current = atomInfoList.ToList().Find((i) => i.Symbol == atom.Symbol);
                retval.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }


        public async Task<List<FukuiInputInfo>> GenFukuiInput(string geometryxyz, int charge, int basisSetId)
        {

            List<FukuiInputInfo> retval = new List<FukuiInputInfo>();

            var atomInfoList = await this.AtomInfoRepo.GetAllAsync();
            var basisSet = await this.BasisSetRepo.GetByIdAsync(basisSetId);

            // Neutral
            StringBuilder input = new StringBuilder();
            input.AppendLine($" {basisSet.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=RHF MAXIT=60 MULT=1 ICHARG={charge} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            AtomInfo current = null;
            foreach (var atom in CalculateInitialGeometry(geometryxyz))
            {
                current = atomInfoList.ToList().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            input.AppendLine(" $END");


            retval.Add(new FukuiInputInfo()
            {
                GmsInput = input.ToString(),
                Type = FukuiInputType.neutral
            });

            // Lewis Base
            input.Clear();
            input.AppendLine($" {basisSet.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={charge + 1} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            current = null;
            foreach (var atom in CalculateInitialGeometry(geometryxyz))
            {
                current = atomInfoList.ToList().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            input.AppendLine(" $END");

            retval.Add(new FukuiInputInfo()
            {
                GmsInput = input.ToString(),
                Type = FukuiInputType.lewisbase
            });

            // Lewis Acid
            input.Clear();
            input.AppendLine($" {basisSet.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={charge - 1} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            current = null;
            foreach (var atom in CalculateInitialGeometry(geometryxyz))
            {
                current = atomInfoList.ToList().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            input.AppendLine(" $END");

            retval.Add(new FukuiInputInfo()
            {
                GmsInput = input.ToString(),
                Type = FukuiInputType.lewisacid
            });
            return retval;
        }

        public async Task<string> GenOptInput(string geometryxyz, int charge, int basisSetId)
        {
            var atomInfoList = await this.AtomInfoRepo.GetAllAsync();

            var basisSet = await this.BasisSetRepo.GetByIdAsync(basisSetId);


            StringBuilder input = new StringBuilder();
            input.AppendLine($" {basisSet.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=RHF RUNTYP=OPTIMIZE DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={charge} $END ");
            input.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine(" $STATPT NSTEP=999 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            AtomInfo current = null;
            foreach (var atom in CalculateInitialGeometry(geometryxyz))
            {
                current = atomInfoList.ToList().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",","."));
            }
            input.AppendLine(" $END");
            return input.ToString();
        }

        public static List<MoleculeAtom> CalculateInitialGeometry(string initialGeometry)
        {
            List<MoleculeAtom> retval = new List<MoleculeAtom>();
            foreach (var line in initialGeometry.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var segments = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (segments.Count() >= 4)
                {
                    retval.Add(new MoleculeAtom()
                    {
                        Symbol = segments[0],
                        PosX = QbcStringConvert.ToDecimal(segments[1].Trim()),
                        PosY = QbcStringConvert.ToDecimal(segments[2].Trim()),
                        PosZ = QbcStringConvert.ToDecimal(segments[3].Trim())
                    });
                }
            }
            return retval;
        }
    }
}
