using System;
using System.Data;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class StudentService : IStudentService
{
    private string connectionString = @"
    Host=localhost;
    Port=5432;
    Username=postgres;
    Database=abdubasir_db;
    Password=Ismoil10";

    public List<Student> GetAllStudents()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"SELECT * FROM Students";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            var students = dataSet.Tables[0];

            List<Student> studentList = new();

            foreach (DataRow row in students.Rows)
            {
                Student student = new Student
                {
                    Id = row.Field<int>("Id"),
                    FullName = row.Field<string>("FullName")!,
                    Email = row.Field<string>("Email")!,
                    BirthDate = row.Field<DateOnly>("BirthDate"),
                };
                studentList.Add(student);
            }
            return studentList;
        }
    }

    public Student GetStudentById(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            SELECT * FROM Students
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);
            var reader = command.ExecuteReader();

            reader.Read();

            Student student = new Student
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                BirthDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("BirthDate"))
            };
            return student;

        }
    }

    public List<CourseStudentCount> GetCourseStudentCounts()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            SELECT c.Title, COUNT(e.StudentId)
            FROM Courses AS c
            JOIN Enrollments AS e ON c.Id = e.CourseId
            GROUP BY c.Title";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            List<CourseStudentCount> studentList = new();

            while (reader.Read())
            {
                CourseStudentCount courseStudentCount = new CourseStudentCount
                {
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Count = reader.GetInt32(reader.GetOrdinal("Count"))
                };
                studentList.Add(courseStudentCount);
            }
            return studentList;
        }
    }

    public List<Student> GetStudentWithoutCourse()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            SELECT * FROM Students
            WHERE Id NOT IN (
                SELECT StudentId FROM Enrollments
            )";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            List<Student> students = new();

            while (reader.Read())
            {
                Student student = new Student
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    BirthDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("BirthDate"))
                };
                students.Add(student);
            }
            return students;
        }
    }

    public void AddStudent(Student student)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string sql = @"
            INSERT INTO students (FullName, Email, BirthDate) 
            VALUES (@fullname, @email, @birthdate)";

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@fullname", student.FullName);
            command.Parameters.AddWithValue("@email", student.Email);
            command.Parameters.AddWithValue("@birthdate", student.BirthDate);

            command.ExecuteNonQuery();
        }
    }

    public void UpdateStudent(Student student)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string sql = @"
            UPDATE Students SET 
            FullName = @fullname, 
            Email = @email, 
            BirthDate = @birthdate
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@fullname", student.FullName);
            command.Parameters.AddWithValue("@email", student.Email);
            command.Parameters.AddWithValue("@birthdate", student.BirthDate);
            command.Parameters.AddWithValue("@id", student.Id);

            command.ExecuteNonQuery();
        }
    }

    public void DeleteStudent(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            DELETE FROM Students
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }

}
