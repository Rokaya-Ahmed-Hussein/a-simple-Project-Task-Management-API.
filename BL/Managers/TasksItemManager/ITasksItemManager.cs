using BL.DTO.ProjectItem;
using BL.DTO.TasksItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.TasksItemManager
{
    public interface ITasksItemManager
    {
        GetTasksByProjectDTO AddTask(CreateTaskDTO Task);
        bool UpdatTask(UpdateTaskDTO Task);
        void DeleteTask(int id);
        List<GetTasksByProjectDTO?> GetTaskByProject(int id);
    }
}
