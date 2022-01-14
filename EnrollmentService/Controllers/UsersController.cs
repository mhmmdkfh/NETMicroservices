using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentService.Controllers
{
    // [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        [AllowAnonymous]
        //Regristrasi user
        [HttpPost]
        public async Task<ActionResult> Registration(CreateUserDto user)
        {
            try
            {
                 await _user.Registration(user);
                 return Ok($"Regristrasi user {user.Username} berhasil");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        //Get All User
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            return Ok(_user.GetAllUser());
        }
        
        [AllowAnonymous]
        //Create Role
        [HttpPost("Role")]
        public async Task<ActionResult> AddRole(CreateRoleDto roleDto)
        {
            try
            {
                await _user.AddRole(roleDto.RoleName);
                return Ok($"Tambah role {roleDto.RoleName} berhasil");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        //Get all role
        [HttpGet("Role")]
        public ActionResult<IEnumerable<CreateRoleDto>> GetAllRole()
        {
            return Ok(_user.GetRoles());
        }

        [AllowAnonymous]
        [HttpPost("UserInRole")]
        public async Task<ActionResult> AddUserToRole([FromBody] CreateUserInRoleDto userInRoleDto)
        {
            try
            {
                await _user.AddUserToRole(userInRoleDto.Username, userInRoleDto.RoleName);
                return Ok($"Berhasil menambahkan user {userInRoleDto.Username} ke role {userInRoleDto.RoleName}");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("RolesByUser/{username}")]
        public async Task<ActionResult<List<string>>> GetRolesByUser(string username)
        {
            var results = await _user.GetRolesFromUser(username);
            return Ok(results); 
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<ActionResult<User>> Authentication(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _user.Authenticate(createUserDto.Username, createUserDto.Password);
                if (user == null)
                    return BadRequest("username/password tidak tepat");
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }   
}