using DrMock.EfCore.Exceptions;
using DrMock.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;

namespace DrMock.EfCore.Helpers
{
    internal static class ParamRangeHelper
    {
        public static void VerifyRangeAddedAsObjectArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.AddRange, times);
        }
        
        public static void VerifyRangeAddedAsObjectNestedArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object[], T>(matches, EfMethod.AddRange, times);
        }

        public static void VerifyRangeAdded<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRange(It.IsAny<T[]>()));
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.AddRange, times);
        }

        public static void VerifyRangeAddedAsyncAsObjectArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRangeAsync(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeAddedAsyncAsObjectNestedArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRangeAsync(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object[], T>(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeAddedAsync<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRangeAsync(It.IsAny<T[]>(), It.IsAny<CancellationToken>()));
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeUpdatedAsObjectArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.UpdateRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.UpdateRange, times);
        }
        
        public static void VerifyRangeUpdatedAsObjectNestedArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.UpdateRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object[], T>(matches, EfMethod.UpdateRange, times);
        }

        public static void VerifyRangeUpdated<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.UpdateRange(It.IsAny<T[]>()));
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.UpdateRange, times);
        }

        public static void VerifyRangeRemovedAsObjectArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.RemoveRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object, T>(matches, EfMethod.RemoveRange, times);
        }

        public static void VerifyRangeRemovedAsObjectNestedArray<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.RemoveRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches<object[], T>(matches, EfMethod.RemoveRange, times);
        }

        public static void VerifyRangeRemoved<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.RemoveRange(It.IsAny<T[]>()));
            mockDbSet.CheckInvocationsForMatches<T, T>(matches, EfMethod.RemoveRange, times);
        }

        public static void CheckInvocationsForMatches<TIn, TOut>(this Mock mock, Expression<Func<IEnumerable<TOut>, bool>> matches, EfMethod efMethod, Times times)
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
                throw DrMockException.CallWasNotMade<TOut>(efMethod); // Change This

        }

        private static bool CheckForParameterMatches<TIn, TOut>(this IInvocation invocation, Expression<Func<IEnumerable<TOut>, bool>> matches, EfMethod efMethod)
        {
            IEnumerable<TIn> arguments;

            try
            {
                arguments = (IList<TIn>)invocation.Arguments[0];

                arguments = arguments.ToList();
            }
            catch (Exception ex)
            {
                return false;
            }
             
            IEnumerable<TOut> castedArguments = new List<TOut>();

            try
            {
                if (typeof(TIn) == typeof(object[]))
                    castedArguments = (arguments.First() as object[]).Select(x => (TOut) x).ToList();
                else if (typeof(TIn) == typeof(object))
                    castedArguments = arguments.Select(x => (TOut) (x as object)).ToList();
                else
                    castedArguments = arguments as TOut[];
            }
            catch (Exception ex)
            {
                return false;
            }

            var compiled = matches.Compile();
            return compiled(castedArguments);
        }
    }
}