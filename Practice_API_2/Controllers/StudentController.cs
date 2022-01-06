using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            _studentRepository = studentRepository;
        }
        
        // GET ALL Student
        [HttpGet ("Student/All")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return Ok(await _studentRepository.GetStudents());
        }
        // GET  Student by ID
        [HttpGet ("Student/Get/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var result = await _studentRepository.GetStudent(id);
            if (result == null)
                    return NotFound();
                
            return result;

        }
        // POST Student
        [HttpPost("Student/Post")]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentInputDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var createdStudent = await _studentRepository.AddStudent(input);

                return CreatedAtAction(nameof(CreateStudent)
                    , new {StudentId = createdStudent.StudentId, name = createdStudent.StudentName, gender= createdStudent.Gender },createdStudent);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // PUT Student
        [HttpPut("Student/Put/{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id,StudentInputDto input)
        {
            try
            {
                var studentToUpdate = await _studentRepository.GetStudent(id);
                if (studentToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");
                var update = await _studentRepository.UpdateStudent(id,input);
                return Ok($"Successfully update Student with StudentId: {id}; name: {input.StudentName}; date of birth: {input.DateOfBirth}; gender:{input.Gender}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // DELETE Student
        [HttpDelete("Student/Delete/{id}")]
        public async Task<ActionResult<StudentDto>> SoftDeleteStudent(int id)
        {
            try
            {
                var studentToUpdate = await _studentRepository.GetStudent(id);
                if (studentToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");
                var softDelete = await _studentRepository.DeleteStudent(id);
                return Ok($"Successfully softDelete Student with StudentId {id} with new status isDeleted: true");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        
        // GET ALL Subject
        [HttpGet ("Subject/All")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return Ok(await _studentRepository.GetSubjects());
        }
        
        // GET  Subject by ID
        [HttpGet ("Subject/Get/{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            var result = await _studentRepository.GetSubject(id);
            if (result == null)
                    return NotFound();
                
            return result;
        }
        
        // POST Subject
        [HttpPost("Subject/Post")]
        public async Task<ActionResult<SubjectDto>> CreateSubject(SubjectInputDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var createdSubject = await _studentRepository.AddSubject(input);

                return CreatedAtAction(nameof(CreateSubject)
                    , new {SubjectId = createdSubject.SubjectId, SubjectName = createdSubject.SubjectName, TeacherName= createdSubject.Teacher},createdSubject);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // PUT Subject
        [HttpPut("Subject/Put/{id}")]
        public async Task<ActionResult<SubjectDto>> UpdateSubject(int id,SubjectInputDto input)
        {
            try
            {
                var subjectToUpdate = await _studentRepository.GetSubject(id);
                if (subjectToUpdate == null)
                    return NotFound($"Subject with Id = {id} not found");
                var update = await _studentRepository.UpdateSubject(id,input);
                return Ok($"Successfully update Subject with SubjectId: {id}; SubjectName: {input.SubjectName}; TeacherName: {input.Teacher}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // DELETE Subject
        [HttpDelete("Subject/Delete/{id}")]
        public async Task<ActionResult<SubjectDto>> SoftDeleteSubject(int id)
        {
            try
            {
                var subjectToUpdate = await _studentRepository.GetSubject(id);
                if (subjectToUpdate == null)
                    return NotFound($"Subject with Id = {id} not found");
                var softDelete = await _studentRepository.DeleteSubject(id);
                return Ok($"Successfully softDelete Subject with SubjectId {id} with new status isDeleted: true");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        
         // GET ALL StudentClass
        [HttpGet ("StudentClass/All")]
        public async Task<ActionResult<IEnumerable<StudentClass>>> GetStudentClases()
        {
            return Ok(await _studentRepository.GetStudentClases());
        }
        
        // GET  StudentClass by ID
        [HttpGet ("StudentClass/Get/{idStu}/{idSub}")]
        public async Task<ActionResult<StudentClass>> GetStudentClass(int idStu, int idSub)
        {
            var result = await _studentRepository.GetStudentClass(idStu,idSub);
            if (result == null)
                    return NotFound();
                
            return result;
        }
        
        // POST StudentClass
        [HttpPost("StudentClass/Post")]
        public async Task<ActionResult<StudentClassDto>> CreateStudentClass(StudentClassDto input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var checkidStu = await _studentRepository.GetStudent(input.StudentId);
                var checkidSub = await _studentRepository.GetSubject(input.SubjectId);

                if (checkidStu == null || checkidSub == null)
                {
                    if (checkidSub == null && checkidStu != null)
                    {
                        return NotFound($"Not Found SubjectId: {input.SubjectId}");
                    }

                    if (checkidStu == null && checkidSub != null)
                    {
                        return NotFound($"Not Found StudentId: {input.StudentId}");
                    }
                    if(checkidSub == null && checkidSub == null)
                        return NotFound("Not Found in both Student and Subject");
                }
                var createdStudentclass = await _studentRepository.AddStudentClass(input);

                return CreatedAtAction(nameof(CreateStudentClass)
                    , new {StudentId=input.StudentId,SubjectId=input.SubjectId,Start=input.DateTimeStart,End=input.DateTimeEnd},
                    input);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // PUT StudentClass
        [HttpPut("StudentClass/Put/{idStu}/{idSub}")]
        public async Task<ActionResult<StudentClassDto>> UpdateStudentClass(int idStu,int idSub,StudentClassInputDto input)
        {
            try
            {
                var studentclassToUpdate = await _studentRepository.GetStudentClass(idStu,idSub);
                if (studentclassToUpdate == null)
                    return NotFound($"StudentClass with StudentId: {idStu}; SubjectId:{idSub} not found");
                var update = await _studentRepository.UpdateStudentClass(idStu,idSub,input);
                return Ok($"Successfully update StudentClass with StudentId:{idStu};SubjectId:{idSub};dateStart:{input.DateTimeStart};dateEnd:{input.DateTimeEnd}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        // DELETE StudentClass
        [HttpDelete("StudentClass/Delete/{idStu}/{idSub}")]
        public async Task<ActionResult<StudentClass>> SoftDeleteStudentClass(int idStu,int idSub)
        {
            try
            {
                var studentclassToUpdate = await _studentRepository.GetStudentClass(idStu,idSub);
                if (studentclassToUpdate == null)
                    return NotFound($"StudentClass with StudentId: {idStu}; SubjectId:{idSub} Not Found");
                var softDelete = await _studentRepository.DeleteStudentClass(idStu,idSub);
                return Ok($"Successfully softDelete StudentClass with StudentId: {idStu}; SubjectId: {idSub} with new status isDeleted: true");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }
        
    }
}
