using QbcBackend.Molecules.Model.Botany;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IBotanicalNameService
    {
        Task<BotanicalNameInfo> GetByNameAsync(string name);

        Task<ICollection<BotanicalNameInfo>> GetPedigreeByNameAsync(string name);

        Task<BotanicalNameInfo> GetAllByNameAsync(string name);

        Task<BotanicalNameInfo> CreateAsync(BotanicalNameInfo toCreate);

        Task<BotanicalNameInfo> UpdateAsync(BotanicalNameInfo toUpdate);

        Task DeleteAsync(string name);
    }
}
