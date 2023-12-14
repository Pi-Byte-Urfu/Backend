using Backend.Dal.Auth;
using Backend.Dal.Auth.Interfaces;
using Backend.Dal.Courses;
using Backend.Dal.Courses.Interfaces;
using Backend.Logic.Auth;
using Backend.Logic.Auth.Interfaces;
using Backend.Logic.Courses;
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
        _services.AddScoped<IAccountRepo, AccountRepo>();
        _services.AddScoped<IGroupRepo, GroupRepo>();
        _services.AddScoped<IUserRepo, UserRepo>();
        _services.AddScoped<IStudentRepo, StudentRepo>();
        _services.AddScoped<ITeacherRepo, TeacherRepo>();
        _services.AddScoped<IPasswordRepo, PasswordRepo>();
    }

    private void RegisterBusinessServices()
    {
        _services.AddScoped<UserService>();
        _services.AddScoped<AccountService>();
        _services.AddScoped<GroupService>();

        _services.AddScoped<IPasswordHelperService, PasswordHelperService>();
        _services.AddScoped<IEncryptionService, EncryptionService>();
    }
}
