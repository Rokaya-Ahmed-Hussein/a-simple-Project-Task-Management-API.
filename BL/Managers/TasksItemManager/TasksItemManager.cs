using AutoMapper;
using BL.DTO.ProjectItem;
using BL.DTO.TasksItem;
using DAL.Data.Models;
using DAL.GenericRepositories.ProjectItemRepositories;
using DAL.GenericRepositories.TasksItemRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.TasksItemManager
{
    public class TasksItemManager : ITasksItemManager
    {
        private readonly ITasksItemRepositories _tasksItemRepositories;
        private readonly IMapper _mapper;
        public TasksItemManager(ITasksItemRepositories tasksItemRepositories, IMapper mapper)
        {
            _tasksItemRepositories = tasksItemRepositories;
            _mapper = mapper;
        }

        #region AddTask
        public GetTasksByProjectDTO AddTask(CreateTaskDTO Newtask )
        {
            if (Newtask == null)
                return null;
            var TaskData = _mapper.Map<TasksItem>(Newtask);
            
            _tasksItemRepositories.Create(TaskData);
            _tasksItemRepositories.SaveChanges();
            return _mapper.Map<GetTasksByProjectDTO>(TaskData);

        }
        #endregion

        #region DeleteTask
        public void DeleteTask(int id)
        {
            var OldData = _tasksItemRepositories.GetById(id);
            if (OldData == null)
                return ;
            _tasksItemRepositories.Delete(OldData);
            _tasksItemRepositories.SaveChanges();

        }
        #endregion

        #region GetTaskByProject
        public List<GetTasksByProjectDTO?> GetTaskByProject(int id)
        {
            var TaskData = _tasksItemRepositories.GetTasksByProject(id);
            if(TaskData == null)
                return null;
            return _mapper.Map<List<GetTasksByProjectDTO>>(TaskData);
        }
        #endregion

        #region UpdatTask
        public bool UpdatTask(UpdateTaskDTO Updatetask)
        {
            var OldData = _tasksItemRepositories.GetById(Updatetask.Id);
            if (OldData == null)
                return false;
            _mapper.Map(Updatetask, OldData);
            _tasksItemRepositories.Update(OldData);
            _tasksItemRepositories.SaveChanges();
            return true;
        }
        #endregion
    }
}
