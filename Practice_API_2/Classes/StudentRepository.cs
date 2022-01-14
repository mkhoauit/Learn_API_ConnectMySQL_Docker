using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_API_2.Interfaces;

namespace Practice_API_2.Classes
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext _context;

        public StudentRepository(StudentContext context)
        {
            this._context = context;
        }


        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s=>s.StudentId == id);
        }

        public async Task<Student> AddStudent(StudentInputDto input)
        {
            var student = new Student()
            {
                StudentName = input.StudentName,
                Gender = input.Gender,
                DateOfBirth = input.DateOfBirth
            };
            var result = await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return result.Entity;
            
        }

        public async Task<Student> UpdateStudent(int id, StudentInputDto student)
        {
            var check = await _context.Students.FirstOrDefaultAsync(c => c.StudentId == id);
           
            if (check == null)
                return null;
            
            check.StudentName = student.StudentName;
            check.Gender = student.Gender;
            check.DateOfBirth = student.DateOfBirth;
            await _context.SaveChangesAsync();
            return check;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var result = await _context.Students
                .FirstOrDefaultAsync(e => e.StudentId == id);
            if (result != null && result.isDeleted != true)
            {
                result.isDeleted = true;
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubject(int id)
        {
            return await  _context.Subjects.FirstOrDefaultAsync(s=>s.SubjectId == id);
        }

        public async Task<Subject> AddSubject(SubjectInputDto inputDto)
        {
            var subject = new Subject()
            {
                SubjectName = inputDto.SubjectName,
                Teacher = inputDto.Teacher
            };
            var result = await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Subject> UpdateSubject(int id, SubjectInputDto subject)
        {
            var result = await _context.Subjects.FirstOrDefaultAsync(s=>s.SubjectId == id);
            if (result == null)
                return null;
            
            result.SubjectName = subject.SubjectName;
            result.Teacher = subject.Teacher;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Subject> DeleteSubject(int id)
        {
           
            var result = await _context.Subjects
                .FirstOrDefaultAsync(e => e.SubjectId == id);
            if (result != null && result.isDeleted != true)
            {
                result.isDeleted = true;
                await _context.SaveChangesAsync();
            }
            return null;
        }

        
        public async Task<IEnumerable<StudentClass>> GetStudentClases()
        {
            return await _context.StudentClasses.ToListAsync();
        }

        public async Task<StudentClass> GetStudentClass(int idStu, int idSub)
        {
            var result = _context.StudentClasses.Where(p => p.StudentId == idStu)
                .FirstOrDefault<StudentClass>(o => o.SubjectId == idSub);
            if (result == null)
                return null;
            
            return result;
        }

        public async Task<StudentClass> AddStudentClass(StudentClassDto inputDto)
        {
            var studentClass = new StudentClass()
            {
                StudentId = inputDto.StudentId,
                SubjectId = inputDto.SubjectId,
                DateTimeStart = inputDto.DateTimeStart,
                DateTimeEnd = inputDto.DateTimeEnd
            };
            var result = await _context.StudentClasses.AddAsync(studentClass);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<StudentClass> UpdateStudentClass(int idStu, int idSub, StudentClassInputDto studentClass)
        {
            
            var result = await _context.StudentClasses.Where(o=>o.StudentId== idStu)
                .FirstOrDefaultAsync<StudentClass>(s=>s.SubjectId == idSub);
            if (result == null)
                return null;
            result.DateTimeStart = studentClass.DateTimeStart;
            result.DateTimeEnd = studentClass.DateTimeEnd;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<StudentClass> DeleteStudentClass(int idStu, int idSub)
        {
            var result = await _context.StudentClasses.Where(o=>o.StudentId== idStu)
                .FirstOrDefaultAsync<StudentClass>(s=>s.SubjectId == idSub);
            if (result != null)
            {
                result.isDeleted = true;
                await _context.SaveChangesAsync();
            }
            return null;
        }
    }
}