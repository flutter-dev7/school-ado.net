using System;
using System.Data;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class EnrollmentService : IEnrollmentService
{
    private string connectionString = @"
    Host=localhost;
    Port=5432;
    Username=postgres;
    Database=abdubasir_db;
    Password=Ismoil10";

    public List<Enrollment> GetAllEnrollments()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Enrollments";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            var enrollments = dataSet.Tables[0];

            List<Enrollment> enrollmentList = new();
            foreach (DataRow row in enrollments.Rows)
            {
                Enrollment enrollment = new Enrollment
                {
                    Id = row.Field<int>("Id"),
                    StudentId = row.Field<int>("StudentId"),
                    CourseId = row.Field<int>("CourseId"),
                    EnrolledAt = row.Field<DateTime>("EnrolledAt"),
                    isPaid = row.Field<bool>("IsPaid")
                };
                enrollmentList.Add(enrollment);
            }
            return enrollmentList;
        }
    }

    public void EnrollStudent(int studentId, int courseId)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            INSERT INTO Enrollments (StudentId, CourseId, EnrolledAt, IsPaid)
            VALUES (@studentId, @courseId, @enrolledAt, @isPaid)";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("studentId", studentId);
            command.Parameters.AddWithValue("courseId", courseId);
            command.Parameters.AddWithValue("enrolledAt", DateTime.Now);
            command.Parameters.AddWithValue("isPaid", false);

            command.ExecuteNonQuery();
        }
    }

    public void DeleteEnrollment(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string sql = @"
            DELETE FROM Enrollments
            WHERE Id = @id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}

