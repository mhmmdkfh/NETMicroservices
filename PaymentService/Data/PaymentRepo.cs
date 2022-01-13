using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext _context;

        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateEnrollment(Enrollment plat)
        {
            if(plat==null)
                throw new ArgumentNullException(nameof(plat));
            _context.Enrollments.Add(plat);
        }

        public bool EnrollmentExist(int enrollmentid)
        {
            return _context.Enrollments.Any(e => e.Id==enrollmentid);
        }

        public bool ExternalEnrollmentExist(int externalEnrollmentId)
        {
            return _context.Enrollments.Any(e=>e.EnrollmentID==externalEnrollmentId); 
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments.ToList();
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            _context.Enrollments.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }
    }
}