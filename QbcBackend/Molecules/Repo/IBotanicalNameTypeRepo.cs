using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IBotanicalNameTypeRepo : IQbcBaseRepo
    {
        /// <summary>
        /// Add a botanical name type to the context
        /// </summary>
        /// <param name="botanicalNameType">botanicalName Type</param>
        /// <returns></returns>
        BotanicalNameType Add(BotanicalNameType botanicalNameType);

        /// <summary>
        /// Remove a botanical name type from context
        /// </summary>
        /// <param name="botanicalNameTypeId">the ID of the botanical Name</param>
        void Remove(int botanicalNameTypeId);

        /// <summary>
        /// GetByNameAsync
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        Task<BotanicalNameType> GetByNameAsync(string name);

        /// <summary>
        /// CountByNameAsync
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        Task<int> CountByNameAsync(string name);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BotanicalNameType>> GetAllAsync();


    }
}
