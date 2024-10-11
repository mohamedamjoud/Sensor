using Adapter.SQLLit.Models;

namespace Adapter.SQLLit.Repository;

public interface IRepository<TEntity> where TEntity : class, IEntity
{  
    Task<int> Add(TEntity entity);
}