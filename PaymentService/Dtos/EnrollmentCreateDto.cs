using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Dtos
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class EnrollmentCreateDto
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade Grade { get; set; }
    }
}