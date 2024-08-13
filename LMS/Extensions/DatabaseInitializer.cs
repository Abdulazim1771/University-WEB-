using Bogus;
using University.Domain.Entities;
using University.Infrastructure;

namespace LMS.Extensions;

public class DatabaseInitializer
{
    //private static readonly Faker _faker = new();

    public static void SeedDatabase()
    {
        using var context = new UniversityDbContext();

        try
        {
            AddStudents(context);
            //AddCourses(context);
            //AddMentors(context);
            //AddMentorCourses(context);
            //AddGroups(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static void AddStudents(UniversityDbContext context)
    {
        if(context.Students.Any())
        {
            return;
        }

        var faker = new Faker<Student>()
            .RuleFor(s => s.FirstName, f => f.Name.FirstName())
            .RuleFor(s => s.LastName, f => f.Name.LastName())
            .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber("+998(##)-###-####"))
            .RuleFor(s => s.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName));

        var students = faker.Generate(100);

        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
