using DAL.Data.Context;
using DAL.Data.Models;
using DAL.GenericRepositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.ProjectItemRepositories
{
    public class ProjectItemRepositories : GenericRepositories<ProjectItem>, IProjectItemRepositories
    {
        public ProjectItemRepositories(AppDbContext appDbContext):base(appDbContext)
        {
            
        }
    }
}
