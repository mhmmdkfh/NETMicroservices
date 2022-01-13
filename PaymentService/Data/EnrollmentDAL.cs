using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly AppDbContext _db;

        public EnrollmentDAL(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateEnrollemnt(Enrollment enrollment)
        {
            if(enrollment == null) throw new ArgumentNullException(nameof(enrollment));
            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            var results = await (from e in _db.Enrollments orderby e.Grade ascending select e).ToListAsync();
            return results;
        }
    }
}