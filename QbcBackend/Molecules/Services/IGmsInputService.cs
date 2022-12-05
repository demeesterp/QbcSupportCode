using QbcBackend.Molecules.Model.GmsInput;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IGmsInputService
    {

        Task<List<FukuiInputInfo>> GenFukuiInput(string geometryxyz, int charge, int basisSetId);

        Task<string> GenOptInput(string geometryxyz, int charge, int basisSetId);

        Task<string> GenCHelpGChargeInput(string geometryxyz, int charge, int basisSetId);

        Task<string> GenGeoDiskChargeInput(string geometryxyz, int charge, int basisSetId);

    }
}
