using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Models
{
    public class ProjectListViewModel
    {

        public ProjectListViewModel()
        {
            this.ProjectList = new List<ChemicalProjectViewModel>();
        }

        public List<ChemicalProjectViewModel> ProjectList
        {
            get;
            set;
        }


    }
}
