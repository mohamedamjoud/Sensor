using Adapter.SQLite.Context;
using Adapter.SQLite.Exception;
using Adapter.SQLite.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLite.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
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
            throw new DatabaseSaveException("An error occurred while saving the entity.", ex);
        }
    }
    
}