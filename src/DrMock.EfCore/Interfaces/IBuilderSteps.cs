using System;
using System.Linq.Expressions;

namespace DrMock.EfCore.Interfaces
{
    public interface IBuilderSteps<TContext>
        where TContext : class, IDbContext
    {
        MockDbContext<TContext> UseEntity<T>()
            where T : class, new();
        
        MockDbContext<TContext> WithRandomDataFor<T>(int numberOfItems = 5)
            where T : class, new();

        MockDbContext<TContext> Include<T>(params T[] entities)
            where T : class, new();

        MockDbContext<TContext> DontInclude<T>(params T[] entities)
            where T : class, new();

        MockDbContext<TContext> Exists<T>(Expression<Func<T, bool>> matcher)
            where T : class, new();
        
        MockDbContext<TContext> DoesNotExist<T>(Expression<Func<T, bool>> matcher)
            where T : class, new();

        MockDbContext<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new();
        
        MockDbContext<TContext> WithActionOnAddAsync<T>(Action<T> action)
            where T : class, new();

        MockDbContext<TContext> WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new();

        MockDbContext<TContext> WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new();
    }
}