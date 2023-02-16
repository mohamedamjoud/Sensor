using Adapter.SQLLit.Context;
using Adapter.SQLLit.Models;
using Core.SpiPort;

namespace Adapter.SQLLit.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<int> Add(TEntity entity);
}