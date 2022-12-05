using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IMoleculeElpotRepository : IQbcBaseRepo
    { 
    
        MoleculeElPot Add(MoleculeElPot entity);

        void Remove(int moleculeElpotId);

        Task<ICollection<MoleculeElPot>> GetByMoleculeAsync(int moleculeId);

        Task<MoleculeElPot> GetByIdAsync(int id);

        Task<int> CountByMoleculeAsync(int moleculeId);


    }
}
