using Backend.Auth.Dal;
using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Logic;
using Backend.Auth.Logic.Interfaces;
using Backend.Base.Services;
using Backend.Base.Services.Filters;
using Backend.Base.Services.Interfaces;
using Backend.Base.Services.ModelBinders;
using Backend.Courses.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
        _services.AddHttpContextAccessor();
        _services.AddControllers(options => options.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider()));
        _services.AddEndpointsApiExplorer();
        //_services.AddSwaggerGen(options => options.OperationFilter<SwaggerAuthorizationHeaderFilter>());
        _services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerAuthorizationHeaderFilter>();
        });
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
        RegisterHelperServices();
        RegisterAuthLogic();
        RegisterCoursesLogic();
    }

    private void RegisterAuthLogic()
    {
        _services.AddScoped<UserService>();
        _services.AddScoped<AccountService>();
    }

    private void RegisterCoursesLogic()
    {
        _services.AddScoped<GroupService>();
        _services.AddScoped<CourseService>();
        _services.AddScoped<CourseChaptersService>();
    }

    private void RegisterHelperServices()
    {
        _services.AddScoped<IPasswordHelperService, PasswordHelperService>();
        _services.AddScoped<IEncryptionService, EncryptionService>();

        _services.AddScoped<IFileManager, StaticFilesManager>();
        _services.AddScoped<IUserIdGetter, UserIdGetter>();
    }
}
