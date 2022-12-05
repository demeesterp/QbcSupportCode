using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Tools.StringConversion;
using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Parser
{
    public class OptimizedDftEnergyCmd : IParserCommand
    {

        #region private members

        private const string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";

        private const string EnergyStartTag = "          ENERGY COMPONENTS";

        private const string EnergyTag = "                       TOTAL ENERGY";

        #endregion



        public bool Parse(List<string> input, MoleculeInfo molecule)
        {
            bool start = false;
            bool overallstart = false;
            string line = "";
            for (int c= 0; c < input.Count; ++c)
            {
                line = input[c];
                if ( line.Contains(OptimizationResultTag))
                {
                    overallstart = true;
                }

                if (overallstart && line.Contains(EnergyStartTag))
                {
                    start = true;
                }

                if ( start && line.Contains(EnergyTag))
                {
                   var data = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if ( data.Length > 1)
                    {
                        molecule.DftEnergy = QbcStringConvert.ToDecimal(data[1].Trim());

                        break;
                    }
                }
            }
            return start;
        }
    }
}
