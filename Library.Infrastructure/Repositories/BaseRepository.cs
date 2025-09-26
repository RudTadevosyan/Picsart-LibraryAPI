using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>: IRepository<TEntity> where TEntity : class
{
    protected readonly LibraryDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(LibraryDbContext libraryDbContext)
    {
        _context = libraryDbContext;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> CheckId(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity != null;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}