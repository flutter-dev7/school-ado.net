using System;
using System.Data;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class CourseService : ICourseService
{
    private string connectionString = @"
    Host=localhost;
    Port=5432;
    Username=postgres;
    Database=abdubasir_db;
    Password=Ismoil10";

    public List<Course> GetAllCourses()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Courses";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            var courses = dataSet.Tables[0];

            List<Course> courseList = new();
            foreach (DataRow row in courses.Rows)
            {
                Course course = new Course
                {
                    Id = row.Field<int>("Id"),
                    Title = row.Field<string>("Title")!,
                    Price = row.Field<decimal>("Price"),
                    DurationWeeks = row.Field<int>("DurationWeeks")
                };
                courseList.Add(course);
            }
            return courseList;
        }
    }

    public Course GetCourseById(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            SELECT * FROM Courses
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);
            var reader = command.ExecuteReader();

            reader.Read();

            Course course = new Course
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                DurationWeeks = reader.GetInt32(reader.GetOrdinal("DurationWeeks"))
            };
            return course;

        }
    }

    public Course GetExpensiveCourse()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            SELECT * FROM Courses
            WHERE Price = (
                SELECT MAX(Price) FROM Courses
            )";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            reader.Read();

            Course course = new Course
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                DurationWeeks = reader.GetInt32(reader.GetOrdinal("DurationWeeks"))
            };

            return course;
        }
    }

    public decimal GetAveragePriceCourses()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            
            string sql = @"
            SELECT AVG(c.Price) FROM Courses AS c
            JOIN Enrollments AS e ON c.Id = e.CourseId";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            var result = command.ExecuteScalar();
            return Convert.ToDecimal(result);
        }
    }

    public void AddCourse(Course course)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            INSERT INTO Courses (Title, Price, DurationWeeks)
            VALUES (@title, @price, @durationWeeks)";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("title", course.Title);
            command.Parameters.AddWithValue("price", course.Price);
            command.Parameters.AddWithValue("durationWeeks", course.DurationWeeks);

            command.ExecuteNonQuery();
        }
    }

    public void UpdateCourse(Course course)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            UPDATE Courses SET
            Title = @title,
            Price = @price,
            DurationWeeks = @durationWeeks
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("title", course.Title);
            command.Parameters.AddWithValue("price", course.Price);
            command.Parameters.AddWithValue("durationWeeks", course.DurationWeeks);
            command.Parameters.AddWithValue("Id", course.Id);

            command.ExecuteNonQuery();
        }
    }

    public void DeleteCourse(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            DELETE FROM Courses
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
