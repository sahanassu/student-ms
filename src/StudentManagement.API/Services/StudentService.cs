using StudentManagement.Core.Entities;
using StudentManagement.Core.Interfaces;

namespace StudentManagement.API.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        // Validate email format
        if (!IsValidEmail(student.Email))
        {
            throw new ArgumentException("Invalid email format");
        }

        // Validate age
        if (student.Age < 16 || student.Age > 100)
        {
            throw new ArgumentException("Age must be between 16 and 100");
        }

        return await _studentRepository.AddAsync(student);
    }

    public async Task<Student?> UpdateStudentAsync(int id, Student student)
    {
        var existingStudent = await _studentRepository.GetByIdAsync(id);
        if (existingStudent == null)
            return null;

        // Validate email format
        if (!IsValidEmail(student.Email))
        {
            throw new ArgumentException("Invalid email format");
        }

        // Validate age
        if (student.Age < 16 || student.Age > 100)
        {
            throw new ArgumentException("Age must be between 16 and 100");
        }

        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;
        existingStudent.Age = student.Age;
        existingStudent.Course = student.Course;

        return await _studentRepository.UpdateAsync(existingStudent);
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        return await _studentRepository.DeleteAsync(id);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
