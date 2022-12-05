using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IBotanicalNameRepo : IQbcBaseRepo
    {

        BotanicalName Add(BotanicalName botanicalName);

        void Remove(int botanicalNameId);

        /// <summary>
        /// GetByNameAsync
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>list of botanical names</returns>
        Task<BotanicalName> GetByNameAsync(string name);

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id">id of the name</param>
        /// <returns>corresponding botanical name</returns>
        Task<BotanicalName> GetByIdAsync(int id);

        /// <summary>
        /// CountByNameAsync
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>count</returns>
        Task<int> CountByNameAsync(string name);


        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <returns>All botanical names</returns>
        Task<ICollection<BotanicalName>> GetAllAsync();



    }
}
