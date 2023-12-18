using Backend.Auth.Dal;
using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Logic;
using Backend.Auth.Logic.Interfaces;
using Backend.Courses.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Logic;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class DependencyInjectionConfiguring
{
    private IServiceCollection _services { get; set; }

    public DependencyInjectionConfiguring(IServiceCollection services)
    {
        _services = services;
    }

    public void RegisterServices()
    {
        RegisterAspServices();
        RegisterMyServices();
    }

    private void RegisterAspServices()
    {
        _services.AddControllers();
        _services.AddEndpointsApiExplorer();
        _services.AddSwaggerGen();
        _services.AddDbContext<AppDatabase>(options => options.UseNpgsql());
    }

    private void RegisterMyServices()
    {
        RegisterDatabaseRepositories();
        RegisterBusinessServices();
    }

    private void RegisterDatabaseRepositories()
    {
        RegisterAuthDbRepos();
        RegisterCoursesDbRepos();
    }

    private void RegisterAuthDbRepos()
    {
        _services.AddScoped<IAccountRepo, AccountRepo>();
        _services.AddScoped<IUserRepo, UserRepo>();
        _services.AddScoped<IStudentRepo, StudentRepo>();
        _services.AddScoped<ITeacherRepo, TeacherRepo>();
        _services.AddScoped<IPasswordRepo, PasswordRepo>();
    }

    private void RegisterCoursesDbRepos()
    {
        _services.AddScoped<IGroupRepo, GroupRepo>();
        _services.AddScoped<ICourseChaptersRepo, CourseChaptersRepo>();
        _services.AddScoped<ICourseRepo, CourseRepo>();
        _services.AddScoped<IGroupCoursesRepo, GroupCoursesRepo>();
        _services.AddScoped<IGroupRepo, GroupRepo>();
        _services.AddScoped<IStudentGroupsRepo, StudentGroupsRepo>();
    }

    private void RegisterBusinessServices()
    {
        RegisterAuthLogic();
        RegisterCoursesLogic();
    }

    private void RegisterAuthLogic()
    {
        _services.AddScoped<UserService>();
        _services.AddScoped<AccountService>();
        
        _services.AddScoped<IPasswordHelperService, PasswordHelperService>();
        _services.AddScoped<IEncryptionService, EncryptionService>();
    }

    private void RegisterCoursesLogic()
    {
        _services.AddScoped<GroupService>();
        _services.AddScoped<CourseService>();
    }
}
