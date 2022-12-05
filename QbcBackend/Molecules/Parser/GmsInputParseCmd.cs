using System;
using System.Collections.Generic;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Tools.StringConversion;

namespace QbcBackend.Molecules.Parser
{
    public class GmsInputParseCmd : IParserCommand
    {

        #region private helpers

        private const string GmsInputDataTag = "$Data";

        #endregion


        public bool Parse(List<string> input, MoleculeInfo molecule)
        {
            bool retval = true;
            int start = input.FindIndex(i => i == GmsInputDataTag);
            int position = 1;
            for (int pos = start + 1; pos < input.Count; ++pos)
            {
                var result = input[pos].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (result.Length == 5)
                {
                    molecule.Atoms.Add(new MoleculeAtom()
                    {
                        Symbol = result[0],
                        AtomicWeight = (int)QbcStringConvert.ToDecimal(result[1]),
                        PosX = QbcStringConvert.ToDecimal(result[2]),
                        PosY = QbcStringConvert.ToDecimal(result[3]),
                        PosZ = QbcStringConvert.ToDecimal(result[4]),
                        Position = position++
                    });
                }
            }
            return retval;
        }
    }
}
