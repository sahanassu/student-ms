using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Dtos;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Interfaces;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
    {
        _logger.LogInformation("Getting all students");
        var students = await _studentService.GetAllStudentsAsync();
        
        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Age = s.Age,
            Course = s.Course,
            CreatedDate = s.CreatedDate
        });

        return Ok(studentDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        _logger.LogInformation("Getting student with ID: {Id}", id);
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            _logger.LogWarning("Student with ID: {Id} not found", id);
            return NotFound();
        }

        var studentDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            Course = student.Course,
            CreatedDate = student.CreatedDate
        };

        return Ok(studentDto);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto createStudentDto)
    {
        _logger.LogInformation("Creating new student: {Name}", createStudentDto.Name);

        var student = new Student
        {
            Name = createStudentDto.Name,
            Email = createStudentDto.Email,
            Age = createStudentDto.Age,
            Course = createStudentDto.Course
        };

        var createdStudent = await _studentService.CreateStudentAsync(student);

        var studentDto = new StudentDto
        {
            Id = createdStudent.Id,
            Name = createdStudent.Name,
            Email = createdStudent.Email,
            Age = createdStudent.Age,
            Course = createdStudent.Course,
            CreatedDate = createdStudent.CreatedDate
        };

        return CreatedAtAction(nameof(GetStudent), new { id = studentDto.Id }, studentDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
    {
        _logger.LogInformation("Updating student with ID: {Id}", id);

        var student = new Student
        {
            Name = updateStudentDto.Name,
            Email = updateStudentDto.Email,
            Age = updateStudentDto.Age,
            Course = updateStudentDto.Course
        };

        var updatedStudent = await _studentService.UpdateStudentAsync(id, student);

        if (updatedStudent == null)
        {
            _logger.LogWarning("Student with ID: {Id} not found for update", id);
            return NotFound();
        }

        var studentDto = new StudentDto
        {
            Id = updatedStudent.Id,
            Name = updatedStudent.Name,
            Email = updatedStudent.Email,
            Age = updatedStudent.Age,
            Course = updatedStudent.Course,
            CreatedDate = updatedStudent.CreatedDate
        };

        return Ok(studentDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        _logger.LogInformation("Deleting student with ID: {Id}", id);

        var result = await _studentService.DeleteStudentAsync(id);

        if (!result)
        {
            _logger.LogWarning("Student with ID: {Id} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
