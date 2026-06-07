using AutoMapper;
using BL.DTO.ProjectItem;
using DAL.Data.Models;
using DAL.GenericRepositories.ProjectItemRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.ProjectItemManager
{
    public class ProjectItemManager :IProjectItemManager
    {
        private readonly IProjectItemRepositories _projectItemRepositories;
        private readonly IMapper _mapper;
        public ProjectItemManager(IProjectItemRepositories projectItemRepositories, IMapper mapper)
        {
            _projectItemRepositories = projectItemRepositories;
            _mapper = mapper;
        }

        #region AddProject
        public GetAllProjectsDTO AddProject(CreateProjectDTO Project, string userid)
        {
            if (Project.Name == null)
                return null;
            var NewProject = _mapper.Map<ProjectItem>(Project);
            NewProject.UserId=userid;
            _projectItemRepositories.Create(NewProject);
            _projectItemRepositories.SaveChanges();
            return _mapper.Map<GetAllProjectsDTO>(NewProject);

        }
        #endregion

        #region DeleteProject
        public void DeleteProject(int id)
        { 
            var OldDAata = _projectItemRepositories.GetById(id);
            if (OldDAata == null)
                return;
            _projectItemRepositories.Delete(OldDAata );
            _projectItemRepositories.SaveChanges();
        }
        #endregion

        #region GetAllProjects
        public List<GetAllProjectsDTO> GetAllProjects()
        {
            var AllProjects = _projectItemRepositories.GetAll();
            return _mapper.Map<List<GetAllProjectsDTO>>(AllProjects);
        }
        #endregion

        #region GetProjectById
        public GetProjectByIdDTO? GetProjectById(int id)
        {
            var ProjectById = _projectItemRepositories.GetById(id);
            if(ProjectById == null) 
                return null;
            return _mapper.Map<GetProjectByIdDTO>(ProjectById);
        }
        #endregion

        #region UpdatrProject
        public bool UpdatrProject(UpdateProjectDTO ProjectNewData)
        {
            var OldData = _projectItemRepositories.GetById(ProjectNewData.Id);
            if(OldData == null) 
                return false;
            _mapper.Map(ProjectNewData, OldData);
            _projectItemRepositories.Update(OldData);
            _projectItemRepositories.SaveChanges();
            return true ;
        }
        #endregion

    }
}
