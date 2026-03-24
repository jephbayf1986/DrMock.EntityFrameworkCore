using DrMock.EfCore.Base;
using DrMock.EfCore.Helpers;
using DrMock.EfCore.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace DrMock.EfCore.Builders
{
    internal class MockDbContextBuilder<TContext> 
        where TContext : class, IDbContext
    {
        private readonly Mock<TContext> _mockContext;
        private readonly Dictionary<Type, MockDbSetBuilder> _mockDbSets;
        private readonly MockDbContextOptions _options;

        public MockDbContextBuilder(MockDbContextOptions options)
        {
            _options = options;
            _mockContext = new Mock<TContext>();
            _mockDbSets = new Dictionary<Type, MockDbSetBuilder>();
        }

        private MockDbContextBuilder(IEnumerable<Type> dbSetTypes, MockDbContextOptions options)
        {
            _options = options;
            _mockContext = new Mock<TContext>();
            _mockDbSets = new Dictionary<Type, MockDbSetBuilder>();

            foreach (var dbSetType in dbSetTypes)
            {
                var mockDbSetType = MockDbSetBuilder.Create(dbSetType, options);

                _mockDbSets.Add(dbSetType, mockDbSetType);
            }
        }

        public static MockDbContextBuilder<TContext> WithAllDbSets(MockDbContextOptions options)
        {
            var types = ReflectionHelper.GetDbSetTypes<TContext>();

            return new MockDbContextBuilder<TContext>(types, options);
        }

        public MockDbContextBuilder<TContext> WithDbSet<T>() 
            where T : class, new() 
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            if (!alreadyExists)
                _mockDbSets.Add(typeof(T), new MockDbSetBuilder<T>(_options));

            return this;
        }

        public MockDbContextBuilder<TContext> WithRandomDataInDbSet<T>(int numberOfItems = 5) 
            where T : class, new()
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.WithRandomData(numberOfItems);

            return this;
        }

        public MockDbContextBuilder<TContext> WithDbSetData<T>(params T[] entities)
            where T : class, new()
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.EnsurePresent(entities);

            return this;
        }

        public MockDbContextBuilder<TContext> EnsureDbSetDataDoesntContain<T>(params T[] entities) 
            where T : class, new()
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.EnsureNotPresent(entities);

            return this;
        }

        public MockDbContextBuilder<TContext> WithDbSetData<T>(Expression<Func<T, bool>> matcher) 
            where T : class, new()
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.EnsurePresent(matcher);

            return this;
        }

        public MockDbContextBuilder<TContext> EnsureDbSetDataDoesntContain<T>(Expression<Func<T, bool>> matcher) 
            where T : class, new()
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.RemoveAny(matcher);

            return this;
        }

        public MockDbContextBuilder<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new()
        {
            _mockContext.Setup(x => x.Add(It.IsAny<T>())).Callback(action);

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.WithActionOnAdd(action);

            return this;
        }

        public MockDbContextBuilder<TContext> WithActionOnAddAsync<T>(Action<T> action)
            where T : class, new()
        {
            _mockContext
                .Setup(x => x.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Callback((T newItem, CancellationToken token) => action(newItem));

            var mockDbSet = GetOrCreateDbSet<T>();

            mockDbSet.WithActionOnAddAsync(action);

            return this;
        }

        public MockDbContextBuilder<TContext> ThrowOnSaveChanges<TEx>()
            where TEx : Exception, new()
        {
            _mockContext.Setup(x => x.SaveChanges())
                        .Throws<TEx>();

            return this;
        }

        public MockDbContextBuilder<TContext> ThrowOnSaveChangesAsync<TEx>()
            where TEx : Exception, new()
        {
            _mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .Throws<TEx>();

            return this;
        }

        public Mock<TContext> Build()
        { 
            foreach (var mockDbSetKeyValue in _mockDbSets)
            {
                _mockContext.SetMockDbSetAttribute(mockDbSetKeyValue.Key, mockDbSetKeyValue.Value.Build());
            }

            return _mockContext;
        } 

        private MockDbSetBuilder<T> GetOrCreateDbSet<T>()
            where T : class, new()
        {
            var alreadyExists = _mockDbSets.ContainsKey(typeof(T));

            if (!alreadyExists)
                _mockDbSets.Add(typeof(T), new MockDbSetBuilder<T>(_options));

            var existsNow = _mockDbSets.TryGetValue(typeof(T), out MockDbSetBuilder dbSetBuilder);

            if (!existsNow)
                throw new IndexOutOfRangeException($"Unable to get or create a DbSet for type {typeof(T).Name}");

            return dbSetBuilder as MockDbSetBuilder<T>;
        }
    }
}