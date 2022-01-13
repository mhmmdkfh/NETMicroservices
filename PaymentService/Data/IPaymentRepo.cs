using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Data
{
    public interface IPaymentRepo
    {
        bool SaveChanges();

        //Enrollments
        IEnumerable<Enrollment> GetAllEnrollments();
        Task<Enrollment> Insert(Enrollment obj);
        void CreateEnrollment(Enrollment plat);
        bool EnrollmentExist(int enrollmentid);
        bool ExternalEnrollmentExist(int externalEnrollmentId);
    }
}