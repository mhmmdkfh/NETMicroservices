using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
    public class StudentDAL : IStudent
    {
        private AppDbContext _db;
        public StudentDAL(AppDbContext db)
        {
            _db = db;
        }

        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result == null) throw new Exception("Data tidak ditemukan !");
            try
            {
                _db.Students.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await (from s in _db.Students
                                 orderby s.FirstName ascending
                                select s).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(string id)
        {
            try
            {
                 var result = await _db.Students.Where(s => s.ID == Convert.ToInt32(id)).SingleOrDefaultAsync<Student>();
                 if(result==null) throw new Exception($"Data id {id} tidak ditemukan !");
                 return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
                
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _db.Students.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<Student> Update(string id, Student obj)
        {
            try
            {
                var result = await GetById(id);
                result.FirstName = obj.FirstName;
                result.LastName = obj.LastName;
                result.EnrollmentDate=obj.EnrollmentDate;
                await _db.SaveChangesAsync();
                obj.ID = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
    }
}