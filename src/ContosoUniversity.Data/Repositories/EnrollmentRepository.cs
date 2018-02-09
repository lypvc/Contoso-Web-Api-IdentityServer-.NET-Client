using ContosoUniversity.Data;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Model.Entities;

namespace ContosoUniversity.Data.Repositories
{
    public class EnrollmentRepository : EntityBaseRepository<Enrollment>, IEnrollmentRepository
    {
        private ContosoContext _context;

        public EnrollmentRepository(ContosoContext context)
            : base(context)
        {
            _context = context;
        }
    }
}