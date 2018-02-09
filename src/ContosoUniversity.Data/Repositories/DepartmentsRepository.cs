using ContosoUniversity.Data;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Model.Entities;

namespace ContosoUniversity.Data.Repositories
{
    public class DepartmentsRepository : EntityBaseRepository<Department>, IDepartmentsRepository
    {
        private ContosoContext _context;

        public DepartmentsRepository(ContosoContext context)
            : base(context)
        {
            _context = context;
        }  
    }
}