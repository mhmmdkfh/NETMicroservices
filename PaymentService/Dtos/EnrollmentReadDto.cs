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
    public class EnrollmentReadDto
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
    }
}