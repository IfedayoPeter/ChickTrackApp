namespace Base.Repositories.Interfaces;

public interface IMSSQLRepository<T, I> where T : BaseEntity<I>
{
    Task<T[]> AddEntitiesAsync(IEnumerable<T> entities);
    Task<T> CreateAsync(T entity);
    Task<bool> DeleteAsync(I id);
    Task<IList<string>> DeleteAsync(Expression<Func<T, bool>> expression);
    Task<IList<T>> GetAllAsync();
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
    Task<T> GetSingleWithIncludeAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    Task<T> GetByCodeAsync(string code);
    Task<T?> GetByIdAsync(I id);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression);
    Task<bool> UpdateAsync(I id, T entity);
    Task<IList<T>> GetAllAsync(string search = null, string filter = null, int page = 1, int pageSize = 10);
    bool DeleteAsync(IList<T> entities);
    Task<IList<T>> GetAllAsync(string search = null, string filter = null, int page = 1, int pageSize = 10, string orderBy = null, OrderDirectionEnum orderDirection = OrderDirectionEnum.Asc, string baseUrl = "{app_url}");
    Task<Result<List<T>>> GetAllWithMetaAsync(string search = null, string filter = null, int page = 1, int pageSize = 10, string orderBy = null, OrderDirectionEnum orderDirection = OrderDirectionEnum.Asc, string baseUrl = "{app_url}");
}
