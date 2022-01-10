using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Models
{
    public class Course
    {
        public int ID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}