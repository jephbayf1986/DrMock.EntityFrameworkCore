using DrMock.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DrMock.EfCore.Helpers
{
    internal static class ParamRangeHelper
    {
        public static void VerifyRangeAddedWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext
            where T : class, new()
        {
            mockDbContext.Verify(x => x.AddRange(It.IsAny<T[]>()), Times.AtLeastOnce);
            mockDbContext.CheckInvocationsForMatches(matches, times);
        }

        public static void VerifyRangeAddedWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRange(It.IsAny<T[]>()), Times.AtLeastOnce);
            mockDbSet.CheckInvocationsForMatches(matches, times);
        }

        public static void VerifyRangeUpdatedWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext, new()
            where T : class, new()
        {
            mockDbContext.Verify(x => x.UpdateRange(It.IsAny<T[]>()), Times.AtLeastOnce);
            mockDbContext.CheckInvocationsForMatches(matches, times);
        }

        public static void VerifyRangeUpdatedWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.UpdateRange(It.IsAny<T[]>()), Times.AtLeastOnce);
            mockDbSet.CheckInvocationsForMatches(matches, times);
        }

        public static void VerifyRangeRemovedWithParams<TContext, T>(this Mock<TContext> mockDbContext, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where TContext : class, IDbContext, new()
            where T : class, new()
        {
            mockDbContext.Verify(x => x.RemoveRange(It.IsAny<T[]>()), Times.AtLeastOnce);
            mockDbContext.CheckInvocationsForMatches(matches, times);
        }
        
        public static void VerifyRangeRemovedWithParams<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
             where T : class, new()
        {
            mockDbSet.Verify(x => x.RemoveRange(It.IsAny<T[]>()), Times.AtLeastOnce);
            mockDbSet.CheckInvocationsForMatches(matches, times);
        }

        public static void CheckInvocationsForMatches<T>(this Mock mock, Expression<Func<IEnumerable<T>, bool>> matches, Times times)
        {
            T[] captured = null;

            mock.Invocations
                .Where(inv => inv.Method.Name == EfMethod.AddRange.ToString())
                .ToList()
                .ForEach(inv => captured = (T[])inv.Arguments[0]);

            if (captured == null)
                throw new Exception("AddRange was not called.");

            var compiled = matches.Compile();
            var result = compiled(captured);

            // TODO Times Check

            if (!result)
            {
                throw new Exception("AddRange argument did not satisfy the predicate.");
            }
        }
    }
}