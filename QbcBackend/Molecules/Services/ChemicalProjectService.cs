using FluentValidation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.MoleculeProject;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{

    public class ChemicalProjectService : IChemicalProjectService
    {
        #region private properties

        private ICategoryRepository Repo
        {
            get;
        }

        private IValidator<Category> Validator { get; }

        #endregion

        public ChemicalProjectService(ICategoryRepository repo, IValidator<Category> validator) 
        {
            this.Repo = repo;
            this.Validator = validator;
        }

        public async Task<List<ChemicalProject>> SearchChemicalProjectAsync(string name)
        {
            return (from c in await this.Repo.GetAllAsync()
                    where c.Code.IndexOf(name) != -1
                    select new ChemicalProject()
                    {
                        Name = c.Code,
                        Description = c.Description,
                        Id = c.Id
                    }).ToList();
        }

        public async Task<ChemicalProject> GetChemicalProjectAsync(int id)
        {
            ChemicalProject retval = null;
            Category result = await this.Repo.GetByIdAsync(id);
            if (result != null)
            {
                retval = new ChemicalProject() { Description = result.Description, Id = result.Id, Name = result.Code };
            }
            return retval;
        }

        public async Task<List<ChemicalProject>> GetAllChemicalProjectAsync()
        {
            return (from c in await this.Repo.GetAllAsync()
                    select new ChemicalProject()
                    {
                        Name = c.Code,
                        Description = c.Description,
                        Id = c.Id
                    }).ToList();
        }

        public async Task<ChemicalProject> CreateAsync(ChemicalProject project)
        {
            if (project == null) throw new ArgumentNullException("project", "Null input values are not accepted !");
            Category newCategory = new Category()
            {
                Code = project.Name,
                Description = project.Description,
                Type = (int)CategoryType.MoleculeProject,
                Created = DateTime.Now
            };

            this.Validator.Validate(newCategory);

            this.Repo.Add(newCategory);

            await Repo.SaveChangesAsync();

            project.Id = newCategory.Id;



            return project;

        }

        public async Task UpdateAsync(int id, ChemicalProject project)
        {
            if ( project == null ) throw new ArgumentNullException("project","Null input values are not accepted !");
            Category newCategory = await this.Repo.GetByIdAsync(id);

            if (newCategory != null)
            {
                newCategory.Code = project.Name;
                newCategory.Description = project.Description;
                newCategory.Updated = DateTime.Now;
            }
            else
            {
                throw new NotExistsException($"The project {project.Name} does not exist and cannot be updated !");
            }

            Validator.Validate(newCategory);
            await Repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Repo.Remove(id);
            await Repo.SaveChangesAsync();
        }
    }
}
