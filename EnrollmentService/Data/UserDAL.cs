using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Identity;

namespace EnrollmentService.Data
{
    public class UserDAL : IUser
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        // private AppSettings _appSettings;

        public UserDAL(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            // _appSettings = appSettings.Value;
        }
        public async Task AddRole(string rolename)
        {
            IdentityResult roleResult;
            try
            {
                var roleIsExist = await _roleManager.RoleExistsAsync(rolename);
                if(!roleIsExist)
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(rolename));
                else
                    throw new System.Exception($"Role {rolename} sudah ada");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task AddUserToRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            try
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error {ex.Message}");
            }
        }

        public Task<User> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAllUser()
        {
            List<UserDto> users = new List<UserDto>();
            var results = _userManager.Users;
            foreach(var user in results)
            {
                users.Add(new UserDto { Username = user.UserName });
            }

            return users;
        }

        public IEnumerable<CreateRoleDto> GetRoles()
        {
            List<CreateRoleDto> lstRole = new List<CreateRoleDto>();
            var results = _roleManager.Roles;
            foreach(var role in results)
            {
                lstRole.Add(new CreateRoleDto { RoleName = role.Name });    
            }

            return lstRole;
        }

        public async Task<List<string>> GetRolesFromUser(string username)
        {
            List<string> lstRoles = new List<string>();
            var user = await _userManager.FindByEmailAsync(username);

            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                lstRoles.Add(role);
            }
            return lstRoles;
        }

        public async Task Registration(CreateUserDto user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Username,Email=user.Username
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if(!result.Succeeded)
                    throw new System.Exception("Gagal menambahkan user");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error: {ex.Message}");
            }
        }
    }
}