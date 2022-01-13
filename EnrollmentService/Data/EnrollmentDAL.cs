using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentService.Data
{
    public class EnrollmentDAL : IEnrollment
    {
        private AppDbContext _db;
        public EnrollmentDAL(AppDbContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            try
            {
                 var result =  await GetById(id);
                 if(result==null) throw new Exception($"Data enrollment {id} tidak ditemukan");
                 _db.Enrollments.Remove(result);
                 await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await (from e in _db.Enrollments orderby e.Grade ascending select e).ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(string id)
        {
            try
            {
                 var enrollments = await _db.Enrollments .Where(e => e.EnrollmentID == Convert.ToInt32(id)).SingleOrDefaultAsync<Enrollment>();
                 if(enrollments==null) throw new Exception($"Data id {id} tidak ditemukan !");
                 return enrollments;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _db.Enrollments.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<Enrollment> Update(string id, Enrollment obj)
        {
            try
            {
                var enrollment = await GetById(id);
                enrollment.CourseID = obj.CourseID;
                enrollment.StudentID = obj.StudentID;
                enrollment.Grade = obj.Grade;
                await _db.SaveChangesAsync();
                obj.EnrollmentID = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }
    }
}