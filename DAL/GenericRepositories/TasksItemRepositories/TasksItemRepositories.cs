using DAL.Data.Context;
using DAL.Data.Models;
using DAL.GenericRepositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.TasksItemRepositories
{
    public class TasksItemRepositories : GenericRepositories<TasksItem> , ITasksItemRepositories
    {
        private readonly AppDbContext _appDbContext;
        public TasksItemRepositories(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<TasksItem> GetTasksByProject(int id)
        {
            return _appDbContext.TasksItems.Where(x => x.ProjectId == id).ToList();
        }
    }
}
