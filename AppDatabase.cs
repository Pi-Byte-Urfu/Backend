using Backend.Auth.Dal.Models;
using Backend.Courses.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class AppDatabase : DbContext
{
    private IConfiguration _config { get; init; }

    // Auth
    public DbSet<UserModel> Users { get; set; }
    public DbSet<PasswordModel> Passwords { get; set; }
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<TeacherModel> Teachers { get; set; }
    public DbSet<StudentModel> Students { get; set; }

    // Courses
    public DbSet<GroupModel> Groups { get; set; }
    public DbSet<CourseChaptersModel> CourseChaptersModels { get; set; }
    public DbSet<CourseModel> Courses { get; set; }
    public DbSet<GroupCoursesModel> GroupCourses { get; set; }
    public DbSet<StudentGroupsModel> StudentGroups { get; set; }

    public AppDatabase(DbContextOptions<AppDatabase> options, IConfiguration config) : base(options)
    {
        _config = config;
        //Database.EnsureDeleted();
        Database.EnsureCreated();
        try
        {
            Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't migrate for this reason:\n{ex.Message}");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var host = _config.GetValue(typeof(string), "Host")?.ToString();
        var databaseName = _config.GetValue(typeof(string), "DatabaseName")?.ToString();
        var username = _config.GetValue(typeof(string), "Username")?.ToString();
        var password = _config.GetValue(typeof(string), "Password")?.ToString();
        optionsBuilder.UseNpgsql($"Host={host};Database={databaseName};Username={username};Password={password}");
    }
}
