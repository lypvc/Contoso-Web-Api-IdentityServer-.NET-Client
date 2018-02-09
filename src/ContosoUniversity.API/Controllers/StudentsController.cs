using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Model.ViewModels;
using System.Collections.Generic;
using System;
using System.Linq;
using ContosoUniversity.API.Core;
using ContosoUniversity.Model.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        int page = 1;
        int pageSize = 4;

        public StudentsController(IStudentsRepository studentsRepository,
                                  IEnrollmentRepository enrollmentRepository)
        {
            _studentsRepository = studentsRepository;
            _enrollmentRepository = enrollmentRepository;
        }


        public async Task<IActionResult> Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalStudents = await _studentsRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalStudents / pageSize);

            IEnumerable<Student> students = _studentsRepository
                .GetAll()
                .OrderBy(s => s.ID)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalStudents, totalPages);

            IEnumerable<StudentViewModel> studentsVM = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(students);

            return new OkObjectResult(studentsVM);
        }


        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return BadRequest("invalid input data");

            Student student = await _studentsRepository.GetStudentAsync(id);           

            if (student != null)
            {
                StudentDetailsViewModel studentDetailsVM = Mapper.Map<Student, StudentDetailsViewModel>(student);

                return new OkObjectResult(studentDetailsVM);
            }                
            else
                return NotFound("Could not find resource");
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Student newStudent = new Student
            {
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate
            };

            await _studentsRepository.AddAsync(newStudent);
            await _studentsRepository.CommitAsync();
                     
            student = Mapper.Map<Student, StudentViewModel>(newStudent);
         
            return CreatedAtRoute("GetStudent", new { controller = "Students", id = student.ID }, student);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            Student updateStudent = _studentsRepository.GetSingle(s => s.ID == id);

            if (updateStudent == null)
                return NotFound("The Student to be updated was not found");
            else
            {
                updateStudent.LastName = student.LastName;
                updateStudent.FirstMidName = student.FirstMidName;
                updateStudent.EnrollmentDate = student.EnrollmentDate;

                await _studentsRepository.CommitAsync();   
            }


            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new ObjectResult("Student updated successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteStudent = _studentsRepository.GetSingle(s => s.ID == id);

            if (deleteStudent == null)
                return new NotFoundResult();
            else
            {
                IEnumerable<Enrollment> enrollments = _enrollmentRepository.FindBy(e => e.StudentID == id);

                foreach (var enrollment in enrollments)
                    _enrollmentRepository.Delete(enrollment);

                _studentsRepository.Delete(deleteStudent);

                await _studentsRepository.CommitAsync();

                return new NoContentResult();
            }
        }

    }

}