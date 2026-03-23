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
    public sealed class MockDbContext<TContext> : IMoqDirect<TContext>, IBuilderSteps<TContext>, IVerifyActions, IVerifySave 
        where TContext : class, IDbContext
    {
        private Mock<TContext> _mock;
        private MockDbContextBuilder<TContext> _builder;

        public MockDbContext(MockDbContextOptions options = null)
        {
            _builder = new MockDbContextBuilder<TContext>(options ?? new MockDbContextOptions());
        }

        internal MockDbContext(MockDbContextBuilder<TContext> builder)
        {
            _builder = builder;
        }

        public static MockDbContext<TContext> UseAllEntities(MockDbContextOptions options = null)
        {
            var builder = MockDbContextBuilder<TContext>.WithAllDbSets(options ?? new MockDbContextOptions());

            return new MockDbContext<TContext>(builder);
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

        public MockDbContext<TContext> WithExistingEntities<T>(params T[] entities) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithDbSetData(entities);

            return this;
        }

        public MockDbContext<TContext> WithoutNotExistingEntities<T>(params T[] entities) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithRandomDataInDbSet<T>()
                .EnsureDbSetDataDoesntContain(entities);

            return this;
        }

        public MockDbContext<TContext> WithExistingEntity<T>(Expression<Func<T, bool>> matcher) where T : class, new()
        {
            _builder = _builder
                .WithDbSet<T>()
                .WithDbSetData(matcher);

            return this;
        }

        public MockDbContext<TContext> WithoutNotExistingEntity<T>(Expression<Func<T, bool>> matcher) where T : class, new()
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
            _mock.Setup(x => x.SaveChanges())
                 .Throws<TEx>();

            return this;
        }

        public MockDbContext<TContext> WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .Throws<TEx>();

            return this;
        }

        public MockDbSet<T> GetMockDbSet<T>() where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            return new MockDbSet<T>(dbSetMock);
        }

        public void SetMockDbSet<T>(MockDbSet<T> mockDbSet) where T : class, new()
        {
            _mock.SetMockDbSetAttribute(mockDbSet.Object);
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
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
            _mock.Verify(x => x.Add(It.Is(match)), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.Add(It.Is(match)), Times.Never);
        }

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
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
            _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);
        }

        public void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
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
            _mock.VerifyRangeAddedAsObjects(matches, Times.Never());
            _mock.VerifyRangeAddedAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeAdded(matches, Times.Never());
        }

        public void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
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
            _mock.VerifyRangeAddedAsyncAsObjects(matches, Times.Never());
            _mock.VerifyRangeAddedAsyncAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeAddedAsync(matches, Times.Never());
        }

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
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
            _mock.Verify(x => x.Update(It.Is(match)), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.Update(It.Is(match)), Times.Never);
        }

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
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
            _mock.VerifyRangeUpdatedAsObjects(matches, Times.Never());
            _mock.VerifyRangeUpdatedAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeUpdated(matches, Times.Never());
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
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
            _mock.Verify(x => x.Remove(It.Is(match)), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.Remove(It.Is(match)), Times.Never);
        }

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
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
            _mock.VerifyRangeRemovedAsObjects(matches, Times.Never());
            _mock.VerifyRangeRemovedAsClass(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.VerifyRangeRemoved(matches, Times.Never());
        }

        public void VerifyChangesSaved()
        {
            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChanges()),
                () => _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()))
            };

            verifications.EnsureAtLeastOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSaved(Times times)
        {
            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChanges(), times),
                () => _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), times)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSavedOnce()
        {
            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChanges(), Times.Once),
                () => _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), Times.Once)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesNeverSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
            _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())),
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            };

            verifications.EnsureAtLeastOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSavedAsync(Times times)
        {
            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), times),
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), times)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesSavedOnceAsync()
        {
            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once),
                () => _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once)
            };

            verifications.EnsureOnlyOnePasses<TContext>(EfMethod.SaveChanges);
        }

        public void VerifyChangesNeverSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        public ISetup<TContext> Setup(Expression<Action<TContext>> expression)
        {
            return _mock.Setup(expression);
        }

        public ISetup<TContext, TResult> Setup<TResult>(Expression<Func<TContext, TResult>> expression)
        {
            return _mock.Setup(expression);
        }

        public ISetupGetter<TContext, TProperty> SetupGet<TProperty>(Expression<Func<TContext, TProperty>> expression)
        {
            return _mock.SetupGet(expression);
        }

        public ISetupSetter<TContext, TProperty> SetupSet<TProperty>(Action<TContext> setterExpression)
        {
            return _mock.SetupSet<TProperty>(setterExpression);
        }

        public ISetup<TContext> SetupSet(Action<TContext> setterExpression)
        {
            return _mock.SetupSet(setterExpression);
        }

        public ISetup<TContext> SetupAdd(Action<TContext> addExpression)
        {
            return _mock.SetupAdd(addExpression);
        }

        public ISetup<TContext> SetupRemove(Action<TContext> removeExpression)
        {
            return _mock.SetupRemove(removeExpression);
        }

        public Mock<TContext> SetupProperty<TProperty>(Expression<Func<TContext, TProperty>> property)
        {
            return _mock.SetupProperty(property);
        }

        public Mock<TContext> SetupProperty<TProperty>(Expression<Func<TContext, TProperty>> property, TProperty initialValue)
        {
            return _mock.SetupProperty(property, initialValue);
        }

        public Mock<TContext> SetupAllProperties()
        {
            return _mock.SetupAllProperties();
        }

        public ISetupSequentialResult<TResult> SetupSequence<TResult>(Expression<Func<TContext, TResult>> expression)
        {
            return _mock.SetupSequence(expression);
        }

        public ISetupSequentialAction SetupSequence(Expression<Action<TContext>> expression)
        {
            return _mock.SetupSequence(expression);
        }

        public ISetupConditionResult<TContext> When(Func<bool> condition)
        {
            return _mock.When(condition);
        }

        public void Verify(Expression<Action<TContext>> expression)
        {
            _mock.Verify(expression);
        }

        public void Verify(Expression<Action<TContext>> expression, Times times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<TContext>> expression, Func<Times> times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<TContext>> expression, string failMessage)
        {
            _mock.Verify(expression, failMessage);
        }

        public void Verify(Expression<Action<TContext>> expression, Times times, string failMessage)
        {
            _mock.Verify(expression, times, failMessage);
        }

        public void Verify(Expression<Action<TContext>> expression, Func<Times> times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression)
        {
            _mock.Verify(expression);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Times times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Func<Times> times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Func<Times> times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, string failMessage)
        {
            _mock.Verify(expression, failMessage);
        }

        public void Verify<TResult>(Expression<Func<TContext, TResult>> expression, Times times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression)
        {
            _mock.VerifyGet(expression);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Times times)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Func<Times> times)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, string failMessage)
        {
            _mock.Verify(expression, failMessage);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Times times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<TContext, TProperty>> expression, Func<Times> times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void VerifySet(Action<TContext> setterExpression)
        {
            _mock.VerifySet(setterExpression);
        }

        public void VerifySet(Action<TContext> setterExpression, Times times)
        {
            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<TContext> setterExpression, Func<Times> times)
        {
            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<TContext> setterExpression, string failMessage)
        {
            _mock.VerifySet(setterExpression, failMessage);
        }

        public void VerifySet(Action<TContext> setterExpression, Times times, string failMessage)
        {
            _mock.VerifySet(setterExpression, times, failMessage);
        }

        public void VerifySet(Action<TContext> setterExpression, Func<Times> times, string failMessage)
        {
            _mock.VerifySet(setterExpression, times, failMessage);
        }

        public void VerifyAdd(Action<TContext> addExpression)
        {
            _mock.VerifyAdd(addExpression);
        }

        public void VerifyAdd(Action<TContext> addExpression, Times times)
        {
            _mock.VerifyAdd(addExpression, times);
        }

        public void VerifyAdd(Action<TContext> addExpression, Func<Times> times)
        {
            _mock.VerifyAdd(addExpression, times);
        }

        public void VerifyAdd(Action<TContext> addExpression, string failMessage)
        {
            _mock.VerifyAdd(addExpression, failMessage);
        }

        public void VerifyAdd(Action<TContext> addExpression, Times times, string failMessage)
        {
            _mock.VerifyAdd(addExpression, times, failMessage);
        }

        public void VerifyAdd(Action<TContext> addExpression, Func<Times> times, string failMessage)
        {
            _mock.VerifyAdd(addExpression, times, failMessage);
        }

        public void VerifyRemove(Action<TContext> removeExpression)
        {
            _mock.VerifyRemove(removeExpression);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Times times)
        {
            _mock.VerifyRemove(removeExpression, times);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Func<Times> times)
        {
            _mock.VerifyRemove(removeExpression, times);
        }

        public void VerifyRemove(Action<TContext> removeExpression, string failMessage)
        {
            _mock.VerifyRemove(removeExpression, failMessage);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Times times, string failMessage)
        {
            _mock.VerifyRemove(removeExpression, times, failMessage);
        }

        public void VerifyRemove(Action<TContext> removeExpression, Func<Times> times, string failMessage)
        {
            _mock.VerifyRemove(removeExpression, times, failMessage);
        }

        public void VerifyNoOtherCalls()
        {
            _mock.VerifyNoOtherCalls();
        }

        public void Raise(Action<TContext> eventExpression, EventArgs args)
        {
            _mock.Raise(eventExpression, args);
        }

        public void Raise(Action<TContext> eventExpression, params object[] args)
        {
            _mock.Raise(eventExpression, args);
        }

        public Task RaiseAsync(Action<TContext> eventExpression, params object[] args)
        {
            return _mock.RaiseAsync(eventExpression, args);
        }

        public TContext Object
        {
            get
            {
                _mock = _builder.Build();

                return _mock.Object;
            }
        }
    }
}