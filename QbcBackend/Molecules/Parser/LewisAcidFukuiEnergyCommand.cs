﻿namespace QbcBackend.Molecules.Parser
{
    public class LewisAcidFukuiEnergyCommand : FukuiEnergyCommand
    {
        #region tags


        private const string StartTag = "     PROPERTY VALUES FOR THE UHF   SELF-CONSISTENT FIELD WAVEFUNCTION";


        private const string EnergyTag = "TOTAL ENERGY";

        #endregion


        protected override string GetEnergyTag()
        {
            return StartTag;
        }

        protected override string GetStartTag()
        {
            return EnergyTag;
        }

    }
}
