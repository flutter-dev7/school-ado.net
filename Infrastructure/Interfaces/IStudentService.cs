using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    void AddStudent(Student student);
    List<Student> GetAllStudents();
    Student GetStudentById(int id);
    void UpdateStudent(Student student);
    void DeleteStudent(int id);
}
