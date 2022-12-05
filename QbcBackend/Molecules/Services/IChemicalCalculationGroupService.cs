using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IChemicalCalculationGroupService
    {
        Task<ChemicalCalculationGroup> CreateAsync(ChemicalCalculationGroup calculationGroup);

        Task UpdateAsync(int id, ChemicalCalculationGroup calculationGroup);

        Task DeleteAsync(int id);

        Task<List<ChemicalCalculationGroup>> GetByParentAsync(int parentID);
    }
}
