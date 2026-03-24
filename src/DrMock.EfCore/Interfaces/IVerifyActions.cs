using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Moq;

namespace DrMock.EfCore.Interfaces
{
    public interface IVerifyActions
    {
        /// <summary>
        /// Verify Added
        /// Confirms that <c>Add</c> was called for an entity matching <paramref name="match"/>, either on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <remarks>
        /// Note will not verify successfully when <c>AddAsync</c>, <c>AddRange</c>, or <c>AddRangeAsync</c> is used instead of <c>Add</c>.
        /// Use <see cref="VerifyAddedAsync{T}(Expression{Func{T, bool}})"/> or <see cref="VerifyRangeAdded{T}(Expression{Func{IEnumerable{T}, bool}})"/> (or their overloads) for those APIs.
        /// </remarks>
        /// <typeparam name="T">Entity type that was added.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Add(newPerson);
        /// _dbContext.People.Add(newPerson);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyAdded&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyAdded<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Added
        /// Confirms that <c>Add</c> was called the given number of times for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was added.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>AddAsync</c>, <c>AddRange</c>, or <c>AddRangeAsync</c> is used instead of <c>Add</c>.
        /// Use <see cref="VerifyAddedAsync{T}(Expression{Func{T, bool}}, Times)"/> or range helpers for those APIs.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Add(newPerson);
        /// _dbContext.People.Add(newPerson);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyAdded&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs", Times.Once());
        /// </code>
        /// </example>
        void VerifyAdded<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        /// <summary>
        /// Verify Added Once
        /// Confirms that <c>Add</c> was called exactly once for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was added.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>AddAsync</c>, <c>AddRange</c>, or <c>AddRangeAsync</c> is used instead of <c>Add</c>.
        /// Use <see cref="VerifyAddedOnceAsync{T}(Expression{Func{T, bool}})"/> or range helpers for those APIs.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Add(newPerson);
        /// _dbContext.People.Add(newPerson);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyAddedOnce&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyAddedOnce<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Never Added
        /// Confirms that <c>Add</c> was never called with an entity matching <paramref name="match"/> on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="match">Predicate identifying the entity that must not have been added.</param>
        /// <remarks>
        /// This does not consider <c>AddAsync</c> or range adds; use <see cref="VerifyNeverAddedAsync{T}(Expression{Func{T, bool}})"/> or <see cref="VerifyRangeNeverAdded{T}(Expression{Func{IEnumerable{T}, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching Add was invoked:
        /// <code>
        /// // e.g. system under test does not call Add with a Person matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyNeverAdded&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyNeverAdded<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Added Async
        /// Confirms that <c>AddAsync</c> was called for an entity matching <paramref name="match"/>, either on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was added.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when synchronous <c>Add</c> or <c>AddRange</c> / <c>AddRangeAsync</c> is used instead.
        /// Use <see cref="VerifyAdded{T}(Expression{Func{T, bool}})"/> or <see cref="VerifyRangeAdded{T}(Expression{Func{IEnumerable{T}, bool}})"/> (or overloads) for those APIs.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// await _dbContext.AddAsync(newPerson);
        /// await _dbContext.People.AddAsync(newPerson);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyAddedAsync&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Added Async
        /// Confirms that <c>AddAsync</c> was called the given number of times for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was added.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when synchronous <c>Add</c> or range adds are used instead.
        /// Use <see cref="VerifyAdded{T}(Expression{Func{T, bool}}, Times)"/> or range async helpers for those APIs.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// await _dbContext.AddAsync(newPerson);
        /// await _dbContext.People.AddAsync(newPerson);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyAddedAsync&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs", Times.Once());
        /// </code>
        /// </example>
        void VerifyAddedAsync<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        /// <summary>
        /// Verify Added Once Async
        /// Confirms that <c>AddAsync</c> was called exactly once for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was added.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when synchronous <c>Add</c> or range adds are used instead.
        /// Use <see cref="VerifyAddedOnce{T}(Expression{Func{T, bool}})"/> or range helpers for those APIs.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// await _dbContext.AddAsync(newPerson);
        /// await _dbContext.People.AddAsync(newPerson);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyAddedOnceAsync&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyAddedOnceAsync<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Never Added Async
        /// Confirms that <c>AddAsync</c> was never called with an entity matching <paramref name="match"/> on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="match">Predicate identifying the entity that must not have been added.</param>
        /// <remarks>
        /// This does not consider synchronous <c>Add</c> or range adds; use <see cref="VerifyNeverAdded{T}(Expression{Func{T, bool}})"/> or <see cref="VerifyRangeNeverAddedAsync{T}(Expression{Func{IEnumerable{T}, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching AddAsync was invoked:
        /// <code>
        /// // e.g. system under test does not call AddAsync with a Person matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyNeverAddedAsync&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyNeverAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Range Added
        /// Confirms that <c>AddRange</c> was called with a sequence matching <paramref name="matches"/>, via the context (objects or concrete collection) or <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Add</c> / <c>AddAsync</c> or the other range API shape is used instead.
        /// Use <see cref="VerifyAdded{T}(Expression{Func{T, bool}})"/> or <see cref="VerifyAddedAsync{T}(Expression{Func{T, bool}})"/> (or overloads) for single-entity adds.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.AddRange(newPeople);
        /// _dbContext.People.AddRange(newPeople);
        /// _dbContext.AddRange(person1, person2);
        /// _dbContext.People.AddRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeAdded&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Added
        /// Confirms that <c>AddRange</c> was called the given number of times with a sequence matching <paramref name="matches"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity adds are used instead; use <see cref="VerifyAdded{T}(Expression{Func{T, bool}}, Times)"/> or async range overloads as appropriate.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.AddRange(newPeople);
        /// _dbContext.People.AddRange(newPeople);
        /// _dbContext.AddRange(person1, person2);
        /// _dbContext.People.AddRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeAdded&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"), Times.Once());
        /// </code>
        /// </example>
        void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new();

