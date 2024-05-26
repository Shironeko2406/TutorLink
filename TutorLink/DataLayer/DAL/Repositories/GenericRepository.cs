using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DAL.Repositories;

#region Interface GenericRepository

public interface IGenericRepository<T> where T : class
{
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
    Task SaveChangesAsync();
    void Dispose();
    Task<int> CountAsync(Expression<Func<T, bool>> expression = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
}

#endregion

#region Methods
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TutorDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(TutorDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

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

    public void Dispose()
    {
        _context.Dispose();
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
}
#endregion