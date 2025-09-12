﻿namespace Base.Repositories.Implementations;

public abstract class MSSQLBaseRepository<T, I> : IMSSQLRepository<T, I>
    where T : BaseEntity<I>
{
    private readonly IApplicationDbContext _context;

    protected MSSQLBaseRepository(IApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    #region CRUD Operations

    public virtual async Task<IList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IList<T>> GetAllAsync(
            string search = null,
            string filter = null,
            int page = 1,
            int pageSize = 10,
            string orderBy = null,
            OrderDirectionEnum orderDirection = OrderDirectionEnum.Asc,
            string baseUrl = "{app_url}")
    {
        // Return only the data (backward compatible)
        var paged = await GetAllWithMetaAsync(
            search, filter, page, pageSize, orderBy, orderDirection,
            baseUrl: baseUrl);

        return paged.Content.ToList();
    }

    public async Task<Result<List<T>>> GetAllWithMetaAsync(
        string search = null,
        string filter = null,
        int page = 1,
        int pageSize = 10,
        string orderBy = null,
        OrderDirectionEnum orderDirection = OrderDirectionEnum.Asc,
        string baseUrl = "{app_url}")
    {
        //if (pageSize <= 0) pageSize = 10;
        //if (page <= 0) page = 1;

        IQueryable<T> query = _context.Set<T>();

        // FILTER
        if (!string.IsNullOrEmpty(filter))
        {
            try
            {
                var filterParts = filter.Split('=', StringSplitOptions.TrimEntries);
                if (filterParts.Length == 2)
                {
                    var propertyName = filterParts[0];
                    var propertyValue = filterParts[1];

                    var parameter = Expression.Parameter(typeof(T), nameof(T));
                    var property = Expression.Property(parameter, propertyName);

                    Expression constant;
                    Expression equalsExpression;

                    if (property.Type == typeof(bool) && bool.TryParse(propertyValue, out var boolValue))
                    {
                        constant = Expression.Constant(boolValue, typeof(bool));
                        equalsExpression = Expression.Equal(property, constant);
                    }
                    else if (property.Type == typeof(DateTimeOffset) && DateTimeOffset.TryParse(propertyValue, out var dto))
                    {
                        constant = Expression.Constant(dto, typeof(DateTimeOffset));
                        equalsExpression = Expression.Equal(property, constant);
                    }
                    else
                    {
                        constant = Expression.Constant(propertyValue, typeof(string));
                        equalsExpression = Expression.Equal(property, constant);
                    }

                    var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);
                    query = query.Where(lambda);
                }
            }
            catch
            {
                throw new Exception("Provide a valid filter parameter.");
            }
        }

        // ORDER
        if (!string.IsNullOrEmpty(orderBy))
        {
            var prop = typeof(T).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.Property(parameter, prop);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                var methodName = orderDirection == OrderDirectionEnum.Desc ? "OrderByDescending" : "OrderBy";

                var resultExp = Expression.Call(
                    typeof(Queryable), methodName,
                    new Type[] { typeof(T), prop.PropertyType },
                    query.Expression, Expression.Quote(orderByExp));

                query = query.Provider.CreateQuery<T>(resultExp);
            }
        }

        // INCLUDE nav props
        var navigationProperties = typeof(T).GetProperties()
            .Where(p => typeof(IEnumerable<object>).IsAssignableFrom(p.PropertyType) ||
                       (p.PropertyType.IsClass && p.PropertyType != typeof(string)));

        foreach (var nav in navigationProperties)
            query = query.Include(nav.Name);

        // Total BEFORE pagination (note: search is applied in-memory below)
        var totalCount = await query.CountAsync();

        // PAGE BOUNDS
        var lastPage = Math.Max(1, (int)Math.Ceiling((double)totalCount / pageSize));
        if (page > lastPage) page = lastPage;

        var skip = (page - 1) * pageSize;
        var from = totalCount == 0 ? 0 : skip + 1;
        var to = Math.Min(skip + pageSize, totalCount);

        // PAGE DATA
        var results = (page == 0 && pageSize == 0) ? await query.ToListAsync() : await query.Skip(skip).Take(pageSize).ToListAsync();

        // SEARCH (in-memory; if you want DB-side search, we can push it into the expression tree)
        if (!string.IsNullOrEmpty(search))
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            results = results
                .Where(item => props.Any(prop =>
                {
                    var value = prop.GetValue(item)?.ToString();
                    return value != null && value.Contains(search, StringComparison.OrdinalIgnoreCase);
                }))
                .ToList();
            // 'totalCount' doesn't reflect search when search is in-memory.
            // If you need 'Total' to include search, we should move search into the IQueryable.
        }

        // BUILD META (normalize page query to lowercase and no spaces)
        string Q(int n) => $"{baseUrl}?page={n}";

        var result = new Result<List<T>>
        {
            Content = results,
            MetaData = new MetaData
            {
                From = from,
                LastPage = lastPage,
                FirstPageUrl = Q(1),
                LastPageUrl = Q(lastPage),
                NextPageUrl = page < lastPage ? Q(page + 1) : null,
                Path = baseUrl,
                PerPage = pageSize,
                PrevPageUrl = page > 1 ? Q(page - 1) : null,
                To = to,
                Total = totalCount
            }
        };

        return result;
    }

    public async Task<IList<T>> GetAllAsync(
    string search = null,
    string filter = null,
    int page = 1,
    int pageSize = 10)
    {
        IQueryable<T> query = _context.Set<T>();

        // Dynamically include all navigation properties
        var navigationProperties = typeof(T).GetProperties()
            .Where(prop => typeof(IEnumerable<object>).IsAssignableFrom(prop.PropertyType) ||
                           (prop.PropertyType.IsClass && prop.PropertyType != typeof(string)));

        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty.Name);
        }

        // Apply filtering based on the search term
        if (!string.IsNullOrEmpty(filter))
        {
            try
            {
                var filterParts = filter.Split('=', StringSplitOptions.TrimEntries);
                if (filterParts.Length == 2)
                {
                    var propertyName = filterParts[0];
                    var propertyValue = filterParts[1];

                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, propertyName);
                    var propertyType = property.Type;

                    Expression constant = null;

                    // Handle different property types
                    if (propertyType == typeof(string))
                    {
                        constant = Expression.Constant(propertyValue, typeof(string));
                    }
                    else if (propertyType.IsEnum) // Handle Enums
                    {
                        var enumValue = Enum.Parse(propertyType, propertyValue);
                        constant = Expression.Constant(enumValue);
                    }
                    else if (propertyType == typeof(int) || propertyType == typeof(int?)) // Handle Int
                    {
                        constant = Expression.Constant(int.Parse(propertyValue));
                    }
                    else if (propertyType == typeof(bool) || propertyType == typeof(bool?)) // Handle Boolean
                    {
                        constant = Expression.Constant(bool.Parse(propertyValue));
                    }
                    else if (propertyType == typeof(long) || propertyType == typeof(long?)) // Handle long
                    {
                        constant = Expression.Constant(long.Parse(propertyValue));
                    }
                    else
                    {
                        throw new Exception($"Filtering for type {propertyType} is not supported.");
                    }

                    // Create an Equals expression
                    var equalsExpression = Expression.Equal(property, constant);

                    // Create the lambda: x => x.PropertyName == value
                    var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);

                    query = query.Where(lambda);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Provide a valid filter parameter", ex);
            }
        }

        // Fetch data from the database
        var results = await query.ToListAsync();

        // Apply search across all properties in-memory
        if (!string.IsNullOrEmpty(search))
        {
            try
            {
                var resultProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                results = results
                    .Where(result =>
                        resultProperties.Any(prop =>
                        {
                            var value = prop.GetValue(result)?.ToString();
                            return value != null && value.Contains(search, StringComparison.OrdinalIgnoreCase);
                        }))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Provide a valid search parameter", ex);
            }
        }

        // Total data count (after filtering and searching)
        var totalCount = results.Count;

        // Pagination
        if (page != 0 && pageSize != 0)
        {
            results = results
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        return results;
    }

    public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }

    public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(expression);
    }

    public virtual async Task<T> GetSingleWithIncludeAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(expression);
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
    {
        var response = await _context.Set<T>().Where(expression).FirstOrDefaultAsync(expression);
        return response;
    }

    public virtual async Task<T?> GetByIdAsync(I id)
    {
        ArgumentValidatorHelpers.ValidateArgument(id, nameof(id));
        var response = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        return response;
    }

    public virtual async Task<T?> GetByCodeAsync(string code)
    {
        ArgumentValidatorHelpers.ValidateStringArgument(code, nameof(code));
        return await _context.Set<T>().FindAsync(code);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        ArgumentValidatorHelpers.ValidateArgument(entity, nameof(entity));
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(I id, T entity)
    {
        ArgumentValidatorHelpers.ValidateArgument(entity, nameof(entity));

        try
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            // Ensure the ID from entity is not modified
            var entityType = typeof(T);
            var idProperty = entityType.GetProperty("Id");

            if (idProperty != null)
            {
                var existingIdValue = idProperty.GetValue(existingEntity);
                idProperty.SetValue(entity, existingIdValue);
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while updating the entity.", ex);
        }
    }

    public virtual async Task<T[]> AddEntitiesAsync(IEnumerable<T> entities)
{
    ArgumentValidatorHelpers.ValidateArgument(entities, nameof(entities));

    try
    {
        var entityArray = entities.ToArray(); // Convert to array internally
        await _context.Set<T>().AddRangeAsync(entityArray);
        return entityArray;
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException("An error occurred while adding entities.", ex);
    }
}

    public virtual async Task<bool> DeleteAsync(I id)
    {
        ArgumentValidatorHelpers.ValidateArgument(id, nameof(id));

        var entity = await GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Entity not found.");

        _context.Set<T>().Remove(entity);
        return true;
    }

    public virtual bool DeleteAsync(IList<T> entities)
    {
        try
        {
            _context.Set<T>().RemoveRange(entities);

            return true;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task<IList<string>> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        ArgumentValidatorHelpers.ValidateArgument(expression, nameof(expression));

        var entities = await GetAllAsync(expression);

        var ids = entities.Select(e => e.Id?.ToString()).ToList();

        await DeleteEntitiesAsync(expression);

        return ids;
    }

    #endregion

    #region Helper Methods

    private async Task<IList<T>> GetFilteredEntitiesAsync(Dictionary<string, string> fieldValues)
    {
        ArgumentValidatorHelpers.ValidateArgument(fieldValues, nameof(fieldValues));

        var lambda = ExpressionGenerator.GenerateLambda<T>(fieldValues);
        return await _context.Set<T>().Where(lambda).ToListAsync();
    }

    private async Task<IList<T>> DeleteEntitiesAsync(Expression<Func<T, bool>> expression)
    {
        var entitiesToDelete = await _context.Set<T>().Where(expression).ToListAsync();
        if (entitiesToDelete.Count == 0) throw new ArgumentException("No matching records found.");

        _context.Set<T>().RemoveRange(entitiesToDelete);
        await _context.SaveChangesAsync();

        return entitiesToDelete;
    }

    #endregion
}
