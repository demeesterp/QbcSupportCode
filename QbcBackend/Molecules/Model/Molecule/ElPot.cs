using QbcBackend.Tools.Base.Model;
using System;

namespace QbcBackend.Molecules.Model.Molecule
{
    public enum ElPotType
    {
        CHelgG = 1,
        GeoDisc = 2,
        Connolly = 3
    }


    public class ElPot : QbcBase
    {
        public int MoleculeID { get; set; }
        public int Type { get; set; }
        public Decimal PosX { get; set; }
        public Decimal PosY { get; set; }
        public Decimal PosZ { get; set; }
        public Decimal Nuclear { get; set; }
        public Decimal Electronic { get; set; }
        public Decimal Total { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
