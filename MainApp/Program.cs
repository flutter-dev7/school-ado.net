using Domain.Entities;
using Infrastructure.Services;

CourseService courseService = new CourseService();
EnrollmentService enrollmentService = new EnrollmentService();
StudentService studentService = new StudentService();

while (true)
{
    System.Console.WriteLine();
    Console.WriteLine("========== MAIN MENU ==========");
    Console.WriteLine("1. Students");
    Console.WriteLine("2. Courses");
    Console.WriteLine("3. Enrollments");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            StudentMenu();
            break;
        case "2":
            CourseMenu();
            break;
        case "3":
            EnrollmentMenu();
            break;
        case "0":
            return 0;
    }
}

void StudentMenu()
{
    while (true)
    {
        System.Console.WriteLine();
        Console.WriteLine("====== STUDENT MENU ======");
        Console.WriteLine("1. Get all students");
        Console.WriteLine("2. Get student by id");
        Console.WriteLine("3. Add student");
        Console.WriteLine("4. Update student");
        Console.WriteLine("5. Delete student");
        Console.WriteLine("6. Students without course");
        Console.WriteLine("7. Course student counts");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowAllStudents();
                break;
            case "2":
                ShowStudentById();
                break;
            case "3":
                AddStudent();
                break;
            case "4":
                UpdateStudent();
                break;
            case "5":
                DeleteStudent();
                break;
            case "6":
                ShowStudentsWithoutCourse();
                break;
            case "7":
                ShowCourseStudentCounts();
                break;
            case "0":
                return;
        }
    }

    void ShowAllStudents()
    {
        var students = studentService.GetAllStudents();

        foreach (var item in students)
        {
            Console.WriteLine($"Id: {item.Id}, FullName: {item.FullName}, Email: {item.Email}, BirthDate: {item.BirthDate}");
        }
    }

    void ShowStudentById()
    {
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        var student = studentService.GetStudentById(id);

        Console.WriteLine($"Id: {student.Id}, FullName: {student.FullName}, Email: {student.Email}, BirthDate: {student.BirthDate}");
    }

    void AddStudent()
    {
        Console.Write("FullName: ");
        var name = Console.ReadLine()!;

        Console.Write("Email: ");
        var email = Console.ReadLine()!;

        Console.Write("BirthDate: ");
        var birth = DateOnly.Parse(Console.ReadLine()!);

        Student student = new Student
        {
            FullName = name,
            Email = email,
            BirthDate = birth
        };

        studentService.AddStudent(student);
    }

    void UpdateStudent()
    {
        Console.Write("Id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("FullName: ");
        var name = Console.ReadLine()!;

        Console.Write("Email: ");
        var email = Console.ReadLine()!;

        Console.Write("BirthDate: ");
        var birth = DateOnly.Parse(Console.ReadLine()!);

        Student student = new Student
        {
            Id = id,
            FullName = name,
            Email = email,
            BirthDate = birth
        };

        studentService.UpdateStudent(student);
    }

    void DeleteStudent()
    {
        Console.Write("Id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        studentService.DeleteStudent(id);
    }

    void ShowStudentsWithoutCourse()
    {
        var students = studentService.GetStudentWithoutCourse();

        foreach (var item in students)
        {
            Console.WriteLine($"Id: {item.Id}, FullName: {item.FullName}, Email: {item.Email}, BirthDate: {item.BirthDate}");
        }
    }

    void ShowCourseStudentCounts()
    {
        var students = studentService.GetCourseStudentCounts();

        foreach (var item in students)
        {
            Console.WriteLine($"Title: {item.Title}, Count: {item.Count}");
        }
    }
}

void CourseMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("====== COURSE MENU ======");
        Console.WriteLine("1. Get all courses");
        Console.WriteLine("2. Get course by id");
        Console.WriteLine("3. Add course");
        Console.WriteLine("4. Update course");
        Console.WriteLine("5. Delete course");
        Console.WriteLine("6. Most expensive course");
        Console.WriteLine("7. Average price");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowAllCourses();
                break;
            case "2":
                ShowCourseById();
                break;
            case "3":
                AddCourse();
                break;
            case "4":
                UpdateCourse();
                break;
            case "5":
                DeleteCourse();
                break;
            case "6":
                ShowMostExpensiveCourse();
                break;
            case "7":
                ShowAveragePrice();
                break;
            case "0":
                return;
        }
    }

    void ShowAllCourses()
    {
        var courses = courseService.GetAllCourses();

        foreach (var item in courses)
        {
            Console.WriteLine($"Id: {item.Id}, Title: {item.Title}, Price: {item.Price}, Weeks: {item.DurationWeeks}");
        }
    }

    void ShowCourseById()
    {
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        var course = courseService.GetCourseById(id);

        Console.WriteLine($"Id: {course.Id}, Title: {course.Title}, Price: {course.Price}, Weeks: {course.DurationWeeks}");
    }

    void AddCourse()
    {
        Console.Write("Title: ");
        var title = Console.ReadLine()!;

        Console.Write("Price: ");
        var price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Duration weeks: ");
        var weeks = int.Parse(Console.ReadLine()!);

        courseService.AddCourse(new Course
        {
            Title = title,
            Price = price,
            DurationWeeks = weeks
        });
    }

    void UpdateCourse()
    {
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.Write("Title: ");
        var title = Console.ReadLine()!;

        Console.Write("Price: ");
        var price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Duration weeks: ");
        var weeks = int.Parse(Console.ReadLine()!);

        courseService.UpdateCourse(new Course
        {
            Id = id,
            Title = title,
            Price = price,
            DurationWeeks = weeks
        });
    }

    void DeleteCourse()
    {
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        courseService.DeleteCourse(id);
    }

    void ShowMostExpensiveCourse()
    {
        var course = courseService.GetExpensiveCourse();

        Console.WriteLine($"Title: {course.Title}, Price: {course.Price}");
    }

    void ShowAveragePrice()
    {
        var avg = courseService.GetAveragePriceCourses();

        Console.WriteLine($"Average price: {avg}");
    }
}

void EnrollmentMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("====== ENROLLMENT MENU ======");
        Console.WriteLine("1. Get all enrollments");
        Console.WriteLine("2. Enroll student");
        Console.WriteLine("3. Delete enrollment");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowAllEnrollments();
                break;
            case "2":
                EnrollStudent();
                break;
            case "3":
                DeleteEnrollment();
                break;
            case "0":
                return;
        }
    }

    void ShowAllEnrollments()
    {
        var enrollments = enrollmentService.GetAllEnrollments();

        foreach (var item in enrollments)
        {
            Console.WriteLine($"Id: {item.Id}, StudntId: {item.StudentId}, CourseId: {item.CourseId}, Paid: {item.isPaid}");
        }
    }

    void EnrollStudent()
    {
        Console.Write("StudentId: ");
        int studentId = int.Parse(Console.ReadLine()!);

        Console.Write("CourseId: ");
        int courseId = int.Parse(Console.ReadLine()!);

        enrollmentService.EnrollStudent(studentId, courseId);
    }

    void DeleteEnrollment()
    {
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        enrollmentService.DeleteEnrollment(id);
    }
}