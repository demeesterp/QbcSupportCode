using QbcBackend.Molecules.Model.CalculationStatus;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface ICalculationStatusService
    {
        Task<CalculationStatusTransferResult> TransferCalculation(ExecutionStatus statusBefore, ChemicalCalculation calculationAfter);
    }

}
