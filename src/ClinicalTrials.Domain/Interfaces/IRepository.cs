using System.Linq.Expressions;

namespace ClinicalTrials.Domain.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int skipCount, int takeCount,
        Expression<Func<T, object>> orderBy, bool isDescending = false, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}