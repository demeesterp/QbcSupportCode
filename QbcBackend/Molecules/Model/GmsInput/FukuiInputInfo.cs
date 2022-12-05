namespace QbcBackend.Molecules.Model.GmsInput
{
    public enum FukuiInputType
    {
        neutral,
        lewisbase,
        lewisacid
    }

    public class FukuiInputInfo
    {
        public string GmsInput { get; set; }

        public FukuiInputType Type { get; set; }
    }
}
