using DrMock.EfCore.Models;
using Moq;
using System;
using System.Reflection;

namespace DrMock.EfCore.Exceptions
{
    internal class DrMockException : Exception
    {
        private DrMockException(Type type, EfMethod efMethod, string message)
            : base($"Verify failed on EF property '{type.Name}' for method '{efMethod.ToString()}' with the following error: {message}")
        {
        }

        private DrMockException(EfMethod efMethod, string message)
            : base($"Verify failed on EF for method '{efMethod.ToString()}' with the following error: {message}")
        {
        }

        private DrMockException(string message)
            : base(message)
        {
        }

        public static DrMockException DbSetNotFoundForProperty<T>()
        {
            return DbSetNotFoundForProperty(typeof(T));
        }

        public static DrMockException DbSetNotFoundForProperty(Type type)
        {
            return new DrMockException($"No DbSet of type {type.Name} was found on the DbContext");
        }

        public static DrMockException MultipleProperiesForSameType<T>()
        {
            return MultipleProperiesForSameType(typeof(T));
        }

        public static DrMockException MultipleProperiesForSameType(Type type)
        {
            return new DrMockException($"DrMock limits to one DbSet of each type. Multiple DbSets found for type {type.Name} would lead to unexpected behavoir.");
        }

        public static DrMockException NonVirtualProperty<T>()
        {
            return NonVirtualProperty(typeof(T));
        }

        public static DrMockException NonVirtualProperty(Type type)
        {
            return new DrMockException($"DbSet properties need to be set to Virtual. The DbSet for type {type.Name} is not virtual");
        }

        public static DrMockException CallExpectedNotMade<T>(EfMethod efMethod)
        {
            if (typeof(IDbContext).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                return new DrMockException(efMethod, "Call was expected but never made on the DbContext");

            return new DrMockException(typeof(T), efMethod, "Call was expected but never made on either the DbContext or a valid DbSet");
        }

        public static DrMockException CallMadeOnBothContextAndSet<T>(EfMethod efMethod)
        {
            if (typeof(IDbContext).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                return new DrMockException(efMethod, "Call was made as expected but in multiple ways (ie. both with and without paramter 'acceptAllChangesOnSuccess' defined)");

            return new DrMockException(typeof(T), efMethod, $"Call was made as expected but on both a DbSet AND the DbContext");
        }
        
        public static DrMockException CallMadeIncorrectFrequency<T>(EfMethod efMethod, Times timesExpected, int actualFrequency)
        {
            timesExpected.Deconstruct(out int minExpected, out int maxExpected);

            return new DrMockException(typeof(T), efMethod, $"Call was expected to be made between {minExpected} and {maxExpected} times, but was actually made {actualFrequency} times");
        }
    }
}