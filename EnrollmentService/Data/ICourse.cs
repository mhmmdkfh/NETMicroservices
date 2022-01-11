using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByTitle(string title);
    }
}