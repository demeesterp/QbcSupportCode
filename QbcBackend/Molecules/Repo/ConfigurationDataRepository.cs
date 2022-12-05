using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class ConfigurationDataRepository : QbcBaseRepo, IConfigurationDataRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public ConfigurationDataRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public ConfigurationData Add(ConfigurationData entity)
        {
            this.DbContext.ConfigurationData.Add(entity);
            return entity;
        }

        public void Remove(int configDataId)
        {
            var result = this.DbContext.ConfigurationData.Find(configDataId);
            if ( result  != null)
            {
                this.DbContext.ConfigurationData.Remove(result);
            }
        }

        public async Task<ICollection<ConfigurationData>> GetAllAsync()
        {
            return await(from i in this.DbContext.ConfigurationData select i).ToListAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await(from i in this.DbContext.ConfigurationData select i).CountAsync();
        }
    }
}
