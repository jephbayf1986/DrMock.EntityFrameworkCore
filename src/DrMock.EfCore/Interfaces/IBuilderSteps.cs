using System;
using System.Linq.Expressions;

namespace DrMock.EfCore.Interfaces
{
    /// <summary>
    /// Builder Steps Interface
    /// Methods for creating Data within a Mock DbContext
    /// </summary>
    /// <typeparam name="TContext">Type subject to Mocking that implements IDbContext<typeparam>
    public interface IBuilderSteps<TContext>
        where TContext : class, IDbContext
    {
        /// <summary>
        /// Use Entity
        /// Declares use of DbSet within a unit test scope of work
        /// </summary>
        /// <typeparam name="T">Type of Entity to be Used in DbSet</typeparam>
        /// <returns>Self</returns>
        MockDbContext<TContext> UseEntity<T>()
            where T : class, new();

        /// <summary>
        /// With Random Data For (Entity T)
        /// Declares use of DbSet and assigns random data to be used within a unit test scope of work
        /// </summary>
        /// <typeparam name="T">Type of Entity to be Used in DbSet</typeparam>
        /// <param name="numberOfItems">Optional number of items to generate (default is 5)</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> WithRandomDataFor<T>(int numberOfItems = 5)
            where T : class, new();

        /// <summary>
        /// Include (Entities T)
        /// Declares use of DbSet
        /// </summary>
        /// <typeparam name="T">Type of Entity to be included</typeparam>
        /// <param name="entities">Array of Entities to Include</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> Include<T>(params T[] entities)
            where T : class, new();

        /// <summary>
        /// Exlude (Entities T)
        /// Declares use of DbSet and assigns random data, then ensure it doesn't match entitites, within a unit test scope of work
        /// </summary>
        /// <typeparam name="T">Type of Entity to be exluded</typeparam>
        /// <param name="entities">Array of Entities to Exlude</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> Exclude<T>(params T[] entities)
            where T : class, new();

        /// <summary>
        /// Exists (Expression Match)
        /// Declares use of DbSet and sets minimum properties needed for data used wtihin a unit test scope of work
        /// </summary>
        /// <typeparam name="T">Type of Entity to be Used in DbSet</typeparam>
        /// <param name="matcher">Expression Match required to pass</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> Exists<T>(Expression<Func<T, bool>> matcher)
            where T : class, new();

        /// <summary>
        /// Does Not Exist (Expression Match)
        /// Declares use of DbSet and assigns random data, then ensure it doesn't the expression, within a unit test scope of work
        /// </summary>
        /// <typeparam name="T">Type of Entity to be Used in DbSet</typeparam>
        /// <param name="matcher">Expression Match required for data to not have</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> DoesNotExist<T>(Expression<Func<T, bool>> matcher)
            where T : class, new();

        /// <summary>
        /// With Action On Add
        /// For testing changes to entity identity that occur on Add of new entity
        /// </summary>
        /// <typeparam name="T">Type of Entity being created</typeparam>
        /// <param name="action">Callback on call of Add</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new();

        /// <summary>
        /// With Action On AddAsync
        /// For testing changes to entity identity that occur on AddAsync of new entity
        /// </summary>
        /// <typeparam name="T">Type of Entity being created</typeparam>
        /// <param name="action">Callback on call of AddAsync</param>
        /// <returns>Self</returns>
        MockDbContext<TContext> WithActionOnAddAsync<T>(Action<T> action)
            where T : class, new();

        /// <summary>
        /// With Exception Thrown On SaveChanges
        /// For testing handling of unexpected exceptions raised while calling SaveChanges
        /// </summary>
        /// <typeparam name="TEx">Type of Exception expected</typeparam>
        /// <returns>Self</returns>
        MockDbContext<TContext> WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new();

        /// <summary>
        /// With Exception Thrown On SaveChangesAsync
        /// For testing handling of unexpected exceptions raised while calling SaveChangesAsync
        /// </summary>
        /// <typeparam name="TEx">Type of Exception expected</typeparam>
        /// <returns>Self</returns>
        MockDbContext<TContext> WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new();
    }
}