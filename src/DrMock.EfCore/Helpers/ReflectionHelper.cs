using DrMock.EfCore.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DrMock.EfCore.Helpers
{
    internal static class ReflectionHelper
    {
        public static Mock<DbSet<T>> GetMockDbSetAttribute<TContext, T>(this Mock<TContext> mockContext)
            where T : class
            where TContext : class
        {
            var propertyMatch = typeof(TContext).GetProperties()
                                                .FirstOrDefault(x => x.PropertyType == typeof(DbSet<T>));

            var dbSet = propertyMatch.GetValue(mockContext.Object, null);

            if (dbSet == null) 
                throw DrMockException.DbSetNotFoundForProperty<T>();

            return (dbSet as DbSet<T>).GetMockFromObject();
        }

        public static void SetMockDbSetAttribute<TContext, T>(this Mock<TContext> mockContext, DbSet<T> value)
            where T : class
            where TContext : class
        {
            var param = Expression.Parameter(typeof(TContext), "x");

            var propertyMatches = typeof(TContext).GetProperties()
                                                .Where(x => x.PropertyType == typeof(DbSet<T>));

            if (!propertyMatches.Any())
                throw DrMockException.DbSetNotFoundForProperty<T>();

            if (propertyMatches.Count() > 1)
                throw DrMockException.MultipleProperiesForSameType<T>();

            var propertyMatch = propertyMatches.First();
            var propertyGetter = propertyMatch.GetGetMethod(true);
            var isVirtualProp = propertyGetter.IsVirtual && !propertyGetter.IsFinal;

            if (!isVirtualProp)
                throw DrMockException.NonVirtualProperty<T>();

            var property = Expression.Property(param, propertyMatch.Name);
            var lambda = Expression.Lambda<Func<TContext, DbSet<T>>>(property, param);

            mockContext.Setup(lambda).Returns(value);
        }


        public static Mock<T> GetMockFromObject<T>(this T mockedObject) where T : class
        {
            PropertyInfo[] propInfo = mockedObject.GetType().GetProperties()
                .Where(
                    p => p.PropertyType.Name == "Mock`1"
                ).ToArray();

            return propInfo.FirstOrDefault().GetGetMethod().Invoke(mockedObject, null) as Mock<T>;
        }

        public static Expression<Func<T[], bool>> ToArrayPredicate<T>(
            this Expression<Func<IEnumerable<T>, bool>> enumerablePredicate)
        {

            var arrayParam = Expression.Parameter(typeof(T[]), enumerablePredicate.Parameters[0].Name);

            var body = new ReplaceVisitor(enumerablePredicate.Parameters[0], arrayParam)
                .Visit(enumerablePredicate.Body);

            return Expression.Lambda<Func<T[], bool>>(body, arrayParam);
        }

        private class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression _from;
            private readonly Expression _to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                _from = from;
                _to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == _from ? _to : base.Visit(node);
            }
        }


        public static string BuildDiagnosticMessage<T>(
            Expression<Func<IEnumerable<T>, bool>> predicate,
            IEnumerable<T> actual)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Verification failed: The range did not satisfy the expected predicate.");
            sb.AppendLine();

            sb.AppendLine("Expected predicate:");
            sb.AppendLine($"  {predicate}");
            sb.AppendLine();

            sb.AppendLine("Actual values passed to AddRange/UpdateRange:");
            foreach (var item in actual)
            {
                sb.AppendLine("  - " + DumpObject(item));
            }

            return sb.ToString();
        }


        private static string DumpObject<T>(T obj)
        {
            if (obj == null)
                return "<null>";

            var props = typeof(T).GetProperties();
            var parts = props.Select(p => $"{p.Name} = {p.GetValue(obj)}");
            return string.Join(", ", parts);
        }


    }
}