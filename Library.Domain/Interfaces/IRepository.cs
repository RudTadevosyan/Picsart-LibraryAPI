namespace Library.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<bool> CheckId(int id);
    Task SaveChanges();
}