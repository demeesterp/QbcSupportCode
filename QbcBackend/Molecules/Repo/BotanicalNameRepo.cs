using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class BotanicalNameRepo: QbcBaseRepo, IBotanicalNameRepo
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public BotanicalNameRepo(QuantumbiochemistryContext dbContext) :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public BotanicalName Add(BotanicalName botanicalName)
        {
            this.DbContext.BotanicalName.Add(botanicalName);
            return botanicalName;
        }

        public async Task<int> CountByNameAsync(string name)
        {
            return await(from i in this.DbContext.BotanicalName 
                                where
                                 !string.IsNullOrWhiteSpace(name) &&  i.Name.ToLower() == name.ToLower() 
                                select i).CountAsync();
        }

        public async Task<ICollection<BotanicalName>> GetAllAsync()
        {
            return await this.DbContext.BotanicalName
                                .Include(p => p.BotanicalNameNavigation)
                                .Include(p => p.BotanicalNameType)
                                .Include(p => p.InverseBotanicalNameNavigation)
                                .ToListAsync();
        }

        public async Task<BotanicalName> GetByNameAsync(string name)
        {
            return await(from i in 
                                this.DbContext.BotanicalName 
                         where
                               !string.IsNullOrWhiteSpace(name) 
                               && 
                               i.Name.ToLower() == name.ToLower()
                         select i)
                            .Include(p => p.BotanicalNameType)
                            .Include(p => p.BotanicalNameNavigation)
                            .ThenInclude(ii => ii.BotanicalNameType)
                            .Include(p => p.InverseBotanicalNameNavigation)
                            .ThenInclude(ii => ii.BotanicalNameType)
                            .FirstOrDefaultAsync();
        }

        public async Task<BotanicalName> GetByIdAsync(int id)
        {
            return await(from i in
                                this.DbContext.BotanicalName
                         where
                               i.Id == id
                         select i)
                            .Include(p => p.BotanicalNameType)
                            .Include(p => p.BotanicalNameNavigation)
                            .ThenInclude(ii => ii.BotanicalNameType)
                            .Include(p => p.InverseBotanicalNameNavigation)
                            .ThenInclude(ii => ii.BotanicalNameType)
                            .FirstOrDefaultAsync();
        }

        public void Remove(int botanicalNameId)
        {
            var result = this.DbContext.BotanicalName.Find(botanicalNameId);
            if ( result != null)
            {
                this.DbContext.BotanicalName.Remove(result);
            }
        }


    }
}
