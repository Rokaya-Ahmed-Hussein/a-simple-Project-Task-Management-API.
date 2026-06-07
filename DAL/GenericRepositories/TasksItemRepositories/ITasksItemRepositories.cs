using DAL.Data.Models;
using DAL.GenericRepositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.TasksItemRepositories
{
    public interface ITasksItemRepositories : IGenericRepositories<TasksItem>
    {
        List<TasksItem> GetTasksByProject(int id);
    }
}
