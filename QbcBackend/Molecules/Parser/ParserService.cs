using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Parser
{
    public class ParserService : IParserService
    {
        #region private helpers

        public List<IParserCommand> ParseOptimizationCmd { get; }


        public List<IParserCommand> ParseChargeCmd { get; }


        public List<IParserCommand> ParseGmsInputCmd { get; }

        #endregion

        public ParserService()
        {
            ParseOptimizationCmd = new List<IParserCommand>()
            {
                new OptimizedGeometryParseCmd(),
                new OptimizedDftEnergyCmd(),
                new OptimizationPopulationAnalysisCmd()
            };

            ParseChargeCmd = new List<IParserCommand>()
            {
                new ChargeCommand()
            };

            ParseGmsInputCmd = new List<IParserCommand>()
            {
                new GmsInputParseCmd()
            };
        }

        public ParseResult ParseGmsInput(string content)
        {
            ParseResult retval = NewMethod();
            retval.Molecule = new MoleculeInfo();
            if (!String.IsNullOrWhiteSpace(content))
            {
                var lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                this.ParseGmsInputCmd.ForEach((i) => i.Parse(lines, retval.Molecule));
            }
            return retval;
        }

        private static ParseResult NewMethod()
        {
            return new ParseResult();
        }

        public ParseResult ParseGmsInputForFukui(string content)
        {
            ParseResult retval = new ParseResult();
            if ( !String.IsNullOrWhiteSpace(content))
            {
                string[] items = content.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach(string i in items)
                {
                    string[] contentItems = i.Replace("(","")
                                                .Replace(")","")
                                                    .Split(new string[] {","},
                                                            StringSplitOptions.RemoveEmptyEntries);
                    if(contentItems.Length > 1 
                            && contentItems[0].Trim() == "neutral")
                    {
                        retval = ParseGmsInput(contentItems[1]);
                        break;
                    }
                }
            }
            return retval;
        }

        public async Task<ParseResult> ParseChargeAsync(string content, MoleculeInfo existingMolecule)
        {
            ParseResult retval = new ParseResult();
            retval.Molecule = existingMolecule;
            if (CheckValid(content))
            {
                retval.Error = false;
                var lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                this.ParseChargeCmd.ForEach((i) => i.Parse(lines, retval.Molecule));
            }
            else
            {
                retval.Error = true;
            }
            return await Task.FromResult<ParseResult>(retval);
        }

        public async Task<ParseResult> ParseFukuiAsync(string neutralcontent, string basecontent, string acidcontent, MoleculeInfo existingMolecule)
        {
            ParseResult retval = new ParseResult();
            List<IParserCommand> commands = new List<IParserCommand>();


            if (CheckValid(neutralcontent) && CheckValid(basecontent) && CheckValid(acidcontent))
            {
                // Neutral parsing
                var popcmd = new NeutralPopulationAnalysisCmd();
                var lines = neutralcontent.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                popcmd.Parse(lines, existingMolecule);

                var energyCommand = new NeutralFukuiEnergyCommand();
                var neutralEnergy = energyCommand.Parse(lines);

                // Acid Parsing
                var acidpopcmd = new LewisAcidPopulationAnalysisCmd();
                lines = acidcontent.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                acidpopcmd.Parse(lines, existingMolecule);

                var acidEnergyCommand = new LewisAcidFukuiEnergyCommand();
                var acidEnergy = acidEnergyCommand.Parse(lines);

                // Base parsing
                var basepopcmd = new LewisBasePopulationAnalysisCmd();
                lines = basecontent.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                basepopcmd.Parse(lines, existingMolecule);

                var baseEnergyCommand = new LewisBaseFukuiEnergyCommand();
                var baseEnergy = baseEnergyCommand.Parse(lines);




                decimal I = baseEnergy - neutralEnergy;
                decimal A = neutralEnergy - acidEnergy;

                existingMolecule.ElectronAffinity = (I + A) / 2;
                existingMolecule.Hardness = (I - A) / 2;

                retval.Molecule = existingMolecule;
                retval.Error = false;
            }
            else
            {
                retval.Error = true;
            }
            return await Task.FromResult<ParseResult>(retval);
        }

        public async Task<ParseResult> ParseOptimizationAsync(string content)
        {
            ParseResult retval = new ParseResult();
            if (CheckValid(content))
            {
                retval.Error = false;
                var lines = content.Split(new string[] { "\r\n" },StringSplitOptions.None).ToList();
                this.ParseOptimizationCmd.ForEach((i) => i.Parse(lines, retval.Molecule));
            }
            else
            {
                retval.Error = true;
            }
            return await Task.FromResult<ParseResult>(retval);
        }


        #region private helpers


        private bool CheckValid(string content)
        {
            return content.Contains("EXECUTION OF GAMESS TERMINATED NORMALLY");
        }


        #endregion

    }
}
