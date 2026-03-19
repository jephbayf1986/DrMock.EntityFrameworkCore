using DrMock.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Xml.Linq;

namespace DrMock.EfCore.Helpers
{
    internal static class ParamRangeHelper
    {
        public static void VerifyRangeAddedWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRange(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches(matches, EfMethod.AddRange, times);
        }

        public static void VerifyRangeAddedWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRange(It.IsAny<T[]>()));
            mockDbSet.CheckInvocationsForMatches(matches, EfMethod.AddRange, times);
        }

        public static void VerifyRangeAddedAsyncWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRangeAsync(It.IsAny<object[]>()));
            mockDbContext.CheckInvocationsForMatches(matches, EfMethod.AddRangeAsync, times);
        }
        
        public static void VerifyRangeAddedAsyncWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRangeAsync(It.IsAny<T[]>(), It.IsAny<CancellationToken>()));
            mockDbSet.CheckInvocationsForMatches(matches, EfMethod.AddRangeAsync, times);
        }

        public static void VerifyRangeUpdatedWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.UpdateRange(It.IsAny<T[]>()));
            mockDbContext.CheckInvocationsForMatches(matches, EfMethod.UpdateRange, times);
        }

        public static void VerifyRangeUpdatedWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.UpdateRange(It.IsAny<T[]>()));
            mockDbSet.CheckInvocationsForMatches(matches, EfMethod.UpdateRange, times);
        }

        public static void VerifyRangeRemovedWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.RemoveRange(It.IsAny<T[]>()));
            mockDbContext.CheckInvocationsForMatches(matches, EfMethod.RemoveRange, times);
        }
        
        public static void VerifyRangeRemovedWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.RemoveRange(It.IsAny<T[]>()));
            mockDbSet.CheckInvocationsForMatches(matches, EfMethod.RemoveRange, times);
        }

        public static void CheckInvocationsForMatches<T>(this Mock mock, Expression<Func<IEnumerable<T>, bool>> matches, EfMethod efMethod, Times times)
        {
            object[] captured = null;

            mock.Invocations
                .Where(inv => inv.Method.Name == efMethod.ToString())
                .ToList()
                .ForEach(inv => captured = (object[])inv.Arguments[0]);

            if (captured == null)
                throw new Exception("AddRange was not called.");

            var capturedCasted = captured.Select(x => (T)x);

            var compiled = matches.Compile();
            var result = compiled(capturedCasted);

            // TODO Times Check

            if (!result)
            {
                throw new Exception("AddRange argument did not satisfy the predicate.");
            }
        }
    }
}