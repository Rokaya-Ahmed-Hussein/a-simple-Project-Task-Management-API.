using BL.DTO.TasksItem;
using BL.Managers.TasksItemManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Build_a_simple_Project___Task_Management_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksItemController : ControllerBase
    {
        private readonly ITasksItemManager _taskManager;
        public TasksItemController(ITasksItemManager tasksItemManager)
        {
            _taskManager = tasksItemManager;
        }
        #region Add New Task
        [HttpPost]
        [Authorize("ManagerPolicy")]
        [Route("AddNewTask")]
        
        public ActionResult<GetTasksByProjectDTO> AddNewTask(CreateTaskDTO NewTaskt)
        {
            var NewProject = _taskManager.AddTask(NewTaskt );
            return CreatedAtAction("GetTaskByProject", new { id = NewProject.ProjectId }, NewProject);
        }
        #endregion

        #region Update Task Status 
        [HttpPut]
        [Authorize("Both")]
        [Route("UpdatrTaskStatus")] 
        public ActionResult UpdatrTaskStatus(UpdateTaskDTO UpdateTask, int id)
        {
            if (id != UpdateTask.Id)
                return BadRequest("Task ID mismatch.");

            var updatedTask = _taskManager.UpdatTask(UpdateTask);
            if (updatedTask == null)
                return NotFound();

            return Ok(updatedTask);
        }
        #endregion

        #region Delete Task
        [HttpDelete]
        [Authorize("ManagerPolicy")]
        [Route("DeleteTask")]
        
        public ActionResult DeleteTask(int id)
        {
            _taskManager.DeleteTask(id);
            return NoContent();
        }
        #endregion

        #region Get Task By Project
        [HttpGet]
        [Authorize("Both")]
        [Route("GetTaskByProject")]
        
        public ActionResult<GetTasksByProjectDTO> GetTaskByProject(int id)
        {
            var TaskData = _taskManager.GetTaskByProject(id);
            if (TaskData == null)
                return NotFound();
            return Ok(TaskData);
        }
        #endregion
    }
}
