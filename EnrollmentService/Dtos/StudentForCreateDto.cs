using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.ValidationAttributes;

namespace EnrollmentService.Dtos
{
    [StudentFirstLastMustBeDifferentAttribute]
    public class StudentForCreateDto
    {
        [Required(ErrorMessage ="FirstName tidak boleh kosong")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName tidak boleh kosong")]
        public string LastName { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}