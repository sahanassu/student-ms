using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManagement.API.Controllers;
using StudentManagement.API.Dtos;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Interfaces;
using Xunit;

namespace StudentManagement.API.Tests.Controllers;

public class StudentsControllerTests
{
    private readonly Mock<IStudentService> _mockStudentService;
    private readonly Mock<ILogger<StudentsController>> _mockLogger;
    private readonly StudentsController _controller;

    public StudentsControllerTests()
    {
        _mockStudentService = new Mock<IStudentService>();
        _mockLogger = new Mock<ILogger<StudentsController>>();
        _controller = new StudentsController(_mockStudentService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetStudents_ShouldReturnOkResult_WithStudents()
    {
        // Arrange
        var students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science", CreatedDate = DateTime.UtcNow },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Age = 22, Course = "Mathematics", CreatedDate = DateTime.UtcNow }
        };

        _mockStudentService.Setup(s => s.GetAllStudentsAsync()).ReturnsAsync(students);

        // Act
        var result = await _controller.GetStudents();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStudents = Assert.IsAssignableFrom<IEnumerable<StudentDto>>(okResult.Value);
        Assert.Equal(2, returnedStudents.Count());
    }

    [Fact]
    public async Task GetStudent_WithValidId_ShouldReturnOkResult()
    {
        // Arrange
        var student = new Student { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science", CreatedDate = DateTime.UtcNow };
        _mockStudentService.Setup(s => s.GetStudentByIdAsync(1)).ReturnsAsync(student);

        // Act
        var result = await _controller.GetStudent(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStudent = Assert.IsType<StudentDto>(okResult.Value);
        Assert.Equal("John Doe", returnedStudent.Name);
    }

    [Fact]
    public async Task GetStudent_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        _mockStudentService.Setup(s => s.GetStudentByIdAsync(999)).ReturnsAsync((Student?)null);

        // Act
        var result = await _controller.GetStudent(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateStudent_WithValidData_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var createStudentDto = new CreateStudentDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Age = 20,
            Course = "Computer Science"
        };

        var student = new Student
        {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            Age = 20,
            Course = "Computer Science",
            CreatedDate = DateTime.UtcNow
        };

        _mockStudentService.Setup(s => s.CreateStudentAsync(It.IsAny<Student>())).ReturnsAsync(student);

        // Act
        var result = await _controller.CreateStudent(createStudentDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedStudent = Assert.IsType<StudentDto>(createdAtActionResult.Value);
        Assert.Equal("John Doe", returnedStudent.Name);
        Assert.Equal(1, returnedStudent.Id);
    }

    [Fact]
    public async Task UpdateStudent_WithValidId_ShouldReturnOkResult()
    {
        // Arrange
        var updateStudentDto = new UpdateStudentDto
        {
            Name = "John Updated",
            Email = "john.updated@example.com",
            Age = 21,
            Course = "Data Science"
        };

        var updatedStudent = new Student
        {
            Id = 1,
            Name = "John Updated",
            Email = "john.updated@example.com",
            Age = 21,
            Course = "Data Science",
            CreatedDate = DateTime.UtcNow
        };

        _mockStudentService.Setup(s => s.UpdateStudentAsync(1, It.IsAny<Student>())).ReturnsAsync(updatedStudent);

        // Act
        var result = await _controller.UpdateStudent(1, updateStudentDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStudent = Assert.IsType<StudentDto>(okResult.Value);
        Assert.Equal("John Updated", returnedStudent.Name);
    }

    [Fact]
    public async Task UpdateStudent_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var updateStudentDto = new UpdateStudentDto
        {
            Name = "John Updated",
            Email = "john.updated@example.com",
            Age = 21,
            Course = "Data Science"
        };

        _mockStudentService.Setup(s => s.UpdateStudentAsync(999, It.IsAny<Student>())).ReturnsAsync((Student?)null);

        // Act
        var result = await _controller.UpdateStudent(999, updateStudentDto);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteStudent_WithValidId_ShouldReturnNoContent()
    {
        // Arrange
        _mockStudentService.Setup(s => s.DeleteStudentAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteStudent(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteStudent_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        _mockStudentService.Setup(s => s.DeleteStudentAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteStudent(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
