using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Models
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class Enrollment
    {
        public int Id { get; set; }
        public int EnrollmentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public Grade? Grade { get; set; }
    }
}