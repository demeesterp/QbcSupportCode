using QbcBackend.Molecules.Model.Botany;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IBotanicalRankService
    {

        Task<BotanicalRank> GetByNameAsync(string name);

        Task<ICollection<BotanicalRank>> GetPedigreeByNameAsync(string name);

        Task<ICollection<BotanicalRank>> GetAllAsync();

        Task<BotanicalRank> CreateAsync(BotanicalRank toCreate);

        Task<BotanicalRank> UpdateAsync(string name, BotanicalRank toUpdate);

        Task DeleteAsync(string name);

    }
}