        /// <summary>
        /// Verify Range Added Once
        /// Confirms that <c>AddRange</c> was called exactly once with a sequence matching <paramref name="matches"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity adds are used instead; use <see cref="VerifyAddedOnce{T}(Expression{Func{T, bool}})"/> or async range overloads as appropriate.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.AddRange(newPeople);
        /// _dbContext.People.AddRange(newPeople);
        /// _dbContext.AddRange(person1, person2);
        /// _dbContext.People.AddRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeAddedOnce&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeAddedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Never Added
        /// Confirms that <c>AddRange</c> was never invoked with a sequence matching <paramref name="matches"/> on any supported path (context objects, context collection, or <c>DbSet&lt;T&gt;</c>).
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate identifying the sequence that must not have been added.</param>
        /// <remarks>
        /// This does not consider <c>AddRangeAsync</c> or single-entity adds; use <see cref="VerifyRangeNeverAddedAsync{T}(Expression{Func{IEnumerable{T}, bool}})"/>, <see cref="VerifyNeverAdded{T}(Expression{Func{T, bool}})"/>, or <see cref="VerifyNeverAddedAsync{T}(Expression{Func{T, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching AddRange was invoked:
        /// <code>
        /// // e.g. system under test does not call AddRange with a sequence matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeNeverAdded&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeNeverAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Added Async
        /// Confirms that <c>AddRangeAsync</c> was called with a sequence matching <paramref name="matches"/>, via the context or <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when synchronous <c>AddRange</c> or single-entity adds are used instead.
        /// Use <see cref="VerifyRangeAdded{T}(Expression{Func{IEnumerable{T}, bool}})"/> or <see cref="VerifyAdded{T}(Expression{Func{T, bool}})"/> (or overloads) for those APIs.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// await _dbContext.AddRangeAsync(newPeople);
        /// await _dbContext.People.AddRangeAsync(newPeople);
        /// await _dbContext.AddRangeAsync(person1, person2);
        /// await _dbContext.People.AddRangeAsync(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeAddedAsync&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Added Async
        /// Confirms that <c>AddRangeAsync</c> was called the given number of times with a sequence matching <paramref name="matches"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when synchronous range or single-entity adds are used instead.
        /// Use <see cref="VerifyRangeAdded{T}(Expression{Func{IEnumerable{T}, bool}}, Times)"/> or single-entity helpers as appropriate.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// await _dbContext.AddRangeAsync(newPeople);
        /// await _dbContext.People.AddRangeAsync(newPeople);
        /// await _dbContext.AddRangeAsync(person1, person2);
        /// await _dbContext.People.AddRangeAsync(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeAddedAsync&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"), Times.Once());
        /// </code>
        /// </example>
        void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new();

