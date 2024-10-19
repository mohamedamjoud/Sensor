using Adapter.SQLite.Models;

namespace Adapter.SQLite.Repository;

public interface IRepository<TEntity> where TEntity : class, IEntity
{  
    Task<int> Add(TEntity entity);
}