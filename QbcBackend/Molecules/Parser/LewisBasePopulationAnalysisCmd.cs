namespace QbcBackend.Molecules.Parser
{
    public class LewisBasePopulationAnalysisCmd : UHFPopulationAnalysisCmd
    {
        protected override PopulationStatus GetPopulationStatus()
        {
            return PopulationStatus.lewisbase;
        }
    }
}
