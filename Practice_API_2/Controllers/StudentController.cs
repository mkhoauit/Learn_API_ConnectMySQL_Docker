using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice_API_2.Classes;
using Practice_API_2.Interfaces;

namespace Practice_API_2.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }
        
        // GET ALL Student
        [HttpGet ("Student/All")]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok(await _studentRepository.GetStudents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // GET  Student by ID
        [HttpGet ("Student/Get/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await _studentRepository.GetStudent(id);
                if (result == null)
                    return NotFound();
                
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // POST Student
        [HttpPost("Student/Post")]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentInputDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var inputStudent = new StudentDto()
                {
                    StudentName = input.StudentName,
                    Gender = input.Gender,
                    DateOfBirth = input.DateOfBirth
                };
                var student = new Student()
                {
                    StudentName = input.StudentName,
                    Gender = input.Gender,
                    DateOfBirth = input.DateOfBirth
                };
                var createdStudent = await _studentRepository.AddStudent(student);
                return CreatedAtAction(nameof(CreateStudent)
                    , new {StudentId = student.StudentId, name = student.StudentName, gender= student.Gender },student);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // PUT Student
        [HttpPut("Student/Put/{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id,StudentDto input)
        {
            try
            {
                if (id != input.StudentId)
                    return BadRequest("StudentId not match");
                var studentToUpdate = await _studentRepository.GetStudent(id);
                if (studentToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");
                
                return await _studentRepository.UpdateStudent(input);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        
    }
}