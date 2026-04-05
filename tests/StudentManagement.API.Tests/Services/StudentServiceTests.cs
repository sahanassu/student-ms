using Microsoft.EntityFrameworkCore;
using Moq;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Interfaces;
using StudentManagement.API.Services;
using Xunit;

namespace StudentManagement.API.Tests.Services;

public class StudentServiceTests
{
    private readonly Mock<IStudentRepository> _mockRepository;
    private readonly StudentService _studentService;

    public StudentServiceTests()
    {
        _mockRepository = new Mock<IStudentRepository>();
        _studentService = new StudentService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllStudentsAsync_ShouldReturnAllStudents()
    {
        // Arrange
        var students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Age = 22, Course = "Mathematics" }
        };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(students);

        // Act
        var result = await _studentService.GetAllStudentsAsync();

        // Assert
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetStudentByIdAsync_WithValidId_ShouldReturnStudent()
    {
        // Arrange
        var student = new Student { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

        // Act
        var result = await _studentService.GetStudentByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task CreateStudentAsync_WithValidData_ShouldCreateStudent()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        _mockRepository.Setup(r => r.AddAsync(student)).ReturnsAsync(student);

        // Act
        var result = await _studentService.CreateStudentAsync(student);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        _mockRepository.Verify(r => r.AddAsync(student), Times.Once);
    }

    [Fact]
    public async Task CreateStudentAsync_WithInvalidEmail_ShouldThrowArgumentException()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "invalid-email", Age = 20, Course = "Computer Science" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _studentService.CreateStudentAsync(student));
    }

    [Fact]
    public async Task CreateStudentAsync_WithInvalidAge_ShouldThrowArgumentException()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 15, Course = "Computer Science" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _studentService.CreateStudentAsync(student));
    }

    [Fact]
    public async Task UpdateStudentAsync_WithValidId_ShouldUpdateStudent()
    {
        // Arrange
        var existingStudent = new Student { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        var updatedStudent = new Student { Name = "John Updated", Email = "john.updated@example.com", Age = 21, Course = "Data Science" };

        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingStudent);
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Student>())).ReturnsAsync(updatedStudent);

        // Act
        var result = await _studentService.UpdateStudentAsync(1, updatedStudent);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Student>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStudentAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Student?)null);

        // Act
        var result = await _studentService.UpdateStudentAsync(999, student);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Student>()), Times.Never);
    }

    [Fact]
    public async Task DeleteStudentAsync_WithValidId_ShouldDeleteStudent()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _studentService.DeleteStudentAsync(1);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteStudentAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _studentService.DeleteStudentAsync(999);

        // Assert
        Assert.False(result);
        _mockRepository.Verify(r => r.DeleteAsync(999), Times.Once);
    }
}
