using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Data
{
    public interface IEnrollment
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollments();
        Task CreateEnrollemnt(Enrollment enrollment);
    }
}