using System.Collections.Generic;
using System.Threading.Tasks;
using Practice_API_2.Classes;

namespace Practice_API_2.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<Student> AddStudent(StudentInputDto student);
        Task<Student> UpdateStudent(int id,StudentInputDto student);
        Task<Student> DeleteStudent(int id);
        
        Task<IEnumerable<Subject>> GetSubjects();
        Task<Subject> GetSubject(int id);
        Task<Subject> AddSubject(SubjectInputDto subject);
        Task<Subject> UpdateSubject(int id, SubjectInputDto subject);
        Task<Subject> DeleteSubject(int id);
        
        Task<IEnumerable<StudentClass>> GetStudentClasses();
        Task<StudentClass> GetStudentClass(int idStu,int idSub);
        Task<StudentClass> AddStudentClass(StudentClassDto studentClass);
        Task<StudentClass> UpdateStudentClass(int idStu, int idSub, StudentClassInputDto studentClass);
        Task<StudentClass> DeleteStudentClass(int idStu, int idSub);
        
    }
}