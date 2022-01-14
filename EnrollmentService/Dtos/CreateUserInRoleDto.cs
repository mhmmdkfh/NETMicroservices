using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Dtos
{
    public class CreateUserInRoleDto
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }
}