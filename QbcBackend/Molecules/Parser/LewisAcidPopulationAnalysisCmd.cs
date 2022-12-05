namespace QbcBackend.Molecules.Parser
{
    public class LewisAcidPopulationAnalysisCmd : UHFPopulationAnalysisCmd
    {
        protected override PopulationStatus GetPopulationStatus()
        {
            return PopulationStatus.lewisacid;
        }
    }
}
