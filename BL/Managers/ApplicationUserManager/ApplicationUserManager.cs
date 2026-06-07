using AutoMapper;
using BL.DTO.User;
using DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.ApplicationUserManager
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManag;
        public ApplicationUserManager(UserManager<ApplicationUser> userManager , IConfiguration config) 
        {
            _userManag = userManager;
            _config = config;
        }
        #region Manager Register
        public async Task<bool> ManagerRegister(RegisterDTO model)
        {
            //Receive Data From User interface in Objet of Employee For the First Time
            var NewUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Department = model.Department
            };

            //Check Connection of DataBase
            var res = await _userManag.CreateAsync(NewUser, model.Password);
            if (!res.Succeeded)
                return false; 
            //if DataBase is Connected then Store Claims in DataBase of this user 
            //Set Claimes
            var Claaaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , NewUser.Id),
                new Claim(ClaimTypes.Role ,"Manager")

            };

            //Add Claimes to this user in DataBase(to Add Claimes we use the UserManager)
            var CreateClimes = await _userManag.AddClaimsAsync(NewUser, Claaaims);

            if (!CreateClimes.Succeeded)
                return false;
            return true;
        }
        #endregion

        #region Employee Register
        public async Task<bool> EmployeeRegister(RegisterDTO model)
        {
            //Receive Data From User interface in Objet of Employee For the First Time
            var NewUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Department = model.Department
            };

            //Check Connection of DataBase
            var res = await _userManag.CreateAsync(NewUser, model.Password);
            if (!res.Succeeded)
                return false; ;
            //if DataBase is Connected then Store Claims in DataBase of this user 
            //Set Claimes
            var Claaaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , NewUser.Id),
                new Claim(ClaimTypes.Role ,"Employee")

            };

            //Add Claimes to this user in DataBase(to Add Claimes we use the UserManager)
            var CreateClimes = await _userManag.AddClaimsAsync(NewUser, Claaaims);

            if (!CreateClimes.Succeeded)
                return false;
            return true;
        }
        #endregion

        #region LogIn
        public async Task<LoginResponseDTO?> Login(LoginDTO log)
        {
            //find user using its user name
            var UserData = await _userManag.FindByNameAsync(log.UserName);

            //check the password which the user enterd
            if (!await _userManag.CheckPasswordAsync(UserData, log.Password))
                return null;
            //create user Token ==> gtting Claimes from DataBase to set it in user token
            var UserClaimes = await _userManag.GetClaimsAsync(UserData);

            //setting key & put it in byte value
            var StringKey = _config.GetValue<string>("SecretKey");
            var ByteKey = Encoding.ASCII.GetBytes(StringKey);
            var key = new SymmetricSecurityKey(ByteKey);

            //combine secret key with Hashing Algorithm
            var SigningCrad = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //Generate Token
            //create an object contain token
            var jwt = new JwtSecurityToken(

                 claims: UserClaimes,
                 signingCredentials: SigningCrad,
                 expires: DateTime.Now.AddMinutes(_config.GetValue<int>("TokenDuration")),
                 notBefore: DateTime.Now

                );
            //Generate Token as String
            var tokkaenhandler = new JwtSecurityTokenHandler();
            var tokenstring = tokkaenhandler.WriteToken(jwt);

            return new LoginResponseDTO
            {
                Token = tokenstring,
                Expire = DateTime.Now.AddMinutes(_config.GetValue<int>("TokenDuration"))
            };
          
        }
        #endregion
    }

}
