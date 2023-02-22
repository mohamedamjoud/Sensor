namespace Adapter.SQLLit.Repository;

public interface IRepository<TEntity> where TEntity : class
{  
    Task<int> Add(TEntity entity);
}