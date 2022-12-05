using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;

namespace QbcBackend.Molecules.Repo
{
    public interface IMoleculePropertyRepository: IQbcBaseRepo
    {

        MoleculeProperty Add(MoleculeProperty entity);

        void Remove(int moleculePropertyId);

    }
}
