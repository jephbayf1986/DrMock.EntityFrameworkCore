using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrMock.EfCore.Interfaces
{
    public interface IMoqDirect<T> where T : class
    {
        /// <summary>
        /// Setup - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to a <see langword="void"/> method.
        /// </summary>
        /// <param name="expression">Lambda expression that specifies the expected method invocation.</param>
        /// <remarks>
        ///   If more than one setup is specified for the same method or property,
        ///   the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     var mock = new Mock&lt;IProcessor&gt;();
        ///     mock.Setup(x => x.Execute("ping"));
        ///   </code>
        /// </example>
        ISetup<T> Setup(Expression<Action<T>> expression);

        /// <summary>
        /// Setup - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to a non-<see langword="void"/> (value-returning) method.
        /// </summary>
        /// <param name="expression">Lambda expression that specifies the method invocation.</param>
        /// <typeparam name="TResult">Type of the return value. Typically omitted as it can be inferred from the expression.</typeparam>
        /// <remarks>
        ///   If more than one setup is specified for the same method or property,
        ///   the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     mock.Setup(x => x.HasInventory("Talisker", 50))
        ///         .Returns(true);
        ///   </code>
        /// </example>
        ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression);

        /// <summary>
        /// SetupGet - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to a property getter.
        /// </summary>
        /// <param name="expression">Lambda expression that specifies the property getter.</param>
        /// <typeparam name="TProperty">Type of the property. Typically omitted as it can be inferred from the expression.</typeparam>
        /// <remarks>
        ///   If more than one setup is set for the same property getter,
        ///   the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     mock.SetupGet(x => x.Suspended)
        ///         .Returns(true);
        ///   </code>
        /// </example>
        ISetupGetter<T, TProperty> SetupGet<TProperty>(Expression<Func<T, TProperty>> expression);

        /// <summary>
        /// SetupSet - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to a property setter.
        /// </summary>
        /// <param name="setterExpression">The Lambda expression that sets a property to a value.</param>
        /// <typeparam name="TProperty">Type of the property.</typeparam>
        /// <remarks>
        ///   If more than one setup is set for the same property setter,
        ///   the latest one wins and is the one that will be executed.
        ///   <para>
        ///     This overloads allows the use of a callback already typed for the property type.
        ///   </para>
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     mock.SetupSet(x => x.Suspended = true);
        ///   </code>
        /// </example>
        ISetupSetter<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression);

        /// <summary>
        /// SetupSet - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to a property setter.
        /// </summary>
        /// <param name="setterExpression">Lambda expression that sets a property to a value.</param>
        /// <remarks>
        ///   If more than one setup is set for the same property setter,
        ///   the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     mock.SetupSet(x => x.Suspended = true);
        ///   </code>
        /// </example>
        ISetup<T> SetupSet(Action<T> setterExpression);

        /// <summary>
        /// SetupAdd - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to an event add.
        /// </summary>
        /// <param name="addExpression">Lambda expression that adds an event.</param>
        /// <remarks>
        ///   If more than one setup is set for the same event add,
        ///   the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     mock.SetupAdd(x => x.EventHandler += (s, e) => {});
        ///   </code>
        /// </example>
        ISetup<T> SetupAdd(Action<T> addExpression);

        /// <summary>
        /// SetupRemove - Direct Moq Method
        /// Specifies a setup on the mocked type for a call to an event remove.
        /// </summary>
        /// <param name="removeExpression">Lambda expression that removes an event.</param>
        /// <remarks>
        ///   If more than one setup is set for the same event remove,
        ///   the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <example group="setups">
        ///   <code>
        ///     mock.SetupRemove(x => x.EventHandler -= (s, e) => {});
        ///   </code>
        /// </example>
        ISetup<T> SetupRemove(Action<T> removeExpression);

        /// <summary>
        /// SetupProperty - Direct Moq Method
        /// Specifies that the given property should have "property behavior",
        /// meaning that setting its value will cause it to be saved and later returned when the property is requested.
        /// (This is also known as "stubbing".)
        /// </summary>
        /// <param name="property">Property expression to stub.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property, inferred from the property expression (does not need to be specified).
        /// </typeparam>
        /// <example group="setups">
        ///   If you have an interface with an int property <c>Value</c>,
        ///   you might stub it using the following straightforward call:
        ///   <code>
        ///     var mock = new Mock&lt;IHaveValue&gt;();
        ///     mock.SetupProperty(v => v.Value);
        ///   </code>
        ///   After the <c>SetupProperty</c> call has been issued, setting and retrieving
        ///   the object value will behave as expected:
        ///   <code>
        ///     IHaveValue v = mock.Object;
        ///     v.Value = 5;
        ///     Assert.Equal(5, v.Value);
        ///   </code>
        /// </example>
        Mock<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> property);

        /// <summary>
        /// SetupProperty - Direct Moq Method
        /// Specifies that the given property should have "property behavior",
        /// meaning that setting its value will cause it to be saved and later returned when the property is requested.
        /// This overload allows setting the initial value for the property.
        /// (This is also known as "stubbing".)
        /// </summary>
        /// <param name="property">Property expression to stub.</param>
        /// <param name="initialValue">Initial value for the property.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property, inferred from the property expression (does not need to be specified).
        /// </typeparam>
        /// <example group="setups">
        ///   If you have an interface with an int property <c>Value</c>,
        ///   you might stub it using the following straightforward call:
        ///   <code>
        ///     var mock = new Mock&lt;IHaveValue&gt;();
        ///     mock.SetupProperty(v => v.Value, 5);
        ///   </code>
        ///   After the <c>SetupProperty</c> call has been issued, setting and retrieving the object value
        ///   will behave as expected:
        ///   <code>
        ///     IHaveValue v = mock.Object;
        ///     Assert.Equal(5, v.Value); // Initial value was stored
        ///
        ///     // New value set which changes the initial value
        ///     v.Value = 6;
        ///     Assert.Equal(6, v.Value);
        ///   </code>
        /// </example>
        Mock<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> property, TProperty initialValue);

        /// <summary>
        /// SetupAllProperties - Direct Moq Method
        /// Specifies that the all properties on the mock should have "property behavior",
        /// meaning that setting their value will cause them to be saved and later returned when the properties is requested.
        /// (This is also known as "stubbing".)
        /// The default value for each property will be the one generated as specified by the <see cref="P:Moq.Mock.DefaultValue"/>
        /// property for the mock.
        /// </summary>
        /// <remarks>
        ///   If the mock's <see cref="P:Moq.Mock.DefaultValue"/> is set to <see cref="F:Moq.DefaultValue.Mock"/>,
        ///   the mocked default values will also get all properties setup recursively.
        /// </remarks>
        Mock<T> SetupAllProperties();

        /// <summary>
        /// SetupSequence - Direct Moq Method
        /// Return a sequence of values, once per call.
        /// </summary>
        ISetupSequentialResult<TResult> SetupSequence<TResult>(Expression<Func<T, TResult>> expression);

        /// <summary>
        /// SetupSequence - Direct Moq Method
        /// Performs a sequence of actions, one per call.
        /// </summary>
        ISetupSequentialAction SetupSequence(Expression<Action<T>> expression);

        /// <summary>
        /// When - Direct Moq Method
        /// Allows setting up a conditional setup.
        /// Conditional setups are only matched by an invocation
        /// when the specified condition evaluates to <see langword="true"/>
        /// at the time when the invocation occurs.
        /// </summary>
        /// <param name="condition">
        ///   The condition that should be checked
        ///   when a setup is being matched against an invocation.
        /// </param>
        ISetupConditionResult<T> When(Func<bool> condition);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given invocation with specific parameters was performed:
        ///   <code>
        ///     var mock = new Mock&lt;IProcessor&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't call Execute with a "ping" string argument.
        ///     mock.Verify(proc => proc.Execute("ping"));
        ///   </code>
        /// </example>
        void Verify(Expression<Action<T>> expression);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void Verify(Expression<Action<T>> expression, Times times);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void Verify(Expression<Action<T>> expression, Func<Times> times);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock,
        /// specifying a failure error message.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        void Verify(Expression<Action<T>> expression, string failMessage);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock,
        /// specifying a failure error message.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void Verify(Expression<Action<T>> expression, Times times, string failMessage);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock,
        /// specifying a failure error message.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void Verify(Expression<Action<T>> expression, Func<Times> times, string failMessage);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <typeparam name="TResult">Type of return value from the expression.</typeparam>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given invocation with specific parameters was performed:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't call HasInventory.
        ///     mock.Verify(warehouse => warehouse.HasInventory(TALISKER, 50));
        ///   </code>
        /// </example>
        void Verify<TResult>(Expression<Func<T, TResult>> expression);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <typeparam name="TResult">Type of return value from the expression.</typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void Verify<TResult>(Expression<Func<T, TResult>> expression, Times times);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock.
        /// Use in conjunction with the default <see cref="F:Moq.MockBehavior.Loose"/>.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <typeparam name="TResult">Type of return value from the expression.</typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void Verify<TResult>(Expression<Func<T, TResult>> expression, Func<Times> times);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock,
        /// specifying a failure error message.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <typeparam name="TResult">Type of return value from the expression.</typeparam>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given invocation with specific parameters was performed:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't call HasInventory.
        ///     mock.Verify(warehouse => warehouse.HasInventory(TALISKER, 50),
        ///                 "When filling orders, inventory has to be checked");
        ///   </code>
        /// </example>
        void Verify<TResult>(Expression<Func<T, TResult>> expression, string failMessage);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock,
        /// specifying a failure error message.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <typeparam name="TResult">Type of return value from the expression.</typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number times specified by <paramref name="times"/>.
        /// </exception>
        void Verify<TResult>(Expression<Func<T, TResult>> expression, Times times, string failMessage);

        /// <summary>
        /// Verify - Direct Moq Method
        /// Verifies that a specific invocation matching the given expression was performed on the mock,
        /// specifying a failure error message.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <typeparam name="TResult">Type of return value from the expression.</typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number times specified by <paramref name="times"/>.
        /// </exception>
        void Verify<TResult>(Expression<Func<T, TResult>> expression, Func<Times> times, string failMessage);

        /// <summary>
        /// VerifyGet - Direct Moq Method
        /// Verifies that a property was read on the mock.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property to verify. Typically omitted as it can be inferred from the expression's return type.
        /// </typeparam>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given property was retrieved from it:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't retrieve the IsClosed property.
        ///     mock.VerifyGet(warehouse => warehouse.IsClosed);
        ///   </code>
        /// </example>
        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression);

        /// <summary>
        /// VerifyGet - Direct Moq Method
        /// Verifies that a property was read on the mock.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property to verify. Typically omitted as it can be inferred from the expression's return type.
        /// </typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times);

        /// <summary>
        /// VerifyGet - Direct Moq Method
        /// Verifies that a property was read on the mock.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property to verify. Typically omitted as it can be inferred from the expression's return type.
        /// </typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Func<Times> times);

        /// <summary>
        /// VerifyGet - Direct Moq Method
        /// Verifies that a property was read on the mock, specifying a failure error message.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property to verify. Typically omitted as it can be inferred from the expression's return type.
        /// </typeparam>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, string failMessage);

        /// <summary>
        /// VerifyGet - Direct Moq Method
        /// Verifies that a property was read on the mock, specifying a failure error message.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property to verify. Typically omitted as it can be inferred from the expression's return type.
        /// </typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times, string failMessage);

        /// <summary>
        /// VerifyGet - Direct Moq Method
        /// Verifies that a property was read on the mock, specifying a failure error message.
        /// </summary>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <typeparam name="TProperty">
        ///   Type of the property to verify. Typically omitted as it can be inferred from the expression's return type.
        /// </typeparam>
        /// <exception cref="MockException">
        ///   The invocation was not called the number times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Func<Times> times, string failMessage);

        /// <summary>
        /// VerifySet - Direct Moq Method
        /// Verifies that a property was set on the mock.
        /// </summary>
        /// <param name="setterExpression">Expression to verify.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given property was set on it:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't set the IsClosed property.
        ///     mock.VerifySet(warehouse => warehouse.IsClosed = true);
        ///   </code>
        /// </example>
        void VerifySet(Action<T> setterExpression);

        /// <summary>
        /// VerifySet - Direct Moq Method
        /// Verifies that a property was set on the mock.
        /// </summary>
        /// <param name="setterExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifySet(Action<T> setterExpression, Times times);

        /// <summary>
        /// VerifySet - Direct Moq Method
        /// Verifies that a property was set on the mock.
        /// </summary>
        /// <param name="setterExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifySet(Action<T> setterExpression, Func<Times> times);

        /// <summary>
        /// VerifySet - Direct Moq Method
        /// Verifies that a property was set on the mock, specifying a failure message.
        /// </summary>
        /// <param name="setterExpression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example>
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given property was set on it:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't set the IsClosed property.
        ///     mock.VerifySet(warehouse => warehouse.IsClosed = true,
        ///                    "Warehouse should always be closed after the action");
        ///   </code>
        /// </example>
        void VerifySet(Action<T> setterExpression, string failMessage);

        /// <summary>
        /// VerifySet - Direct Moq Method
        /// Verifies that a property was set on the mock, specifying a failure message.
        /// </summary>
        /// <param name="setterExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifySet(Action<T> setterExpression, Times times, string failMessage);

        /// <summary>
        /// VerifySet - Direct Moq Method
        /// Verifies that a property was set on the mock, specifying a failure message.
        /// </summary>
        /// <param name="setterExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifySet(Action<T> setterExpression, Func<Times> times, string failMessage);

        /// <summary>
        /// VerifyAdd - Direct Moq Method
        /// Verifies that an event was added to the mock.
        /// </summary>
        /// <param name="addExpression">Expression to verify.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given event handler was subscribed to an event:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't subscribe to the OnClosed event.
        ///     mock.VerifyAdd(warehouse => warehouse.OnClosed += It.IsAny&lt;EventHandler&gt;());
        ///   </code>
        /// </example>
        void VerifyAdd(Action<T> addExpression);

        /// <summary>
        /// VerifyAdd - Direct Moq Method
        /// Verifies that an event was added to the mock.
        /// </summary>
        /// <param name="addExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyAdd(Action<T> addExpression, Times times);

        /// <summary>
        /// VerifyAdd - Direct Moq Method
        /// Verifies that an event was added to the mock.
        /// </summary>
        /// <param name="addExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyAdd(Action<T> addExpression, Func<Times> times);

        /// <summary>
        /// VerifyAdd - Direct Moq Method
        /// Verifies that an event was added to the mock, specifying a failure message.
        /// </summary>
        /// <param name="addExpression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        void VerifyAdd(Action<T> addExpression, string failMessage);

        /// <summary>
        /// VerifyAdd - Direct Moq Method
        /// Verifies that an event was added to the mock, specifying a failure message.
        /// </summary>
        /// <param name="addExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyAdd(Action<T> addExpression, Times times, string failMessage);

        /// <summary>
        /// VerifyAdd - Direct Moq Method
        /// Verifies that an event was added to the mock, specifying a failure message.
        /// </summary>
        /// <param name="addExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyAdd(Action<T> addExpression, Func<Times> times, string failMessage);

        /// <summary>
        /// VerifyRemove - Direct Moq Method
        /// Verifies that an event was removed from the mock.
        /// </summary>
        /// <param name="removeExpression">Expression to verify.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <example group="verification">
        ///   This example assumes that the mock has been used, and later we want to verify
        ///   that a given event handler was removed from an event:
        ///   <code>
        ///     var mock = new Mock&lt;IWarehouse&gt;();
        ///
        ///     ... // exercise mock
        ///
        ///     // Will throw if the test code didn't unsubscribe from the OnClosed event.
        ///     mock.VerifyRemove(warehouse => warehouse.OnClose -= It.IsAny&lt;EventHandler&gt;());
        ///   </code>
        /// </example>
        void VerifyRemove(Action<T> removeExpression);

        /// <summary>
        /// VerifyRemove - Direct Moq Method
        /// Verifies that an event was removed from the mock.
        /// </summary>
        /// <param name="removeExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyRemove(Action<T> removeExpression, Times times);

        /// <summary>
        /// VerifyRemove - Direct Moq Method
        /// Verifies that an event was removed from the mock.
        /// </summary>
        /// <param name="removeExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyRemove(Action<T> removeExpression, Func<Times> times);

        /// <summary>
        /// VerifyRemove - Direct Moq Method
        /// Verifies that an event was removed from the mock, specifying a failure message.
        /// </summary>
        /// <param name="removeExpression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        void VerifyRemove(Action<T> removeExpression, string failMessage);

        /// <summary>
        /// VerifyRemove - Direct Moq Method
        /// Verifies that an event was removed from the mock, specifying a failure message.
        /// </summary>
        /// <param name="removeExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyRemove(Action<T> removeExpression, Times times, string failMessage);

        /// <summary>
        /// VerifyRemove - Direct Moq Method
        /// Verifies that an event was removed from the mock, specifying a failure message.
        /// </summary>
        /// <param name="removeExpression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">
        ///   The invocation was not called the number of times specified by <paramref name="times"/>.
        /// </exception>
        void VerifyRemove(Action<T> removeExpression, Func<Times> times, string failMessage);

        /// <summary>
        /// VerifyNoOtherCalls - Direct Moq Method
        /// Verifies that there were no calls other than those already verified.
        /// </summary>
        /// <exception cref="MockException">There was at least one invocation not previously verified.</exception>
        void VerifyNoOtherCalls();

        /// <summary>
        /// Raise - Direct Moq Method
        /// Raises the event referenced in <paramref name="eventExpression"/> using the given <paramref name="args"/> argument.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///   The <paramref name="args"/> argument is invalid for the target event invocation,
        ///   or the <paramref name="eventExpression"/> is not an event attach or detach expression.
        /// </exception>
        /// <example>
        ///   The following example shows how to raise a
        ///   <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> event:
        ///   <code>
        ///     var mock = new Mock&lt;IViewModel&gt;();
        ///     mock.Raise(x => x.PropertyChanged -= null, new PropertyChangedEventArgs("Name"));
        ///   </code>
        /// </example>
        /// <example>
        ///   This example shows how to invoke an event with a custom event arguments class
        ///   in a view that will cause its corresponding presenter to react by changing its state:
        ///   <code>
        ///     var mockView = new Mock&lt;IOrdersView&gt;();
        ///     var presenter = new OrdersPresenter(mockView.Object);
        ///
        ///     // Check that the presenter has no selection by default
        ///     Assert.Null(presenter.SelectedOrder);
        ///
        ///     // Raise the event with a specific arguments data
        ///     mockView.Raise(v => v.SelectionChanged += null, new OrderEventArgs { Order = new Order("moq", 500) });
        ///
        ///     // Now the presenter reacted to the event, and we have a selected order
        ///     Assert.NotNull(presenter.SelectedOrder);
        ///     Assert.Equal("moq", presenter.SelectedOrder.ProductName);
        ///   </code>
        /// </example>
        void Raise(Action<T> eventExpression, EventArgs args);

        /// <summary>
        /// Raise - Direct Moq Method
        /// Raises the event referenced in <paramref name="eventExpression"/> using the given <paramref name="args"/> argument for a non-<see cref="EventHandler"/>-typed event.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///   The <paramref name="args"/> arguments are invalid for the target event invocation,
        ///   or the <paramref name="eventExpression"/> is not an event attach or detach expression.
        /// </exception>
        /// <example>
        ///   The following example shows how to raise a custom event that does not adhere
        ///   to the standard <c>EventHandler</c>:
        ///   <code>
        ///     var mock = new Mock&lt;IViewModel&gt;();
        ///     mock.Raise(x => x.MyEvent -= null, "Name", true, 25);
        ///   </code>
        /// </example>
        void Raise(Action<T> eventExpression, params object[] args);

        /// <summary>
        /// RaiseAsync - Direct Moq Method
        /// Raises the event referenced in <paramref name="eventExpression"/> using the given arguments
        /// for an event with a <c>Func&lt;..., Task&gt;</c> or <c>Func&lt;..., ValueTask&gt;</c> signature.
        /// The returned <see cref="Task"/> completes when all of the <see cref="Task"/> or <see cref="ValueTask"/>
        /// instances returned by the event handlers have completed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///   The arguments are invalid for the target event invocation,
        ///   or the <paramref name="eventExpression"/> is not an event attach or detach expression.
        /// </exception>
        /// <example>
        ///   The following example shows how to raise an event with async event handlers:
        ///   <code>
        ///     interface IViewModel
        ///     {
        ///         event Func&lt;InitializationData, Task&gt; Initialized;
        ///     }
        ///     var mock = new Mock&lt;IViewModel&gt;();
        ///     mock.Object.Initialized += async initializationData => ...;
        ///     await mock.RaiseAsync(x => x.Initialized += null, new InitializationData { ... });
        ///   </code>
        /// </example>
        Task RaiseAsync(Action<T> eventExpression, params object[] args);
    }
}
