using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IBondRepository : IQbcBaseRepo
    {

        Bond Add(Bond entity);

        void Remove(int bondId);

        Task<List<Bond>> GetBondForMolecule(int moleculeId);


    }
}
