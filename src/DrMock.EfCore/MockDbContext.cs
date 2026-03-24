using DrMock.EfCore.Base;
using DrMock.EfCore.Builders;
using DrMock.EfCore.Helpers;
using DrMock.EfCore.Interfaces;
using DrMock.EfCore.Models;
using DrMock.EfCore.Options;
using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DrMock.EfCore
{
    /// <summary>
    /// Mock DbContext
    /// Used to test Entity Framework Core inputs and outputs
    /// </summary>
    /// <typeparam name="TContext">Type of DbContext implementing IDbContext</typeparam>
    public sealed class MockDbContext<TContext> : IMoqDirect<TContext>, IBuilderSteps<TContext>, IVerifyActions, IVerifySave 
        where TContext : class, IDbContext
    {
        private Mock<TContext> _mock = null;
        private MockDbContextBuilder<TContext> _builder;

        /// <summary>
        /// Construct new instance of MockDbContext
        /// </summary>
        /// <param name="options">Options defining MockDbContext behavoir</param>
        public MockDbContext(MockDbContextOptions options = null)
        {
            _builder = new MockDbContextBuilder<TContext>(options ?? new MockDbContextOptions());
        }

        internal MockDbContext(MockDbContextBuilder<TContext> builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Use All Entities
        /// Creates an instance of MockDbContext where each DbSet can be called without exceptions
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Instance of MockDbContext</returns>
        public static MockDbContext<TContext> UseAllEntities(MockDbContextOptions options = null)
        {
            var builder = MockDbContextBuilder<TContext>.WithAllDbSets(options ?? new MockDbContextOptions());

            return new MockDbContext<TContext>(builder);
        }

        /// <summary>
        /// Get Mock DbSet
        /// For performing custom setup and verifications on a specific DbSet
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <returns>MockDbSet Wrapper with all direct Moq methods</returns>
        public MockDbSet<T> GetMockDbSet<T>() where T : class, new()
        {
            if (_mock is null)
                return _builder
                    .GetMockDbSet<T>()
                    .BuildMockDbSet();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            return new MockDbSet<T>(dbSetMock);
        }

        /// <summary>
        /// Set Mock DbSet
        /// For performing custom setup and verifications on a specific DbSet
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="mockDbSet">MockDbSet Wrapper</param>
        public void SetMockDbSet<T>(MockDbSet<T> mockDbSet) where T : class, new()
        {
            if (_mock is null)
            {
                _builder.SetDbSet(mockDbSet);
                return;
            }

            _mock.SetMockDbSetAttribute(mockDbSet.Object);
        }

        public MockDbContext<TContext> UseEntity<T>() where T : class, new()
        {
            _builder = _builder.WithDbSet<T>();

            return this;
        }

        public MockDbContext<TContext> WithRandomDataFor<T>(int numberOfItems = 5) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithRandomDataInDbSet<T>(numberOfItems);

            return this;
        }

        public MockDbContext<TContext> Include<T>(params T[] entities) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithDbSetData(entities);

            return this;
        }

        public MockDbContext<TContext> Exclude<T>(params T[] entities) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithRandomDataInDbSet<T>()
                .EnsureDbSetDataDoesntContain(entities);

            return this;
        }

        public MockDbContext<TContext> Exists<T>(Expression<Func<T, bool>> matcher) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithDbSetData(matcher);

            return this;
        }

        public MockDbContext<TContext> DoesNotExist<T>(Expression<Func<T, bool>> matcher) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithRandomDataInDbSet<T>()
                .EnsureDbSetDataDoesntContain(matcher);

            return this;
        }

        public MockDbContext<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new()
        {
            _builder = _builder.WithActionOnAdd(action);

            return this;
        }

        public MockDbContext<TContext> WithActionOnAddAsync<T>(Action<T> action)
            where T : class, new()
        {
            _builder = _builder.WithActionOnAddAsync(action);

            return this;
        }

        public MockDbContext<TContext> WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new()
        {
            _builder = _builder.ThrowOnSaveChanges<TEx>();

            return this;
        }

        public MockDbContext<TContext> WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new()
        {
            _builder = _builder.ThrowOnSaveChangesAsync<TEx>();

            return this;
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Add(It.Is(match))),
                () => dbSetMock.Verify(x => x.Add(It.Is(match)))
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.Add);
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match, Times times)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Add(It.Is(match)), times),
                () => dbSetMock.Verify(x => x.Add(It.Is(match)), times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Add);
        }

        public void VerifyAddedOnce<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Add(It.Is(match)), Times.Once()),
                () => dbSetMock.Verify(x => x.Add(It.Is(match)), Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Add);
        }

        public void VerifyNeverAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.Verify(x => x.Add(It.Is(match)), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.Add(It.Is(match)), Times.Never);
        }

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>())),
                () => dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()))
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.Add);
        }

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match, Times times)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), times),
                () => dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Add);
        }

        public void VerifyAddedOnceAsync<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Once()),
                () => dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Add);
        }

        public void VerifyNeverAddedAsync<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);
        }

        public void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeAddedAsObjects(matches, Times.AtLeastOnce()),
                () => _mock.VerifyRangeAddedAsClass(matches, Times.AtLeastOnce()),
                () => dbSetMock.VerifyRangeAdded(matches, Times.AtLeastOnce())
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeAddedAsObjects(matches, times),
                () => _mock.VerifyRangeAddedAsClass(matches, times),
                () => dbSetMock.VerifyRangeAdded(matches, times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeAddedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeAddedAsObjects(matches, Times.Once()),
                () => _mock.VerifyRangeAddedAsClass(matches, Times.Once()),
                () => dbSetMock.VerifyRangeAdded(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeNeverAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.VerifyRangeAddedAsObjects(matches, Times.Never());
            _mock.VerifyRangeAddedAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeAdded(matches, Times.Never());
        }

        public void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeAddedAsyncAsObjects(matches, Times.AtLeastOnce()),
                () => _mock.VerifyRangeAddedAsyncAsClass(matches, Times.AtLeastOnce()),
                () => dbSetMock.VerifyRangeAddedAsync(matches, Times.AtLeastOnce())
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeAddedAsyncAsObjects(matches, times),
                () => _mock.VerifyRangeAddedAsyncAsClass(matches, times),
                () => dbSetMock.VerifyRangeAddedAsync(matches, times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeAddedOnceAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                    () => _mock.VerifyRangeAddedAsyncAsObjects(matches, Times.Once()),
                    () => _mock.VerifyRangeAddedAsyncAsClass(matches, Times.Once()),
                    () => dbSetMock.VerifyRangeAddedAsync(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeNeverAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.VerifyRangeAddedAsyncAsObjects(matches, Times.Never());
            _mock.VerifyRangeAddedAsyncAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeAddedAsync(matches, Times.Never());
        }

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Update(It.Is(match))),
                () => dbSetMock.Verify(x => x.Update(It.Is(match)))
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.Update);
        }

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match, Times times) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Update(It.Is(match)), times),
                () => dbSetMock.Verify(x => x.Update(It.Is(match)), times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Update);
        }

        public void VerifyUpdatedOnce<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Update(It.Is(match)), Times.Once()),
                () => dbSetMock.Verify(x => x.Update(It.Is(match)), Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Update);
        }

        public void VerifyNeverUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.Verify(x => x.Update(It.Is(match)), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.Update(It.Is(match)), Times.Never);
        }

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeUpdatedAsObjects(matches, Times.AtLeastOnce()),
                () => _mock.VerifyRangeUpdatedAsClass(matches, Times.AtLeastOnce()),
                () => dbSetMock.VerifyRangeUpdated(matches, Times.AtLeastOnce())
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.UpdateRange);
        }

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeUpdatedAsObjects(matches, times),
                () => _mock.VerifyRangeUpdatedAsClass(matches, times),
                () => dbSetMock.VerifyRangeUpdated(matches, times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.UpdateRange);
        }

        public void VerifyRangeUpdatedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeUpdatedAsObjects(matches, Times.Once()),
                () => _mock.VerifyRangeUpdatedAsClass(matches, Times.Once()),
                () => dbSetMock.VerifyRangeUpdated(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.UpdateRange); 
        }

        public void VerifyRangeNeverUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.VerifyRangeUpdatedAsObjects(matches, Times.Never());
            _mock.VerifyRangeUpdatedAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeUpdated(matches, Times.Never());
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Remove(It.Is(match))),
                () => dbSetMock.Verify(x => x.Remove(It.Is(match)))
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.Remove);
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match, Times times) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Remove(It.Is(match)), times),
                () => dbSetMock.Verify(x => x.Remove(It.Is(match)), times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Remove);
        }

        public void VerifyRemovedOnce<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.Remove(It.Is(match)), Times.Once()),
                () => dbSetMock.Verify(x => x.Remove(It.Is(match)), Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.Remove);
        }

        public void VerifyNeverRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.Verify(x => x.Remove(It.Is(match)), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.Remove(It.Is(match)), Times.Never);
        }

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeRemovedAsObjects(matches, Times.AtLeastOnce()),
                () => _mock.VerifyRangeRemovedAsClass(matches, Times.AtLeastOnce()),
                () => dbSetMock.VerifyRangeRemoved(matches, Times.AtLeastOnce())
            };

            verifications.EnsureAtLeastOnePasses<T>(EfMethod.RemoveRange);
        }

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) 
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeRemovedAsObjects(matches, times),
                () => _mock.VerifyRangeRemovedAsClass(matches, times),
                () => dbSetMock.VerifyRangeRemoved(matches, times)
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.RemoveRange);
        }

        public void VerifyRangeRemovedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            EnsureMockBuilt();

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.VerifyRangeRemovedAsObjects(matches, Times.Once()),
                () => _mock.VerifyRangeRemovedAsClass(matches, Times.Once()),
                () => dbSetMock.VerifyRangeRemoved(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.RemoveRange);
        }

        public void VerifyRangeNeverRemoved<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            EnsureMockBuilt();

            _mock.VerifyRangeRemovedAsObjects(matches, Times.Never());
            _mock.VerifyRangeRemovedAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeRemoved(matches, Times.Never());
        }

        public void VerifyChangesSaved()
        {
            EnsureMockBuilt();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChanges()),
                () => _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()))
            };

            verifications.EnsureAtLeastOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSaved(Times times)
        {
            EnsureMockBuilt();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChanges(), times),
                () => _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), times)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSavedOnce()
        {
            EnsureMockBuilt();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChanges(), Times.Once),
                () => _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), Times.Once)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesNeverSaved()
        {
            EnsureMockBuilt();

            _mock.Verify(x => x.SaveChanges(), Times.Never);
            _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            EnsureMockBuilt();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())),
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            };

            verifications.EnsureAtLeastOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSavedAsync(Times times)
        {
            EnsureMockBuilt();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), times),
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), times)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSavedOnceAsync()
        {
            EnsureMockBuilt();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once),
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesNeverSavedAsync()
        {
            EnsureMockBuilt();

            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        public ISetup<TContext> Setup(Expression<Action<TContext>> expression)
        {
            return _builder.Mock.Setup(expression);
        }

        public ISetup<TContext, TResult> Setup<TResult>(Expression<Func<TContext, TResult>> expression)
        {
            return _builder.Mock.Setup(expression);
        }

        public ISetupGetter<TContext, TProperty> SetupGet<TProperty>(Expression<Func<TContext, TProperty>> expression)
        {
            return _builder.Mock.SetupGet(expression);
        }

        public ISetupSetter<TContext, TProperty> SetupSet<TProperty>(Action<TContext> setterExpression)
        {
            return _builder.Mock.SetupSet<TProperty>(setterExpression);
        }

        public ISetup<TContext> SetupSet(Action<TContext> setterExpression)
        {
            return _builder.Mock.SetupSet(setterExpression);
        }

        public ISetup<TContext> SetupAdd(Action<TContext> addExpression)
        {
            return _builder.Mock.SetupAdd(addExpression);
        }

        public ISetup<TContext> SetupRemove(Action<TContext> removeExpression)
        {
            return _builder.Mock.SetupRemove(removeExpression);
        }

        public Mock<TContext> SetupProperty<TProperty>(Expression<Func<TContext, TProperty>> property)
        {
            return _builder.Mock.SetupProperty(property);
        }

        public Mock<TContext> SetupProperty<TProperty>(Expression<Func<TContext, TProperty>> property, TProperty initialValue)
        {
            return _builder.Mock.SetupProperty(property, initialValue);
        }

        public Mock<TContext> SetupAllProperties()
        {
            return _builder.Mock.SetupAllProperties();
        }

        public ISetupSequentialResult<TResult> SetupSequence<TResult>(Expression<Func<TContext, TResult>> expression)
        {
            return _builder.Mock.SetupSequence(expression);
        }

        public ISetupSequentialAction SetupSequence(Expression<Action<TContext>> expression)
        {
            return _builder.Mock.SetupSequence(expression);
        }

        public ISetupConditionResult<TContext> When(Func<bool> condition)
        {
            return _builder.Mock.When(condition);
        }

        public void Verify(Expression<Action<TContext>> expression)
        {
            EnsureMockBuilt();

            _mock.Verify(expression);
        }

        public void Verify(Expression<Action<TContext>> expression, Times times)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<TContext>> expression, Func<Times> times)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<TContext>> expression, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, failMessage);
        }

        public void Verify(Expression<Action<TContext>> expression, Times times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times, failMessage);
        }

        public void Verify(Expression<Action<TContext>> expression, Func<Times> times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression)
        {
            EnsureMockBuilt();

            _mock.Verify(expression);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Times times)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Func<Times> times)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Func<Times> times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, failMessage);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Times times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression)
        {
            EnsureMockBuilt();

            _mock.VerifyGet(expression);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Times times)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Func<Times> times)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, failMessage);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Times times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Func<Times> times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.Verify(expression, times);
        }

        public void VerifySet(Action<TContext> setterExpression)
        {
            EnsureMockBuilt();

            _mock.VerifySet(setterExpression);
        }

        public void VerifySet(Action<TContext> setterExpression, Times times)
        {
            EnsureMockBuilt();

            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<TContext> setterExpression, Func<Times> times)
        {
            EnsureMockBuilt();

            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<TContext> setterExpression, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifySet(setterExpression, failMessage);
        }

        public void VerifySet(Action<TContext> setterExpression, Times times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifySet(setterExpression, times, failMessage);
        }

        public void VerifySet(Action<TContext> setterExpression, Func<Times> times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifySet(setterExpression, times, failMessage);
        }

        public void VerifyAdd(Action<TContext> addExpression)
        {
            EnsureMockBuilt();

            _mock.VerifyAdd(addExpression);
        }

        public void VerifyAdd(Action<TContext> addExpression, Times times)
        {
            EnsureMockBuilt();

            _mock.VerifyAdd(addExpression, times);
        }

        public void VerifyAdd(Action<TContext> addExpression, Func<Times> times)
        {
            EnsureMockBuilt();

            _mock.VerifyAdd(addExpression, times);
        }

        public void VerifyAdd(Action<TContext> addExpression, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifyAdd(addExpression, failMessage);
        }

        public void VerifyAdd(Action<TContext> addExpression, Times times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifyAdd(addExpression, times, failMessage);
        }

        public void VerifyAdd(Action<TContext> addExpression, Func<Times> times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifyAdd(addExpression, times, failMessage);
        }

        public void VerifyRemove(Action<TContext> removeExpression)
        {
            EnsureMockBuilt();

            _mock.VerifyRemove(removeExpression);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Times times)
        {
            EnsureMockBuilt();

            _mock.VerifyRemove(removeExpression, times);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Func<Times> times)
        {
            EnsureMockBuilt();

            _mock.VerifyRemove(removeExpression, times);
        }

        public void VerifyRemove(Action<TContext> removeExpression, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifyRemove(removeExpression, failMessage);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Times times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifyRemove(removeExpression, times, failMessage);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Func<Times> times, string failMessage)
        {
            EnsureMockBuilt();

            _mock.VerifyRemove(removeExpression, times, failMessage);
        }

        public void VerifyNoOtherCalls()
        {
            EnsureMockBuilt();

            _mock.VerifyNoOtherCalls();
        }

        public void Raise(Action<TContext> eventExpression, EventArgs args)
        {
            // TO DO; FInd out hwat this does

            _mock.Raise(eventExpression, args);
        }

        public void Raise(Action<TContext> eventExpression, params object[] args)
        {
            // TO DO; FInd out hwat this does

            _mock.Raise(eventExpression, args);
        }

        public Task RaiseAsync(Action<TContext> eventExpression, params object[] args)
        {
            // TO DO; FInd out hwat this does

            return _mock.RaiseAsync(eventExpression, args);
        }

        private void EnsureMockBuilt()
        {
            if (_mock is null)
                _mock = _builder.Build();
        }

        /// <summary>
        /// Exposes the mocked DbContext instance.
        /// </summary>
        public TContext Object
        {
            get
            {
                EnsureMockBuilt();

                return _mock.Object;
            }
        }
    }
}