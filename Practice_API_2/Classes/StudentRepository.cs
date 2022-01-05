using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Student> AddStudent(Student student)
        {
            var result = await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return result.Entity;
            
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var result = await _context.Students.FirstOrDefaultAsync(s=>s.StudentId == student.StudentId);
            if (result == null)
                return null;
            result.StudentName = student.StudentName;
            result.Gender = student.Gender;
            result.DateOfBirth = student.DateOfBirth;
            await _context.SaveChangesAsync();
            return result;

        }

        public async Task<Student> DeleteStudent(Student student)
        {
            var result = await _context.Students
                .FirstOrDefaultAsync(e => e.StudentId == student.StudentId);
            if (result != null)
            {
                result.isDeleted = student.isDeleted;
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
            return await _context.Subjects.FirstOrDefaultAsync(s=>s.SubjectId == id);
        }

        public async Task<Subject> AddSubject(Subject subject)
        {
            var result = await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Subject> UpdateSubject(Subject subject)
        {
            var result = await _context.Subjects.FirstOrDefaultAsync(s=>s.SubjectId == subject.SubjectId);
            if (result == null)
                return null;
            result.SubjectName = subject.SubjectName;
           
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Subject> DeleteSubject(Subject subject)
        {
            var result = await _context.Subjects
                .FirstOrDefaultAsync(e => e.SubjectId == subject.SubjectId);
            if (result != null)
            {
                result.isDeleted = subject.isDeleted;
                await _context.SaveChangesAsync();
            }
            return null;
        }

        
        public async Task<IEnumerable<StudentClass>> GetStudentClasess()
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

        public async Task<StudentClass> AddStudentClass(StudentClass studentClass)
        {
            var result = await _context.StudentClasses.AddAsync(studentClass);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<StudentClass> UpdateStudentClass(StudentClass studentClass)
        {
            
            var result = await _context.StudentClasses.Where(o=>o.StudentId==studentClass.StudentId)
                .FirstOrDefaultAsync<StudentClass>(s=>s.SubjectId == studentClass.SubjectId);
            if (result == null)
                return null;
            result.DateTimeStart = studentClass.DateTimeStart;
            result.DateTimeEnd = studentClass.DateTimeEnd;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<StudentClass> DeleteStudentClass(StudentClass studentClass)
        {
            var result = await _context.StudentClasses.Where(o=>o.StudentId==studentClass.StudentId)
                .FirstOrDefaultAsync<StudentClass>(s=>s.SubjectId == studentClass.SubjectId);
            if (result != null)
            {
                result.isDeleted = studentClass.isDeleted;
                await _context.SaveChangesAsync();
            }

            return null;
        }
    }
}