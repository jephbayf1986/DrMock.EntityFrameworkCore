using Moq;

namespace DrMock.EfCore.Interfaces
{
    /// <summary>
    /// Verify Save Interface
    /// </summary>
    public interface IVerifySave
    {
        /// <summary>
        /// Verify Changes Saved
        /// Confirms that SaveChanges was called.
        /// </summary>
        /// <remarks>
        /// Note will not verify successfully when SaveChangesAsync is called.
        /// Use <see cref="VerifyChangesSavedAsync()"/> or other <c>VerifyChangesSavedAsync</c> overloads for that.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesSaved();

        /// <summary>
        /// Verify Changes Saved
        /// Confirms that SaveChanges was called a specific number of times (parameterless or with a boolean).
        /// </summary>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// This does not succeed when only SaveChangesAsync was used.
        /// Use <see cref="VerifyChangesSavedAsync(Times)"/> for asynchronous saves.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesSaved(Times times);

        /// <summary>
        /// Verify Changes Saved Once
        /// Confirms that SaveChanges was called exactly once (parameterless or with a boolean).
        /// </summary>
        /// <remarks>
        /// This does not succeed when only SaveChangesAsync was used.
        /// Use <see cref="VerifyChangesSavedOnceAsync()"/> for asynchronous saves.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesSavedOnce();

        /// <summary>
        /// Verify Changes Never Saved
        /// Confirms that SaveChanges was never called (neither the parameterless nor the boolean overload).
        /// </summary>
        /// <remarks>
        /// This does not consider SaveChangesAsync; use <see cref="VerifyChangesNeverSavedAsync()"/> to assert that no asynchronous save occurred.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesNeverSaved();

        /// <summary>
        /// Verify Changes Saved Async
        /// Confirms that SaveChangesAsync was called at least once (with or without an accept-all-changes flag).
        /// </summary>
        /// <remarks>
        /// This does not succeed when only synchronous SaveChanges was used.
        /// Use <see cref="VerifyChangesSaved()"/> or other <c>VerifyChangesSaved</c> overloads for synchronous saves.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesSavedAsync();

        /// <summary>
        /// Verify Changes Saved Async
        /// Confirms that SaveChangesAsync was called a specific number of times (with or without an accept-all-changes flag).
        /// </summary>
        /// <param name="times">The expected number of invocations, as with Moq's <see cref="Times"/>.</param>
        /// <remarks>
        /// This does not succeed when only synchronous SaveChanges was used.
        /// Use <see cref="VerifyChangesSaved(Times)"/> for synchronous saves.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesSavedAsync(Times times);

        /// <summary>
        /// Verify Changes Saved Once Async
        /// Confirms that SaveChangesAsync was called exactly once (with or without an accept-all-changes flag).
        /// </summary>
        /// <remarks>
        /// This does not succeed when only synchronous SaveChanges was used.
        /// Use <see cref="VerifyChangesSavedOnce()"/> for synchronous saves.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesSavedOnceAsync();

        /// <summary>
        /// Verify Changes Never Saved Async
        /// Confirms that SaveChangesAsync was never called (neither overload).
        /// </summary>
        /// <remarks>
        /// This does not consider SaveChanges; use <see cref="VerifyChangesNeverSaved()"/> to assert that no synchronous save occurred.
        /// </remarks>
        /// <exception cref="MockException">Verification failed.</exception>
        void VerifyChangesNeverSavedAsync();
    }
}
