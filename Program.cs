using System.Net;
using Backend;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder =>
//        {
//            builder.WithOrigins(
//                    "http://127.0.0.1:5500",
//                    "http://localhost:3000",
//                    "http://5.23.54.98:80",
//                    "http://5.23.54.98:3000")
//                .AllowAnyMethod()
//                .AllowAnyHeader();
//        });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var servicesConfigurator = new DependencyInjectionConfiguring(builder.Services);
servicesConfigurator.RegisterServices();


builder.Configuration.AddJsonFile("./database_settings.json");


var app = builder.Build();

//app.UseCors("AllowSpecificOrigin");
app.UseCors("AllowAll");

app.UseExceptionHandler(options =>
    {
        options.Run(async context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionObject != null)
                {
                    var errorMessage = $"Exception Error: {exceptionObject.Error.Message}";
                    if (app.Environment.IsDevelopment())
                        errorMessage = $"Exception Error: {exceptionObject.Error.Message}\nStackTrace:{exceptionObject.Error.StackTrace}";

                    if (exceptionObject.Error is BadHttpRequestException)
                        context.Response.StatusCode = ((BadHttpRequestException)exceptionObject.Error).StatusCode;
                    else
                        context.Response.StatusCode = 500;

                    await context.Response.WriteAsJsonAsync(new Dictionary<string, string>() { ["error_message"] = errorMessage }).ConfigureAwait(false);
                }
            });
    }
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
