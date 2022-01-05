using System.Collections.Generic;
using System.Threading.Tasks;
using Practice_API_2.Classes;

namespace Practice_API_2.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<Student> DeleteStudent(Student student);
        
        Task<IEnumerable<Subject>> GetSubjects();
        Task<Subject> GetSubject(int id);
        Task<Subject> AddSubject(Subject subject);
        Task<Subject> UpdateSubject(Subject subject);
        Task<Subject> DeleteSubject(Subject subject);
        
        Task<IEnumerable<StudentClass>> GetStudentClasess();
        Task<StudentClass> GetStudentClass(int idStu,int idSub);
        Task<StudentClass> AddStudentClass(StudentClass studentClass);
        Task<StudentClass> UpdateStudentClass(StudentClass studentClass);
        Task<StudentClass> DeleteStudentClass(StudentClass studentClass);
        
    }
}