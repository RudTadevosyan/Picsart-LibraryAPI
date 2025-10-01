using System.Text;
using FluentValidation;
using Library.Infrastructure;
using Library.Shared.Validators;
using LibraryAPI.Extensions;
using LibraryAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LibraryAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //DB
        var connLibraryDb = builder.Configuration.GetConnectionString("LibraryConnection");
        builder.Services.AddDbContext<LibraryDbContext>
            (options => options.UseNpgsql(connLibraryDb, x => x.MigrationsAssembly("Library.Infrastructure")));

        var connAuthDb = builder.Configuration.GetConnectionString("AuthConnection");
        builder.Services.AddDbContext<AuthDbContext>
            (options => options.UseNpgsql(connAuthDb, x => x.MigrationsAssembly("Library.Infrastructure")));
        
        //DI
        builder.Services.AddLibraryDependencies();

        //Asp Identity for Auth
        builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], 
                    ValidAudience = builder.Configuration["Jwt:Audience"], 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

        // Validators
        // (in the future in Add.Controllers I will register all validators
        // so, I will not need a lot of DIs and validation will be auto)
        builder.Services.AddValidatorsFromAssemblyContaining<BookFilterDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();


        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter Your Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        // Role seeding in db
        using (var serviceScope = app.Services.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            string[] roles = ["Admin", "Member"];
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }

        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}