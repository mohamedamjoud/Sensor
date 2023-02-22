using Adapter.SQLLit.Context;

namespace Adapter.SQLLit.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly TemperatureContext _context;

    public Repository(TemperatureContext context)
    {
        _context = context;
    }
    public async Task<int> Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        return await _context.SaveChangesAsync();
    }
    
}