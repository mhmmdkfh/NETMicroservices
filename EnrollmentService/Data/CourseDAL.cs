using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentService.Data
{
    public class CourseDAL : ICourse
    {
        private AppDbContext _db;
        public CourseDAL(AppDbContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            try
            {
                var result = await GetById(id);
                if (result == null) throw new Exception($"data course {id} tidak ditemukan");
                _db.Courses.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await (from c in _db.Courses orderby c.Title ascending
                                 select c).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<Course> GetById(string id)
        {
            try
            {
                var result = await (from c in _db.Courses
                                where c.ID == Convert.ToInt32(id)
                                select c).SingleOrDefaultAsync();
                if (result == null) throw new Exception($"data id {id} tidak ditemukan");
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetByTitle(string title)
        {
            var results = await (from c in _db.Courses 
                                 where c.Title.ToLower().Contains(title.ToLower())
                                 orderby c.Title ascending
                                 select c).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _db.Courses.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public async Task<Course> Update(string id, Course obj)
        {
            try
            {
                var result = await GetById(id);
                if (result == null) throw new Exception($"data course id {id} tidak ditemukan");
                result.Title = obj.Title;
                result.Credits = obj.Credits;
                await _db.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }
    }
}