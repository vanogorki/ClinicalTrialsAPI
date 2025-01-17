using System.Linq.Expressions;
using ClinicalTrials.Domain.Interfaces;
using ClinicalTrials.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalTrials.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<T> _dbSet;

    protected Repository(DatabaseContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int skipCount, int takeCount,
        Expression<Func<T, object>> orderBy, bool isDescending = false, CancellationToken cancellationToken = default)
    {
        if (isDescending)
            return await _dbSet.Where(predicate)
                .OrderByDescending(orderBy)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync(cancellationToken);
        return await _dbSet.Where(predicate)
            .OrderBy(orderBy)
            .Skip(skipCount)
            .Take(takeCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }
}