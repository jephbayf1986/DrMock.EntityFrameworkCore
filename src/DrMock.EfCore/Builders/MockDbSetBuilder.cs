using dotRandom;
using DrMock.EfCore.Models;
using DrMock.EfCore.Options;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace DrMock.EfCore.Builders
{
    internal class MockDbSetBuilder 
    {
        public static MockDbSetBuilder Create(Type type, MockDbContextOptions options)
        {
            Type mockDbSetType = typeof(MockDbSetBuilder<>).MakeGenericType(type);

            return Activator.CreateInstance(mockDbSetType, new object[] { options }) as MockDbSetBuilder;
        }

        public virtual object Build() { return null; }
    }

    internal sealed class MockDbSetBuilder<T> : MockDbSetBuilder
        where T : class, new()
    {
        private Mock<DbSet<T>> _mock;
        private readonly MockDbContextOptions _options;
        private readonly ICollection<T> _items;

        public MockDbSetBuilder(MockDbContextOptions options)
        {
            _mock = new Mock<DbSet<T>>();
            _options = options;
            _items = new List<T>();

            if (_options.MinItemsInDbSet.HasValue)
            {
                for (var i = 0; i < _options.MinItemsInDbSet.Value; i++)
                    _items.Add(DotRandom.GenerateRandom<T>());
            }
        }

        public MockDbSetBuilder<T> WithCallBackOnAdd(Action<T> action)
        {
            _mock.Setup(x => x.Add(It.IsAny<T>()))
                 .Callback((T newItem) => action(newItem));

            return this;
        }

        public MockDbSetBuilder<T> WithRandomData(int? numberOfItems = 5)
        {
            for (var i = 0; i < numberOfItems; i++)
                _items.Add(DotRandom.GenerateRandom<T>());

            return this;
        }

        public MockDbSetBuilder<T> EnsurePresent(params T[] entities)
        {
            // TODO : Ensure doesn't already exist

            foreach (var newEntity in entities)
                _items.Add(newEntity);

            return this;
        }

        public MockDbSetBuilder<T> EnsureNotPresent(params T[] entities)
        {
            // TO DO This

            return this;
        }

        public MockDbSetBuilder<T> WithEntityMatch(Expression<Func<T, bool>> matcher)
        {
            // TO DO This

            return this;
        }

        public MockDbSetBuilder<T> WithoutEntityMatch(Expression<Func<T, bool>> matcher)
        {
            // TO DO This

            return this;
        }

        private void SetDbSetData(IEnumerable<T> data)
        {
            var queryableData = data.AsQueryable();

            _mock.As<IAsyncEnumerable<T>>()
               .Setup(m => m.GetAsyncEnumerator(CancellationToken.None))
               .Returns(new TestDbAsyncEnumerator<T>(queryableData.GetEnumerator()));

            _mock.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(queryableData.Provider));

            _mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            _mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            _mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
        }

        public MockDbSetBuilder<T> WithActionOnAdd(Action<T> action)
        {
            _mock.Setup(x => x.Add(It.IsAny<T>())).Callback(action);

            return this;
        }

        public MockDbSetBuilder<T> WithActionOnAddAsync(Action<T> action)
        {
            _mock.Setup(x => x.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>())).Callback(action);

            return this;
        }

        public override object Build()
        {
            SetDbSetData(_items);

            return _mock.Object;
        }
    }
}