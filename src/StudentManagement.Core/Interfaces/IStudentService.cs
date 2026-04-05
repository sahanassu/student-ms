using StudentManagement.Core.Entities;

namespace StudentManagement.Core.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(Student student);
    Task<Student?> UpdateStudentAsync(int id, Student student);
    Task<bool> DeleteStudentAsync(int id);
}
