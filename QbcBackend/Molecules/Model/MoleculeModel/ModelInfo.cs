using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.MoleculeModel
{
    public class ModelInfo : QbcBase
    {

        /// <summary>
        /// ProjectID
        /// </summary>
        public int ProjectID
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the Model
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description of the model
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// the charge of the model
        /// </summary>
        public int Charge
        {
            get;
            set;
        }

        /// <summary>
        /// The initial geometry that was used by this model
        /// </summary>
        public string InitialGeometry
        {
            get;
            set;
        }

        /// <summary>
        /// The current geometry , obtained after calculations
        /// </summary>
        public string CurrentGeometry
        {
            get;
            set;
        }

    }
}
