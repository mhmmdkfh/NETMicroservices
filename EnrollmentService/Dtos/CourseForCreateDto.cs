using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Dtos
{
    public class CourseForCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public int Credits { get; set; }
    }
}