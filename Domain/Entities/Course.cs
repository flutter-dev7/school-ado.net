using System;

namespace Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationWeeks { get; set; }
}
