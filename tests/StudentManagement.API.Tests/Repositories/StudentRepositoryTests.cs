using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StudentManagement.Core.Entities;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Infrastructure.Repositories;
using Xunit;

namespace StudentManagement.API.Tests.Repositories;

public class StudentRepositoryTests : IDisposable
{
    private readonly StudentDbContext _context;
    private readonly StudentRepository _repository;

    public StudentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemory(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new StudentDbContext(options);
        _repository = new StudentRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllStudents()
    {
        // Arrange
        await _context.Students.AddRangeAsync(
            new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" },
            new Student { Name = "Jane Smith", Email = "jane@example.com", Age = 22, Course = "Mathematics" }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnStudent()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(student.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddStudent()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };

        // Act
        var result = await _repository.AddAsync(student);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        
        var savedStudent = await _context.Students.FindAsync(result.Id);
        Assert.NotNull(savedStudent);
        Assert.Equal("John Doe", savedStudent.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateStudent()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        student.Name = "John Updated";
        student.Age = 21;

        // Act
        var result = await _repository.UpdateAsync(student);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Updated", result.Name);
        Assert.Equal(21, result.Age);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldDeleteStudent()
    {
        // Arrange
        var student = new Student { Name = "John Doe", Email = "john@example.com", Age = 20, Course = "Computer Science" };
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteAsync(student.Id);

        // Assert
        Assert.True(result);
        
        var deletedStudent = await _context.Students.FindAsync(student.Id);
        Assert.Null(deletedStudent);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var result = await _repository.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
