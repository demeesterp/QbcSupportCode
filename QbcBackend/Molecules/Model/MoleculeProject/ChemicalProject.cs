using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.MoleculeProject
{
    /// <summary>
    /// A molecular calculation project 
    /// </summary>
    public class ChemicalProject : QbcBase
    {
        /// <summary>
        /// The name of the project
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// De description of the project
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The code of the parent project when the project is a subproject
        /// </summary>
        public string ParentCode
        {
            get;
            set;
        }

    }
}