        /// <summary>
        /// Verify Range Added Once Async
        /// Confirms that <c>AddRangeAsync</c> was called exactly once with a sequence matching <paramref name="matches"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when synchronous range or single-entity adds are used instead.
        /// Use <see cref="VerifyRangeAddedOnce{T}(Expression{Func{IEnumerable{T}, bool}})"/> or single-entity helpers as appropriate.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// await _dbContext.AddRangeAsync(newPeople);
        /// await _dbContext.People.AddRangeAsync(newPeople);
        /// await _dbContext.AddRangeAsync(person1, person2);
        /// await _dbContext.People.AddRangeAsync(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeAddedOnceAsync&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeAddedOnceAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Never Added Async
        /// Confirms that <c>AddRangeAsync</c> was never invoked with a sequence matching <paramref name="matches"/> on any supported path.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate identifying the sequence that must not have been added.</param>
        /// <remarks>
        /// This does not consider synchronous <c>AddRange</c> or single-entity adds; use <see cref="VerifyRangeNeverAdded{T}(Expression{Func{IEnumerable{T}, bool}})"/> or <see cref="VerifyNeverAddedAsync{T}(Expression{Func{T, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching AddRangeAsync was invoked:
        /// <code>
        /// // e.g. system under test does not call AddRangeAsync with a sequence matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeNeverAddedAsync&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeNeverAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Updated
        /// Confirms that <c>Update</c> was called for an entity matching <paramref name="match"/>, either on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was updated.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>UpdateRange</c> is used instead; use <see cref="VerifyRangeUpdated{T}(Expression{Func{IEnumerable{T}, bool}})"/> (or overloads) for range updates.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Update(person);
        /// _dbContext.People.Update(person);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyUpdated&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyUpdated<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Updated
        /// Confirms that <c>Update</c> was called the given number of times for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was updated.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>UpdateRange</c> is used instead; use <see cref="VerifyRangeUpdated{T}(Expression{Func{IEnumerable{T}, bool}}, Times)"/> for range updates.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Update(person);
        /// _dbContext.People.Update(person);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyUpdated&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs", Times.Once());
        /// </code>
        /// </example>
        void VerifyUpdated<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        /// <summary>
        /// Verify Updated Once
        /// Confirms that <c>Update</c> was called exactly once for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was updated.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>UpdateRange</c> is used instead; use <see cref="VerifyRangeUpdatedOnce{T}(Expression{Func{IEnumerable{T}, bool}})"/> for range updates.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Update(person);
        /// _dbContext.People.Update(person);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyUpdatedOnce&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyUpdatedOnce<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Never Updated
        /// Confirms that <c>Update</c> was never called with an entity matching <paramref name="match"/> on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="match">Predicate identifying the entity that must not have been updated.</param>
        /// <remarks>
        /// This does not consider <c>UpdateRange</c>; use <see cref="VerifyRangeNeverUpdated{T}(Expression{Func{IEnumerable{T}, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching Update was invoked:
        /// <code>
        /// // e.g. system under test does not call Update with a Person matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyNeverUpdated&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyNeverUpdated<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Range Updated
        /// Confirms that <c>UpdateRange</c> was called with a sequence matching <paramref name="matches"/>, via the context or <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Update</c> is used instead; use <see cref="VerifyUpdated{T}(Expression{Func{T, bool}})"/> (or overloads) for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.UpdateRange(people);
        /// _dbContext.People.UpdateRange(people);
        /// _dbContext.UpdateRange(person1, person2);
        /// _dbContext.People.UpdateRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeUpdated&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Updated
        /// Confirms that <c>UpdateRange</c> was called the given number of times with a sequence matching <paramref name="matches"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Update</c> is used instead; use <see cref="VerifyUpdated{T}(Expression{Func{T, bool}}, Times)"/> for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.UpdateRange(people);
        /// _dbContext.People.UpdateRange(people);
        /// _dbContext.UpdateRange(person1, person2);
        /// _dbContext.People.UpdateRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeUpdated&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"), Times.Once());
        /// </code>
        /// </example>
        void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new();

