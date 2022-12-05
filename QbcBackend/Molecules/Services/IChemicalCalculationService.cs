using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IChemicalCalculationService
    {

        Task<ChemicalCalculation> CreateAsync(ChemicalCalculation calculation);

        Task UpdateAsync(int id, ChemicalCalculation calculation);

        Task DeleteAsync(int id);

        Task<ICollection<ChemicalCalculation>> GetByModel(int modelID);

        Task<ChemicalCalculation> Get(int ID);

    }
}
