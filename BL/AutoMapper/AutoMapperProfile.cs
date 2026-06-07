using AutoMapper;
using BL.DTO.ProjectItem;
using BL.DTO.TasksItem;
using DAL.Data.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BL.AutoMapper
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            #region retrive data
            CreateMap<ProjectItem , GetAllProjectsDTO>();
            CreateMap<ProjectItem , GetProjectByIdDTO>();
            CreateMap<TasksItem , GetTasksByProjectDTO>();

            #endregion

            #region add data
            CreateMap<CreateProjectDTO , ProjectItem>();
            CreateMap<CreateTaskDTO , TasksItem>();
            #endregion

            #region update data
            CreateMap<UpdateProjectDTO ,ProjectItem>();
            CreateMap<UpdateTaskDTO, TasksItem>();

            #endregion


        }
    }
}