        /// <summary>
        /// Verify Range Updated Once
        /// Confirms that <c>UpdateRange</c> was called exactly once with a sequence matching <paramref name="matches"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Update</c> is used instead; use <see cref="VerifyUpdatedOnce{T}(Expression{Func{T, bool}})"/> for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.UpdateRange(people);
        /// _dbContext.People.UpdateRange(people);
        /// _dbContext.UpdateRange(person1, person2);
        /// _dbContext.People.UpdateRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeUpdatedOnce&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeUpdatedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Range Never Updated
        /// Confirms that <c>UpdateRange</c> was never invoked with a sequence matching <paramref name="matches"/> on any supported path.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="matches">Predicate identifying the sequence that must not have been updated.</param>
        /// <remarks>
        /// This does not consider single-entity <c>Update</c>; use <see cref="VerifyNeverUpdated{T}(Expression{Func{T, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching UpdateRange was invoked:
        /// <code>
        /// // e.g. system under test does not call UpdateRange with a sequence matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeNeverUpdated&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeNeverUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        /// <summary>
        /// Verify Removed
        /// Confirms that <c>Remove</c> was called for an entity matching <paramref name="match"/>, either on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was removed.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>RemoveRange</c> is used instead; use <see cref="VerifyRangeRemoved{T}(Expression{Func{IEnumerable{T}, bool}})"/> (or overloads) for range removes.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Remove(person);
        /// _dbContext.People.Remove(person);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRemoved&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyRemoved<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Removed
        /// Confirms that <c>Remove</c> was called the given number of times for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was removed.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>RemoveRange</c> is used instead; use <see cref="VerifyRangeRemoved{T}(Expression{Func{IEnumerable{T}, bool}}, Times)"/> for range removes.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Remove(person);
        /// _dbContext.People.Remove(person);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRemoved&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs", Times.Once());
        /// </code>
        /// </example>
        void VerifyRemoved<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        /// <summary>
        /// Verify Removed Once
        /// Confirms that <c>Remove</c> was called exactly once for an entity matching <paramref name="match"/>, on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type that was removed.</typeparam>
        /// <param name="match">Predicate the passed entity must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when <c>RemoveRange</c> is used instead; use <see cref="VerifyRangeRemovedOnce{T}(Expression{Func{IEnumerable{T}, bool}})"/> for range removes.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (either will work):
        /// <code>
        /// _dbContext.Remove(person);
        /// _dbContext.People.Remove(person);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRemovedOnce&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyRemovedOnce<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Never Removed
        /// Confirms that <c>Remove</c> was never called with an entity matching <paramref name="match"/> on the context or on <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="match">Predicate identifying the entity that must not have been removed.</param>
        /// <remarks>
        /// This does not consider <c>RemoveRange</c>; use <see cref="VerifyRangeNeverRemoved{T}(Expression{Func{IEnumerable{T}, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching Remove was invoked:
        /// <code>
        /// // e.g. system under test does not call Remove with a Person matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyNeverRemoved&lt;Person&gt;(x => x.FirstName == "Joe" &amp;&amp; x.LastName == "Bloggs");
        /// </code>
        /// </example>
        void VerifyNeverRemoved<T>(Expression<Func<T, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Range Removed
        /// Confirms that <c>RemoveRange</c> was called with a sequence matching <paramref name="match"/>, via the context or <c>DbSet&lt;T&gt;</c>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="match">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Remove</c> is used instead; use <see cref="VerifyRemoved{T}(Expression{Func{T, bool}})"/> (or overloads) for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.RemoveRange(people);
        /// _dbContext.People.RemoveRange(people);
        /// _dbContext.RemoveRange(person1, person2);
        /// _dbContext.People.RemoveRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeRemoved&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Range Removed
        /// Confirms that <c>RemoveRange</c> was called the given number of times with a sequence matching <paramref name="match"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="match">Predicate the passed enumerable must satisfy.</param>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Remove</c> is used instead; use <see cref="VerifyRemoved{T}(Expression{Func{T, bool}}, Times)"/> for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.RemoveRange(people);
        /// _dbContext.People.RemoveRange(people);
        /// _dbContext.RemoveRange(person1, person2);
        /// _dbContext.People.RemoveRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeRemoved&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"), Times.Once());
        /// </code>
        /// </example>
        void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match, Times times) where T : class, new();

        /// <summary>
        /// Verify Range Removed Once
        /// Confirms that <c>RemoveRange</c> was called exactly once with a sequence matching <paramref name="match"/>.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="match">Predicate the passed enumerable must satisfy.</param>
        /// <remarks>
        /// Note will not verify successfully when single-entity <c>Remove</c> is used instead; use <see cref="VerifyRemovedOnce{T}(Expression{Func{T, bool}})"/> for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification (any one will work):
        /// <code>
        /// _dbContext.RemoveRange(people);
        /// _dbContext.People.RemoveRange(people);
        /// _dbContext.RemoveRange(person1, person2);
        /// _dbContext.People.RemoveRange(person1, person2);
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeRemovedOnce&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeRemovedOnce<T>(Expression<Func<IEnumerable<T>, bool>> match) where T : class, new();

        /// <summary>
        /// Verify Range Never Removed
        /// Confirms that <c>RemoveRange</c> was never invoked with a sequence matching <paramref name="match"/> on any supported path.
        /// </summary>
        /// <typeparam name="T">Entity type in the range.</typeparam>
        /// <param name="match">Predicate identifying the sequence that must not have been removed.</param>
        /// <remarks>
        /// This does not consider single-entity <c>Remove</c>; use <see cref="VerifyNeverRemoved{T}(Expression{Func{T, bool}})"/> when applicable.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        /// <example>
        /// The following will satisfy verification when no matching RemoveRange was invoked:
        /// <code>
        /// // e.g. system under test does not call RemoveRange with a sequence matching the predicate
        /// </code>
        /// The following is used to verify the call
        /// <code>
        /// _mockContext.VerifyRangeNeverRemoved&lt;Person&gt;(xs => xs.Any(p => p.FirstName == "Joe" &amp;&amp; p.LastName == "Bloggs"));
        /// </code>
        /// </example>
        void VerifyRangeNeverRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match) where T : class, new();
    }
}
