using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IChemicalBasissetService
    {
        Task<List<BasisSetInfo>> GetAllAsync();
    }
}
