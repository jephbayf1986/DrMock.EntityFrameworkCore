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

        public static void SetMockDbSetAttribute<TContext>(this Mock<TContext> mockContext, Type genericType, object value)
            where TContext : class
        {
            var param = Expression.Parameter(typeof(TContext), "x");

            var dbSetType = typeof(DbSet<>).MakeGenericType(genericType);
            
            var propertyMatches = typeof(TContext).GetProperties()
                                                .Where(x => x.PropertyType == dbSetType);

            if (!propertyMatches.Any())
                throw DrMockException.DbSetNotFoundForProperty(genericType);

            if (propertyMatches.Count() > 1)
                throw DrMockException.MultipleProperiesForSameType(genericType);

            var propertyMatch = propertyMatches.First();
            var propertyGetter = propertyMatch.GetGetMethod(true);
            var isVirtualProp = propertyGetter.IsVirtual && !propertyGetter.IsFinal;

            if (!isVirtualProp)
                throw DrMockException.NonVirtualProperty(genericType);

            var property = Expression.Property(param, propertyMatch.Name);
            var dbSetAccessor = typeof(Func<,>).MakeGenericType(typeof(TContext), dbSetType);
            var lambda = Expression.Lambda(dbSetAccessor, property, param);

            // Aim for mockContext.Setup(lambda).Returns(value);

            var setupMethod = typeof(Mock<TContext>)
                    .GetMethods()
                    .First(m => m.Name == "Setup" && m.GetParameters().Length == 1 && m.IsGenericMethodDefinition)
                    .MakeGenericMethod(dbSetType);

            var setupResult = setupMethod.Invoke(mockContext, new object[] { lambda });

            var returnsMethod = setupResult.GetType()
                .GetMethod("Returns", new[] { dbSetType });

            returnsMethod.Invoke(setupResult, new[] { value });
        }

        public static IEnumerable<Type> GetDbSetTypes<TContext>()
            where TContext : class, IDbContext
        {
            var propertyMatches = typeof(TContext)
                .GetProperties()
                .Where(x => x.PropertyType.Name == typeof(DbSet<>).Name);

            return propertyMatches.Select(x => x.PropertyType.GetGenericArguments().First());
        }

        public static Mock<T> GetMockFromObject<T>(this T mockedObject) where T : class
        {
            PropertyInfo[] propInfo = mockedObject.GetType().GetProperties()
                .Where(
                    p => p.PropertyType.Name == "Mock`1"
                ).ToArray();

            return propInfo.FirstOrDefault().GetGetMethod().Invoke(mockedObject, null) as Mock<T>;
        }

        public static bool HasSharedPropertiesWith<T>(this T item1, T item2) 
            where T : class
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var item1Value = property.GetValue(item1);
                var item2Value = property.GetValue(item2);

                bool isNullable = !property.GetType().IsValueType || Nullable.GetUnderlyingType(property.GetType()) != null;

                if (isNullable && (item1Value is null || item2Value is null)) 
                    continue;

                if (item1Value == item2Value)
                    return true;
            }

            return false;
        }

        public static IEnumerable<Action<T>> ToUpdateActions<T>(this Expression<Func<T, bool>> matcher)
            where T : class
        {
            var deconstructor = new ExpressionDeconstructor<T>();

            deconstructor.Extract(matcher);

            return deconstructor.GetActionExpressions();
        }
    }
}