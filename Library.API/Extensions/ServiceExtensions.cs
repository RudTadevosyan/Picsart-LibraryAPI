using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Infrastructure.Repositories;

namespace LibraryAPI.Extensions;

public static class ServiceExtensions
{
    public static void AddLibraryDependencies(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookDetailRepository, BookDetailRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        
        //Services
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IMemberService, MemberService>();
    }
}