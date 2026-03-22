using DrMock.EfCore.Exceptions;
using DrMock.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DrMock.EfCore.Helpers
{
    internal static class RangeInvocationMatcher
    {
        public static void VerifyRangeAddedAsObjects<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.AddRange, times);
        }
        
        public static void VerifyRangeAddedAsClass<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<T, T>(matches, EfMethod.AddRange, times);
        }

        public static void VerifyRangeAdded<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.AddRange, times);
        }

        public static void VerifyRangeAddedAsyncAsObjects<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeAddedAsyncAsClass<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<T, T>(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeAddedAsync<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeUpdatedAsObjects<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.UpdateRange, times);
        }
        
        public static void VerifyRangeUpdatedAsClass<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<T, T>(matches, EfMethod.UpdateRange, times);
        }

        public static void VerifyRangeUpdated<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.UpdateRange, times);
        }

        public static void VerifyRangeRemovedAsObjects<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.RemoveRange, times);
        }

        public static void VerifyRangeRemovedAsClass<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.CheckInvocationsForMatches<T, T>(matches, EfMethod.RemoveRange, times);
        }

        public static void VerifyRangeRemoved<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.RemoveRange, times);
        }

        private static void CheckInvocationsForMatches<TIn, TOut>(this Mock mock, Expression<Func<IEnumerable<TOut>, bool>> matches, EfMethod efMethod, Times times)
        {
            var validInvocations = 0;
            
            var invocationsOfMethod = mock.Invocations
                .Where(inv => inv.Method.Name == efMethod.ToString())
                .ToList();

            foreach (var invocation in invocationsOfMethod)
            {
                var isValidInvocation = invocation.CheckForParameterMatches<TIn, TOut>(matches, efMethod);

                if (isValidInvocation) validInvocations++;
            }

            var validNumberOfInvocations = times.Validate(validInvocations);

            if (!validNumberOfInvocations)
                throw DrMockException.CallMadeIncorrectFrequency<TOut>(efMethod, times, validInvocations);

        }

        private static bool CheckForParameterMatches<TIn, TOut>(this IInvocation invocation, Expression<Func<IEnumerable<TOut>, bool>> matches, EfMethod efMethod)
        {
            IEnumerable<TIn> arguments;

            try
            {
                arguments = (IList<TIn>)invocation.Arguments[0];

                arguments = arguments.ToList();
            }
            catch
            {
                // Sometimes it is passed as [[]] so we need to check next level.
                try
                {
                    var argumentsAsObjects = (IList<object>)invocation.Arguments[0];

                    if (argumentsAsObjects.Count() == 1
                            && argumentsAsObjects.First().GetType().IsGenericType
                            && argumentsAsObjects.First().GetType().GetGenericTypeDefinition() == typeof(List<>)
                            && argumentsAsObjects.First().GetType().GenericTypeArguments.First() == typeof(TIn))
                        arguments = argumentsAsObjects.First() as IEnumerable<TIn>;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }

            IEnumerable<TOut> castedArguments = new List<TOut>();

            try
            {
                if (typeof(TIn) == typeof(object[]))
                    castedArguments = (arguments.First() as object[]).Select(x => (TOut) x).ToList();
                else if (typeof(TIn) == typeof(object))
                    castedArguments = arguments.Select(x => (TOut) (x as object)).ToList();
                else
                    castedArguments = arguments.Cast<TOut>().ToList();
            }
            catch (Exception ex)
            {
                return false;
            }

            if (castedArguments is null)
                return false;

            var compiled = matches.Compile();
            return compiled(castedArguments);
        }
    }
}