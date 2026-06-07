using BL.DTO.User;
using BL.Managers.ApplicationUserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Build_a_simple_Project___Task_Management_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserManager _applicationUserManager;
        public UsersController(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }
        #region User Register
        [HttpPost]
        [Route("EmployeeUserRegister")]
        public async Task<ActionResult> EmployeeUserRegister(RegisterDTO model)
        {
            var result = await _applicationUserManager.EmployeeRegister(model);

            if (!result)
                return BadRequest("Registration failed");

            return Ok("User registered successfully");
        }
        #endregion 
        #region User Register
        [HttpPost]
        [Route("ManagerUserRegister")]
        public async Task<ActionResult> ManagerUserRegister(RegisterDTO model)
        {
            var result = await _applicationUserManager.ManagerRegister(model);

            if (!result)
                return BadRequest("Registration failed");

            return Ok("User registered successfully");
        }
        #endregion 


        #region User Login
        [HttpPost]
        [Route("Longin")]
        public async Task<IActionResult> UserLogin(LoginDTO Log)
        {
            var result = await _applicationUserManager.Login(Log);

            if (result == null)
                return Unauthorized("Invalid username or password");

            return Ok(result);
        }
        #endregion


    }
}
