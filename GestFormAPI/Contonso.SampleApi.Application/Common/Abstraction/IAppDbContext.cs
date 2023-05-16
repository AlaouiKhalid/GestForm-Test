namespace Contonso.SampleApi.Application.Common.Abstraction;

using System.Data;

public interface IAppDbContext
{
    Task<IReadOnlyList<T>> QueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default);

    Task<T> QueryFirstOrDefaultAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default);

    Task<T> QuerySingleAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
