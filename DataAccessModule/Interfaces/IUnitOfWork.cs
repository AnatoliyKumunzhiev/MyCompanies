using System;
using System.Data.Entity;

namespace DataAccessModule.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<TEntity> Repository<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
