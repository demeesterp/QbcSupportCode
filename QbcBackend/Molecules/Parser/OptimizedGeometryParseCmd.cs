using QbcBackend.Molecules.Model.Molecule;
using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Parser
{
    public class OptimizedGeometryParseCmd : IParserCommand
    {
        #region private members

        private const string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";
        
        #endregion

        public bool Parse(List<string> input, MoleculeInfo molecule)
        {
            bool retval = true;
            string line = string.Empty;
            bool start = false;
            int position = 1;
            for (int c = 0; c < input.Count; ++c)
            {
                line = input[c];
                if (line.Contains(OptimizationResultTag))
                {
                    start = true;
                    c += 3;
                    continue;
                }

                if (start)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        retval = start;
                        break;
                    }

                    var newatom = ParserUtilities.ParseOptAtomPosition(line);
                    newatom.Position = position;
                    newatom.Number = position;

                    var existingAtom = molecule.Atoms.Find((i) => i.Symbol == newatom.Symbol 
                                                                && i.Position == position 
                                                                && i.Number == position);
                    if ( existingAtom != null)
                    {
                        existingAtom.PosX = newatom.PosX;
                        existingAtom.PosY = newatom.PosY;
                        existingAtom.PosZ = newatom.PosZ;
                        existingAtom.AtomicWeight = newatom.AtomicWeight;
                    }
                    else
                    {
                        molecule.Atoms.Add(newatom);
                    }

                    ++position;
                }
            }

            return retval;
        }
    }
}
