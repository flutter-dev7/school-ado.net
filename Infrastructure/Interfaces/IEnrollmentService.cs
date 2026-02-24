using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IEnrollmentService
{
    void EnrollStudent(int studentId, int courseId);
    List<Enrollment> GetAllEnrollments();
    void DeleteEnrollment(int id);
}
