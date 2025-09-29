using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class LibraryDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options):base(options){}
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookDetail> BookDetails { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Book <-> BookDetails 1:1
        modelBuilder.Entity<Book>()
            .HasOne(b => b.BookDetail)
            .WithOne(d => d.Book)
            .HasForeignKey<BookDetail>(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Book -> Loan 1:N
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Loans)
            .WithOne(l => l.Book)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Book <-> Genre N:M
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity<Dictionary<string, object>>( // to make a join table
                "BooksGenres", 
                j => j
                    .HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey("GenreId")
                    .OnDelete(DeleteBehavior.Cascade), 
                j => j
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey("BookId")
                    .OnDelete(DeleteBehavior.Cascade) 
            );

            //Author -> Book 1:N
            modelBuilder.Entity<Author>().HasIndex(a => a.AuthorName);
            
            modelBuilder.Entity<Author>()
               .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); //dont delete also books
            
            //Member -> Loan 1:N
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Loans)
                .WithOne(l => l.Member)
                .HasForeignKey(l => l.MemberId)
                .OnDelete(DeleteBehavior.Cascade); //dont delete also loans
    }
}