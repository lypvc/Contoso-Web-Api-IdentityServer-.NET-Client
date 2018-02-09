using ContosoUniversity.Data;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository : EntityBaseRepository<Student>, IStudentsRepository
    {
        private ContosoContext _context;

        public StudentsRepository(ContosoContext context)
            : base(context)
        {
            _context = context;
        }     


        public async Task<Student> GetStudentAsync(int? id)
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);  // instead of SingleOrDefault
        }
    }
}