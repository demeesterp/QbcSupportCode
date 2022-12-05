using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;

namespace QbcBackend.Molecules.Repo
{
    public interface IAtomOrbitalRepository : IQbcBaseRepo
    {
        AtomOrbital Add(AtomOrbital entity);

        void Remove(int atomOrbitalId);

        AtomOrbital Get(int atomOrbitalId);

    }
}
