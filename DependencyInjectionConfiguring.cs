using Backend.DataAccessLayer;
using Backend.DataAccessLayer.Interfaces;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        _services.AddSingleton<IAccountRepo, AccountRepo>();
        _services.AddSingleton<IGroupRepo, GroupRepo>();
        _services.AddSingleton<IUserRepo, UserRepo>();
    }

    private void RegisterBusinessServices()
    {
        _services.AddSingleton<IUserService, UserService>();
    }
}
