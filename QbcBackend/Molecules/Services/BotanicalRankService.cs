using FluentValidation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.Botany;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.Base.Service;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class BotanicalRankService : QbcBaseService, IBotanicalRankService
    {
        #region private helpers
        private IValidator Validation { get; }
        private IBotanicalNameTypeRepo Repo { get; }
        #endregion


        public BotanicalRankService(IValidator<BotanicalRank> validator, IBotanicalNameTypeRepo repo)
        {
            this.Validation = validator;

            this.Repo = repo;
        }

        public async Task<BotanicalRank> CreateAsync(BotanicalRank toCreate)
        {
            this.Validation.Validate(toCreate);

            if (await this.Repo.CountByNameAsync(toCreate.Name) > 0)
            {
                throw new NotUniqueException(toCreate, "Name");
            }

            BotanicalNameType toinput = new BotanicalNameType();
            toinput.Name = toCreate.Name;
            toinput.Description = toCreate.Description;
            var parent = await this.Repo.GetByNameAsync(toCreate.ParentName);
            if (parent != null)
            {
                toinput.BotanicalNameTypeId = parent.Id;
            }
            else
            {
                throw new NotExistsException($"The Parent BotanicalRank with name {toCreate.ParentName} does exists!");
            }
            Repo.Add(toinput);
            await Repo.SaveChangesAsync();
            return toCreate;
        }

        public async Task DeleteAsync(string name)
        {
            var result = await this.Repo.GetByNameAsync(name);
            if (result != null)
            {
                this.Repo.Remove(result.Id);
                await Repo.SaveChangesAsync();
            }
            else
            {
                throw new NotExistsException($"The BotanicalRank with name {name} does exists!");
            }
        }

        public async Task<ICollection<BotanicalRank>> GetAllAsync()
        {
            List<BotanicalRank> retval = new List<BotanicalRank>();
            List<BotanicalRank> temp = new List<BotanicalRank>();
            var result = await this.Repo.GetAllAsync();
            BotanicalRank current = null;
            foreach (var i in result)
            {
                current = new BotanicalRank
                {
                    Name = i.Name,
                    Description = i.Description,
                    ParentName = i.BotanicalNameTypeNavigation?.Name
                };
                temp.Add(current);
            }

            var init = temp.Find(i => String.IsNullOrWhiteSpace(i.ParentName));
            if (init != null)
            {
                retval.Add(init);
                retval.AddRange(MakeOrdered(temp, init.Name));
            }
            return retval;
        }

        public async Task<BotanicalRank> GetByNameAsync(string name)
        {
            BotanicalRank retval = null;
            var result = await this.Repo.GetByNameAsync(name);
            if (result != null)
            {
                retval = new BotanicalRank()
                {
                    Name = result.Name,
                    Description = result.Description,
                    ParentName = result.BotanicalNameTypeNavigation.Name
                };
            }
            return retval;
        }

        public async Task<ICollection<BotanicalRank>> GetPedigreeByNameAsync(string name)
        {
            List<BotanicalRank> retval = new List<BotanicalRank>();
            var result = await this.Repo.GetByNameAsync(name);
            BotanicalRank current = null;
            while (result != null)
            {
                current = new BotanicalRank
                {
                    Name = result.Name,
                    Description = result.Description,
                    ParentName = result.BotanicalNameTypeNavigation.Name

                };
                retval.Add(current);
            }
            retval.Reverse();
            return retval;
        }

        public async Task<BotanicalRank> UpdateAsync(string name, BotanicalRank toUpdate)
        {
            this.Validation.Validate(toUpdate);

            var result = await this.Repo.GetByNameAsync(name);
            if (result != null)
            {
                result.Name = name;
                result.Description = toUpdate.Description;
                var parent = await this.Repo.GetByNameAsync(toUpdate.ParentName);
                if (parent != null)
                {
                    result.BotanicalNameTypeId = parent.Id;
                }
                else
                {
                    throw new NotExistsException($"The Parent BotanicalRank with name {toUpdate.ParentName} does exists!");
                }

                if (name != toUpdate.Name && await this.Repo.CountByNameAsync(toUpdate.Name) > 0)
                {
                    throw new NotUniqueException(toUpdate, "Name");
                }
                await Repo.SaveChangesAsync();
            }
            else
            {
                throw new NotExistsException($"The BotanicalRank with name {name} does exists!");
            }
            return toUpdate;
        }


        #region private helpers

        private List<BotanicalRank> MakeOrdered(List<BotanicalRank> input, string name)
        {
            List<BotanicalRank> retval = new List<BotanicalRank>();
            var result = input.FindAll(i => i.ParentName == name);
            result.Sort((lhs, rhs) => lhs.Name.CompareTo(rhs.Name));
            foreach (var item in result)
            {
                retval.Add(item);
                retval.AddRange(MakeOrdered(input, item.Name));
            }
            return retval;
        }

        #endregion


    }
}
