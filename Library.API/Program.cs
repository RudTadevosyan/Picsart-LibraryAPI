using Library.Infrastructure;
using LibraryAPI.Extensions;
using LibraryAPI.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //DB
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<LibraryDbContext>
            (options => options.UseNpgsql(conn, x => x.MigrationsAssembly("Library.Infrastructure")));
        
        //DI
        builder.Services.AddLibraryDependencies();

        
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}