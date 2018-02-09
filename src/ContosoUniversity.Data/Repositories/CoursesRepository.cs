using ContosoUniversity.Data;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Data.Repositories
{
    public class CoursesRepository : EntityBaseRepository<Course>, ICoursesRepository
    {
        private ContosoContext _context;

        public CoursesRepository(ContosoContext context)
            : base(context)
        {
            _context = context;
        }      

        public IEnumerable<Department> PopulateDepartmentDropdownList()
        {          
            return _context.Departments.OrderBy(d => d.Name);
        }
    }
}