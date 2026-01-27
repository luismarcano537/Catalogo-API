using APICatalogo.Models;
using APICatalogo.Context;
using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly APICatalogoContext _context;
    public Repository(APICatalogoContext context)
    {
        _context = context;
    }


    public IEnumerable<T> Get()
    {
        return _context.Set<T>().ToList();
    }


    public T GetByID(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }


    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();

        return entity;
    }


    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        
        return entity;
    }
}
