using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QbcBackend.Tools.Base.Repo
{
    public abstract class QbcBaseRepo
    {
        #region private properties

        private DbContext Context { get; }

        #endregion

        protected QbcBaseRepo(DbContext context)
        {
            this.Context = context;
        }


        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }


    }
}
