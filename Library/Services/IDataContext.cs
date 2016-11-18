using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Library.Models;

namespace Library.Services
{
   public interface IDataContext : IObjectContextAdapter, IDisposable
    {
        IDbSet<User> Users { get; set; }
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
