using BL.DTO.ProjectItem;
using BL.Managers.ProjectItemManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Build_a_simple_Project___Task_Management_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectItemController : ControllerBase
    {
        private readonly IProjectItemManager _projectItemManager;
        public ProjectItemController(IProjectItemManager projectItemManager)
        {
            _projectItemManager = projectItemManager;
        }

        #region All Projects
        [HttpGet]
        [Authorize("EmployeePolicy")]
        [Route("AllProjects")]
        
        public ActionResult<IEnumerable<GetAllProjectsDTO>> AllProjects()
        {
            return _projectItemManager.GetAllProjects();
        }

        #endregion

        #region Get Project By Id

        [HttpGet]
        [Authorize("EmployeePolicy")]
        [Route("getProjectbyid/{id}")]
        
        public ActionResult<GetProjectByIdDTO> getProjectbyid(int id)
        {
            var ProjectData = _projectItemManager.GetProjectById(id);
            if(ProjectData == null)
                return NotFound();
            return Ok(ProjectData);
           
        }
        #endregion

        #region Create New Project
        [HttpPost]
        [Authorize("ManagerPolicy")]
        [Route("CreateNewProject")]
        
        public ActionResult<GetAllProjectsDTO> CreateNewProject(CreateProjectDTO NewPtoject)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var NewProject = _projectItemManager.AddProject(NewPtoject, userId);
            return Ok();
        }
        #endregion

        #region Update Project
        [HttpPut]
        [Authorize("ManagerPolicy")]
        [Route("UpdateProject/{id}")]
        
        public ActionResult UpdateProject(UpdateProjectDTO NewData, int id)
        {
            if (NewData.Id != id)
                return BadRequest();
            var Updated = _projectItemManager.UpdatrProject(NewData);
            return Ok(Updated);
        }

        #endregion

        #region Delete Project
        [HttpDelete]
        [Authorize("ManagerPolicy")]
        [Route("DeleteProject")]
        
        public ActionResult DeleteProject(int id)
        {
            _projectItemManager.DeleteProject(id);
            return NoContent();    
        }
        #endregion



    }
}
