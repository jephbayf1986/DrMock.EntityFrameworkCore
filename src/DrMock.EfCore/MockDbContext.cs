using DrMock.EfCore.Builders;
using DrMock.EfCore.Helpers;
using DrMock.EfCore.Interfaces;
using DrMock.EfCore.Models;
using DrMock.EfCore.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace DrMock.EfCore
{
    public sealed class MockDbContext<TContext> : IVerifyActions, IVerifySave where TContext : class, IDbContext
    {
        private readonly Mock<TContext> _mock;
        private readonly MockDbContextOptions _options;

        public MockDbContext(MockDbContextOptions options = null)
        {
            _mock = new Mock<TContext>();

            _options = options ?? new MockDbContextOptions();
        }

        internal MockDbContext(Mock<TContext> mock, MockDbContextOptions options = null)
        {
            _mock = mock;

            _options = options ?? new MockDbContextOptions();
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match, Times? times = null)
            where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.Add(It.Is(match)), times.Value),
                    () => dbSetMock.Verify(x => x.Add(It.Is(match)), times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.Add);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.Add(It.Is(match))),
                    () => dbSetMock.Verify(x => x.Add(It.Is(match)))
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.Add);
            }
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

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match, Times? times = null)
            where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), times.Value),
                    () => dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.Add);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>())),
                    () => dbSetMock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()))
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.Add);
            }
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

        public void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times? times = null) where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.AddRange(It.Is(matches)), times.Value),
                    () => _mock.VerifyRangeAddedAsObjectArray(matches, times.Value),
                    () => _mock.VerifyRangeAddedAsObjectNestedArray(matches, times.Value),
                    () => dbSetMock.Verify(x => x.AddRange(It.Is(matches)), times.Value),
                    () => dbSetMock.VerifyRangeAdded(matches, times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.AddRange(It.Is(matches))),
                    () => _mock.VerifyRangeAddedAsObjectArray(matches, Times.AtLeastOnce()),
                    () => _mock.VerifyRangeAddedAsObjectNestedArray(matches, Times.AtLeastOnce()),
                    () => dbSetMock.Verify(x => x.AddRange(It.Is(matches))),
                    () => dbSetMock.VerifyRangeAdded(matches, Times.AtLeastOnce())
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.AddRange);
            }
        }

        public void VerifyRangeAddedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.AddRange(It.Is(matches)), Times.Once()),
                    () => _mock.VerifyRangeAddedAsObjectArray(matches, Times.Once()),
                    () => _mock.VerifyRangeAddedAsObjectNestedArray(matches, Times.Once()),
                () => dbSetMock.Verify(x => x.AddRange(It.Is(matches)), Times.Once()),
                () => dbSetMock.VerifyRangeAdded(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeNeverAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            _mock.Verify(x => x.AddRange(It.Is(matches)), Times.Never);
            //_mock.VerifyRangeAddedWithParams(matches, Times.Never());

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.AddRange(It.Is(matches)), Times.Never);
            //dbSetMock.VerifyRangeAddedWithParams(matches, Times.Never());

            // TO DO: Decide if this is even needed? ^
        }

        public void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times? times = null) where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), times.Value),
                    () => _mock.VerifyRangeAddedAsyncAsObjectArray(matches, times.Value),
                    () => _mock.VerifyRangeAddedAsyncAsObjectNestedArray(matches, times.Value),
                    () => dbSetMock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), times.Value),
                    () => dbSetMock.VerifyRangeAddedAsync(matches, times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>())),
                    () => _mock.VerifyRangeAddedAsyncAsObjectArray(matches, Times.AtLeastOnce()),
                    () => _mock.VerifyRangeAddedAsyncAsObjectNestedArray(matches, Times.AtLeastOnce()),
                    () => dbSetMock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>())),
                    () => dbSetMock.VerifyRangeAddedAsync(matches, Times.AtLeastOnce())
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.AddRange);
            }
        }

        public void VerifyRangeAddedOnceAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), Times.Once()),
                () => _mock.VerifyRangeAddedAsyncAsObjectArray(matches, Times.Once()),
                () => _mock.VerifyRangeAddedAsyncAsObjectNestedArray(matches, Times.Once()),
                () => dbSetMock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny < CancellationToken >()), Times.Once()),
                () => dbSetMock.VerifyRangeAddedAsync(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.AddRange);
        }

        public void VerifyRangeNeverAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), Times.Never);

            //_mock.Verify(x => x.AddRangeAsync(It.Is(matches.ToArrayPredicate()), It.IsAny<CancellationToken>()), Times.Never);

            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSetMock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), Times.Never);

            //dbSetMock.Verify(x => x.AddRangeAsync(It.Is(matches.ToArrayPredicate()), It.IsAny<CancellationToken>()), Times.Never);
        }

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match, Times? times = null) where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.Update(It.Is(match)), times.Value),
                    () => dbSetMock.Verify(x => x.Update(It.Is(match)), times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.Update);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.Update(It.Is(match))),
                    () => dbSetMock.Verify(x => x.Update(It.Is(match)))
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.Update);
            }
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

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times? times = null) where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.UpdateRange(It.Is(matches)), times.Value),
                    () => _mock.VerifyRangeUpdatedAsObjectArray(matches, times.Value),
                    () => _mock.VerifyRangeUpdatedAsObjectNestedArray(matches, times.Value),
                    () => dbSetMock.Verify(x => x.UpdateRange(It.Is(matches)), times.Value),
                    () => dbSetMock.VerifyRangeUpdated(matches, times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.UpdateRange);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.UpdateRange(It.Is(matches))),
                    () => _mock.VerifyRangeUpdatedAsObjectArray(matches, Times.AtLeastOnce()),
                    () => _mock.VerifyRangeUpdatedAsObjectNestedArray(matches, Times.AtLeastOnce()),
                    () => dbSetMock.Verify(x => x.UpdateRange(It.Is(matches))),
                    () => dbSetMock.VerifyRangeUpdated(matches, Times.AtLeastOnce())
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.UpdateRange);
            }
        }

        public void VerifyRangeUpdatedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.UpdateRange(It.Is(matches)), Times.Once()),
                () => _mock.VerifyRangeUpdatedAsObjectArray(matches, Times.Once()),
                () => _mock.VerifyRangeUpdatedAsObjectNestedArray(matches, Times.Once()),
                () => dbSetMock.Verify(x => x.UpdateRange(It.Is(matches)), Times.Once()),
                () => dbSetMock.VerifyRangeUpdated(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.UpdateRange); 
        }

        public void VerifyRangeNeverUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            _mock.Verify(x => x.UpdateRange(It.Is(matches)), Times.Never);
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match, Times? times = null) where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.Remove(It.Is(match)), times.Value),
                    () => dbSetMock.Verify(x => x.Remove(It.Is(match)), times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.Remove);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.Remove(It.Is(match))),
                    () => dbSetMock.Verify(x => x.Remove(It.Is(match)))
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.Remove);
            }
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

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times? times = null) where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            if (times.HasValue)
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.RemoveRange(It.Is(matches))),
                    () => _mock.VerifyRangeRemovedAsObjectArray(matches, times.Value),
                    () => _mock.VerifyRangeRemovedAsObjectNestedArray(matches, times.Value),
                    () => dbSetMock.Verify(x => x.RemoveRange(It.Is(matches)), times.Value),
                    () => dbSetMock.VerifyRangeRemoved(matches, times.Value)
                };

                verifications.EnsureOnlyOnePasses<T>(EfMethod.RemoveRange);
            }
            else
            {
                List<Action> verifications = new List<Action>()
                {
                    () => _mock.Verify(x => x.RemoveRange(It.Is(matches))),
                    () => _mock.VerifyRangeRemovedAsObjectArray(matches, Times.AtLeastOnce()),
                    () => _mock.VerifyRangeRemovedAsObjectNestedArray(matches, Times.AtLeastOnce()),
                    () => dbSetMock.Verify(x => x.RemoveRange(It.Is(matches))),
                    () => dbSetMock.VerifyRangeRemoved(matches, Times.AtLeastOnce())
                };

                verifications.EnsureAtLeastOnePasses<T>(EfMethod.RemoveRange);
            }
        }

        public void VerifyRangeRemovedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            var dbSetMock = _mock.GetMockDbSetAttribute<TContext, T>();

            List<Action> verifications = new List<Action>()
            {
                () => _mock.Verify(x => x.RemoveRange(It.Is(matches)), Times.Once()),
                    () => _mock.VerifyRangeRemovedAsObjectArray(matches, Times.Once()),
                    () => _mock.VerifyRangeRemovedAsObjectNestedArray(matches, Times.Once()),
                () => dbSetMock.Verify(x => x.RemoveRange(It.Is(matches)), Times.Once()),
                () => dbSetMock.VerifyRangeRemoved(matches, Times.Once())
            };

            verifications.EnsureOnlyOnePasses<T>(EfMethod.RemoveRange);
        }

        public void VerifyRangeNeverRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.RemoveRange(It.Is(match)), Times.Never);
        }

        public void VerifyChangesSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Once);
        }

        public void VerifyChangesNeverSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyChangesNeverSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        public MockDbContext<TContext> WithEntities<T>(params T[] items)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithEntities(items)
                                    .Build();

            _mock.SetMockDbSetAttribute(mockDbSet.Object);

            return this;
        }

        public MockDbContext<TContext> WithEntity<T>(params Action<T>[] actions)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithEntity(actions)
                                    .Build();

            _mock.SetMockDbSetAttribute(mockDbSet.Object);

            return this;
        }

        public MockDbContext<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithRandomData()
                                    .WithCallBackOnAdd(action)
                                    .Build();

            _mock.SetMockDbSetAttribute(mockDbSet.Object);

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

        public TContext Object
        {
            get
            {
                return _mock.Object;
            }
        }
    }
}