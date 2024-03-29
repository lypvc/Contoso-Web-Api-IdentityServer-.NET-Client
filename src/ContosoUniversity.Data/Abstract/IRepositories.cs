﻿using ContosoUniversity.Model.Entities;
using ContosoUniversity.Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Abstract
{
    
    public interface IStudentsRepository : IEntityBaseRepository<Student>
    {
        Task<Student> GetStudentAsync(int? id);
    }

    public interface ICoursesRepository : IEntityBaseRepository<Course>
    {
        IEnumerable<Department> PopulateDepartmentDropdownList();
    }

    public interface IInstructorsRepository : IEntityBaseRepository<Instructor>
    {
        IEnumerable<Instructor> GetAllInstructors();
        Task<Instructor> GetInstructorCourses(int id);
        Task<Course> GetEnrolledStudents(int courseId);
        List<AssignedCourseViewModel> GetAssignedCourses(Instructor instructor);
        void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate);
        bool CheckOfficeExist(int id, string office);
    }

    public interface IDepartmentsRepository : IEntityBaseRepository<Department> { }

    public interface IEnrollmentRepository : IEntityBaseRepository<Enrollment> { }

  
}