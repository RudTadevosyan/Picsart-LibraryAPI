using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class AuthDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options): base  (options) {}
    
}