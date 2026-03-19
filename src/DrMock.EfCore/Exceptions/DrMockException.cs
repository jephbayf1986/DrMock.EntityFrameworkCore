using DrMock.EfCore.Models;
using Moq;
using System;

namespace DrMock.EfCore.Exceptions
{
    internal class DrMockException : Exception
    {
        private DrMockException(Type type, EfMethod efMethod, string message)
            : base($"Verify failed on EF property '{type.Name}' for method '{efMethod.ToString()}' with the following error: {message}")
        {
        }

        private DrMockException(string message)
            : base(message)
        {
        }

        public static DrMockException DbSetNotFoundForProperty<T>()
        {
            return new DrMockException($"No DbSet of type {typeof(T).Name} was found on the DbContext");
        }

        public static DrMockException MultipleProperiesForSameType<T>()
        {
            return new DrMockException($"DrMock limits to one DbSet of each type. Multiple DbSets found for type {typeof(T).Name} would lead to unexpected behavoir.");
        }

        public static DrMockException NonVirtualProperty<T>()
        {
            return new DrMockException($"DbSet properties need to be set to Virtual. The DbSet for type {typeof(T).Name} is not virtual");
        }

        public static DrMockException CallExpectedNotMade<T>(EfMethod efMethod)
        {
            return new DrMockException(typeof(T), efMethod, "Call was expected but never made on either the DbContext or a valid DbSet");
        }

        public static DrMockException CallMadeOnBothContextAndSet<T>(EfMethod efMethod)
        {
            return new DrMockException(typeof(T), efMethod, $"Call was made as expected but on both a DbSet AND the DbContext");
        }
    }
}