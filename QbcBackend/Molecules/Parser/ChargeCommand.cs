using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Tools.StringConversion;
using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Parser
{
    public class ChargeCommand : IParserCommand
    {

        #region private tags

        private const string StartTag = "          ELECTROSTATIC POTENTIAL";

        private const string StartChargedTag = " NET CHARGES:";

        private const string EndChargeTag = " -------------------------------------";

        private const string StartElpotTag = " NUMBER OF POINTS SELECTED FOR FITTING";

        private const string GeoDiscTag = "PTSEL=GEODESIC";

        #endregion



        public bool Parse(List<string> input, MoleculeInfo molecule)
        {
            bool retval = false;

            string line = string.Empty;
            bool overallstart = false;
            bool startCharge = false;
            bool startElpot = false;
            bool isGeoDisc = false;
            int currentAtomPos = 1;
            for(int c = 0; c < input.Count; ++c)
            {
                line = input[c];

                if ( line.Contains(StartTag))
                {
                    overallstart = true;
                }

                if (line.Contains(GeoDiscTag))
                {
                    isGeoDisc = true;
                }

                if (overallstart && line.Contains(StartChargedTag))
                {
                    startCharge = true;
                    c += 3;
                    continue;
                }



                if ( overallstart && line.StartsWith(StartElpotTag))
                {
                    startElpot = true;
                    continue;
                }

                if (startElpot)
                {
                    if ( String.IsNullOrWhiteSpace(line))
                    {
                        startElpot = false;
                        continue;
                    }

                    var data = line.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                    if ( data.Length > 6)
                    {
                        ElPot item = new ElPot()
                        {
                            PosX = Convert.ToDecimal(data[1]),
                            PosY = Convert.ToDecimal(data[2]),
                            PosZ = Convert.ToDecimal(data[3]),
                            Electronic = Convert.ToDecimal(data[4]),
                            Nuclear = Convert.ToDecimal(data[5]),
                            Total = Convert.ToDecimal(data[6]),
                            MoleculeID = molecule.Id,
                            Type = isGeoDisc ? (int)ElPotType.GeoDisc : (int)ElPotType.CHelgG
                        };
                        molecule.ElPot.Add(item);
                    }
                }

                if (startCharge)
                {
                    if ( line.Contains(EndChargeTag))
                    {
                        return true;
                    }

                    var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if ( data.Length > 2)
                    {
                        string symbol = data[0];
                        decimal charge = QbcStringConvert.ToDecimal(data[1].Trim());
                        var atom = molecule.Atoms.Find(i => i.Position == currentAtomPos && i.Symbol == symbol);
                        if ( atom != null)
                        {
                            if (isGeoDisc)
                            {
                                atom.GeoDiscCharge = charge;
                            }
                            else
                            {
                                atom.CHelpGCharge = charge;
                            }
                            
                        }
                        ++currentAtomPos;
                    }
                }
            }
            return retval;
        }
    }
}
