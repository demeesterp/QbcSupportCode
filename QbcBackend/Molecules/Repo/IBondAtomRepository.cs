using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;

namespace QbcBackend.Molecules.Repo
{
    public interface IBondAtomRepository: IQbcBaseRepo
    {

        BondAtom Add(BondAtom entity);

        void Remove(int bondAtomId);

    }
}
