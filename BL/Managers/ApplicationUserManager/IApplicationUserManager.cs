using BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.ApplicationUserManager
{
    public interface IApplicationUserManager
    {
        Task<bool> ManagerRegister(RegisterDTO model);
        Task<bool> EmployeeRegister(RegisterDTO model);
        Task<LoginResponseDTO?> Login(LoginDTO model);
    }
}
