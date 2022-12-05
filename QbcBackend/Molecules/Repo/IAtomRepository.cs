using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IAtomRepository : IQbcBaseRepo
    {

        Atom Add(Atom entity);

        void Remove(int atomID);

        Task<ICollection<Atom>> GetAtomByMolecule(int moleculeID);

        Task<int> CountAtomByMolecule(int moleculeID);
    }
}
