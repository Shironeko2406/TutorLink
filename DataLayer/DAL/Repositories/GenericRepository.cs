using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
#pragma warning disable
namespace DataLayer.DAL.Repositories;

#region Interface GenericRepository

public interface IGenericRepository<T> where T : class
{
    #region Unasync method
    ICollection<T> GetAll();
    ICollection<T> GetList(Expression<Func<T, bool>> expression);
    T Get(Expression<Func<T, bool>> expression);
    T Add(T entity);
    void AddRange(ICollection<T> entities);
    void Update(T entity);
    void Delete(Guid id);
    void Delete(string id);
    void Remove(T entity);
    void ClearTrackers();
    int SaveChanges();
    void Dispose();
    #endregion
    
    #region Async
    Task<int> CountAsync(Expression<Func<T, bool>> expression = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdAsync(int id);
    Task<ICollection<T>> GetAllWithAsync();
    Task<ICollection<T>> GetListWithAsync(Expression<Func<T, bool>> expression);
    Task<T> GetSingleWithAsync(Expression<Func<T, bool>> expression);
    Task<EntityEntry<T>> AddSingleWithAsync(T entity);
    Task AddRangeWithAsync(ICollection<T> entities);
    Task UpdateWithAsync(T entity);
    Task SaveChangesAsync();
    Task<ICollection<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    Task DeleteAsync(Guid id);

    #endregion
}

#endregion

#region Methods
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TutorDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(TutorDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    #region Unasync method
    public virtual ICollection<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public ICollection<T> GetList(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression).ToList();
    }

    public virtual T Get(Expression<Func<T, bool>> expression)
    {
        return _dbSet.FirstOrDefault(expression);
    }

    public virtual T Add(T entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public void AddRange(ICollection<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public virtual void Delete(Guid id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public virtual void Delete(string id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void ClearTrackers()
    {
        _context.ChangeTracker.Clear();
    }

    public virtual int SaveChanges()
    {
        try
        {
            return _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            // Handle or log the exception
            throw new Exception(ex.Message);
        }
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
    #endregion
    
    
    #region Async Methods
    public async Task UpdateWithAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Handle or log the exception
            throw new Exception(ex.Message);
        }
    }
    
    public IEnumerable<T> GetAllTest()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
    {
        if (expression == null)
        {
            return await _dbSet.CountAsync();
        }

        return await _dbSet.CountAsync(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }

    public async Task<ICollection<T>> GetAllWithAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<ICollection<T>> GetListWithAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    public async Task<T> GetSingleWithAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public async Task<EntityEntry<T>> AddSingleWithAsync(T entity)
    {
        return await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeWithAsync(ICollection<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<ICollection<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.Where(predicate).ToListAsync();
    }
    
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    #endregion
}
#endregion