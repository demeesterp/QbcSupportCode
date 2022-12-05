using FluentValidation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.Botany;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.Base.Service;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class BotanicalNameService : QbcBaseService, IBotanicalNameService
    {

        #region private properties

        private IValidator Validator { get; }

        private IBotanicalNameRepo Repo { get; }

        private IBotanicalNameTypeRepo NameTypeRepo { get; }

        #endregion

        public BotanicalNameService(IValidator<BotanicalNameInfo> validator, IBotanicalNameRepo repo, IBotanicalNameTypeRepo nameTypeRepo)
        {
            this.Validator = validator;
            this.Repo = repo;
            this.NameTypeRepo = nameTypeRepo;
        }

        public async Task<BotanicalNameInfo> CreateAsync(BotanicalNameInfo toCreate)
        {
            this.Validator.Validate(toCreate);

            if (await this.Repo.CountByNameAsync(toCreate.Name) > 0)
            {
                throw new NotUniqueException(toCreate, "Name");
            }

            BotanicalName toinput = new BotanicalName
            {
                Name = toCreate.Name,
                Description = toCreate.Description
            };

            var parent = await this.Repo.GetByNameAsync(toCreate.ParentName);
            if (parent != null)
            {
                toinput.BotanicalNameId = parent.Id;
            }
            else
            {
                throw new NotExistsException($"The Parent BotanicalName with name {toCreate.ParentName} does exists!");
            }

            var type = await this.NameTypeRepo.GetByNameAsync(toCreate.Rank);
            if (parent != null)
            {
                toinput.BotanicalNameTypeId = type.Id;
            }
            else
            {
                throw new NotExistsException($"The Rank with name {toCreate.Rank} does exists!");
            }


            this.Repo.Add(toinput);

            await this.Repo.SaveChangesAsync();

            return await this.GetByNameAsync(toCreate.Name);
        }

        public async Task DeleteAsync(string name)
        {
            var result = await this.Repo.GetByNameAsync(name);
            if (result != null)
            {
                this.Repo.Remove(result.Id);
                await this.Repo.SaveChangesAsync();
            }
            else
            {
                throw new NotExistsException($"The BotanicalName with name {name} does exists!");
            }
        }

        public async Task<BotanicalNameInfo> UpdateAsync(BotanicalNameInfo toUpdate)
        {
            this.Validator.Validate(toUpdate);

            var result = await this.Repo.GetByIdAsync(toUpdate.Id);
            if (result != null)
            {
                result.Name = toUpdate.Name;
                result.Description = toUpdate.Description;

                var parent = await this.Repo.GetByNameAsync(toUpdate.ParentName);
                result.BotanicalNameId = parent?.Id;

                var type = await this.NameTypeRepo.GetByNameAsync(toUpdate.Rank);
                if (type != null)
                {
                    result.BotanicalNameTypeId = type.Id;
                }
                else
                {
                    throw new NotExistsException($"The Rank with name {toUpdate.Rank} does exists!");
                }

                if (result.Name != toUpdate.Name && await this.Repo.CountByNameAsync(toUpdate.Name) > 0)
                {
                    throw new NotUniqueException(toUpdate, "Name");
                }

                await Repo.SaveChangesAsync();
            }
            else
            {
                throw new NotExistsException($"The BotanicalRank with id {toUpdate.Id} does exists!");
            }
            return toUpdate;
        }

        public async Task<BotanicalNameInfo> GetAllByNameAsync(string name)
        {
            BotanicalNameInfo retval = await GetByNameAsync(name);
            if ( retval.Children?.Any() == true)
            {
                foreach(var child in retval.Children)
                {
                    var r = await this.GetAllByNameAsync(child.Name);
                    if ( r != null)
                    {
                        child.Children = r.Children;
                    }
                }
            }
            return retval;
        }

        public async Task<BotanicalNameInfo> GetByNameAsync(string name)
        {
            BotanicalNameInfo retval = null;
            var result = await this.Repo.GetByNameAsync(name);
            if (result != null)
            {
                retval = new BotanicalNameInfo()
                {
                    Name = result.Name,
                    Description = result.Description,
                    ParentName = result.BotanicalNameNavigation?.Name,
                    Id = result.Id
                };
                retval.Rank = result.BotanicalNameType.Name;
                if ( result.InverseBotanicalNameNavigation?.Any() == true)
                {
                    retval.Children = (from 
                                       i 
                                       in
                                       result.InverseBotanicalNameNavigation
                                       select
                                       new BotanicalNameInfo()
                                       {
                                           Name = i.Name,
                                           Description = i.Description,
                                           ParentName = retval.Name,
                                           Rank = i.BotanicalNameType?.Name,
                                           Id = i.Id
                                       }).ToList();
                }
                else
                {
                    retval.Children = new List<BotanicalNameInfo>();
                }
            }
            return retval;
        }

        public async Task<ICollection<BotanicalNameInfo>> GetPedigreeByNameAsync(string name)
        {
            List<BotanicalNameInfo> retval = new List<BotanicalNameInfo>();
            var result = await this.Repo.GetByNameAsync(name);
            while (result != null)
            {
                var current = new BotanicalNameInfo
                {
                    Name = result.Name,
                    Description = result.Description,
                    ParentName = result.BotanicalNameNavigation?.Name   
                };
                current.Rank = result.BotanicalNameType.Name;
                retval.Add(current);
                result = await this.Repo.GetByNameAsync(current.ParentName);
            }
            retval.Reverse();
            return retval;
        }

    }
}
