namespace Contonso.SampleApi.Infrastructure.Persistence;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Domain.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    private readonly DbConnection? sqlConnection;

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
        if (this.Database.IsRelational())
        {
            this.sqlConnection = this.Database.GetDbConnection();
        }
    }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        return (await this.sqlConnection.QueryAsync<T>(sql, param, transaction)).AsList();
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await this.sqlConnection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> QuerySingleAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await this.sqlConnection.QuerySingleAsync<T>(sql, param, transaction);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.ProcessInternalChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override void Dispose()
    {
        this.sqlConnection?.Dispose();
        GC.SuppressFinalize(this);
        base.Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureCreationTracking(EntityEntry entry, DateTime now)
    {
        if (entry.Entity is ICreationTracker creationTrackable && entry.State == EntityState.Added)
        {
            creationTrackable.CreatedOn = now;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureModificationTracking(EntityEntry entry, DateTime now)
    {
        if (entry.Entity is IModificationTracker modificationTrackable &&
            (entry.State == EntityState.Modified || entry.State == EntityState.Deleted))
        {
            modificationTrackable.ModifiedOn = now;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureSoftDeletion(EntityEntry entry, bool force = false)
    {
        if (entry.State != EntityState.Deleted || force)
        {
            return;
        }

        if (entry.Entity is not IArchivable archivable)
        {
            return;
        }

        archivable.IsDeleted = true;
        entry.State = EntityState.Modified;
    }

    private void ProcessInternalChanges()
    {
        var utcNow = DateTime.UtcNow;
        var entries = this.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified &&
                entry.State != EntityState.Deleted)
            {
                continue;
            }

            EnsureCreationTracking(entry, utcNow);
            EnsureModificationTracking(entry, utcNow);
            EnsureSoftDeletion(entry);
        }
    }
}
