using BL.DTO.ProjectItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.ProjectItemManager
{
    public interface IProjectItemManager
    {
        List<GetAllProjectsDTO> GetAllProjects();
        GetProjectByIdDTO? GetProjectById(int id);
        GetAllProjectsDTO AddProject(CreateProjectDTO Ptoject, string userid);
        bool UpdatrProject(UpdateProjectDTO Project);
        void DeleteProject(int id);
    }
}
