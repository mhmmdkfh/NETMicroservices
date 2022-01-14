using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Dtos;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
    public interface IUser
    {
        IEnumerable<UserDto> GetAllUser();
        Task Registration(CreateUserDto user);
        Task AddRole(string rolename);
        IEnumerable<CreateRoleDto> GetRoles();
        Task AddUserToRole(string username, string rolename);
        Task<List<string>> GetRolesFromUser(string username);
        Task<User> Authenticate(string username, string password);
    }
}