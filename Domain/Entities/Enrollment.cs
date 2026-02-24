using System;

namespace Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateOnly EnrollDate { get; set; }
    public bool isPaid { get; set; }
}
