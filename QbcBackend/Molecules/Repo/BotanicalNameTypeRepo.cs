using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class BotanicalNameTypeRepo : QbcBaseRepo, IBotanicalNameTypeRepo
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public BotanicalNameTypeRepo(QuantumbiochemistryContext dbContext):
            base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public BotanicalNameType Add(BotanicalNameType botanicalNameType)
        {
            this.DbContext.BotanicalNameType.Add(botanicalNameType);
            return botanicalNameType;
        }

        public async Task<int> CountByNameAsync(string name)
        {
            return await(from i in this.DbContext.BotanicalNameType where i.Name.ToLower() == name.ToLower() select i).CountAsync();
        }

        public async Task<ICollection<BotanicalNameType>> GetAllAsync()
        {
            return await this.DbContext.BotanicalNameType.Include(p => p.BotanicalNameTypeNavigation).ToListAsync();
        }

        public async Task<BotanicalNameType> GetByNameAsync(string name)
        {
            return await(from i in this.DbContext.BotanicalNameType where i.Name.ToLower() == name.ToLower() select i).FirstOrDefaultAsync();
        }

        public void Remove(int botanicalNameTypeId)
        {
            var result = this.DbContext.BotanicalNameType.Find(botanicalNameTypeId);
            if (result != null)
            {
                this.DbContext.BotanicalNameType.Remove(result);
            }
        }
    }
}
