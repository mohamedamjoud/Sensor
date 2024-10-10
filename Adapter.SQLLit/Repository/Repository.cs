using Adapter.SQLLit.Context;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            _context.Set<TEntity>().Add(entity);
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving the entity.", ex);
        }
    }
    
}