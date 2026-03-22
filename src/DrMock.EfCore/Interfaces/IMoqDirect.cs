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
        ISetup<T> Setup(Expression<Action<T>> expression);

        ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression);

        ISetupGetter<T, TProperty> SetupGet<TProperty>(Expression<Func<T, TProperty>> expression);

        ISetupSetter<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression);

        ISetup<T> SetupSet(Action<T> setterExpression);

        ISetup<T> SetupAdd(Action<T> addExpression);

        ISetup<T> SetupRemove(Action<T> removeExpression);

        Mock<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> property);

        Mock<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> property, TProperty initialValue);

        Mock<T> SetupAllProperties();

        ISetupSequentialResult<TResult> SetupSequence<TResult>(Expression<Func<T, TResult>> expression);

        ISetupSequentialAction SetupSequence(Expression<Action<T>> expression);

        ISetupConditionResult<T> When(Func<bool> condition);

        void Verify(Expression<Action<T>> expression);

        void Verify(Expression<Action<T>> expression, Times times);

        void Verify(Expression<Action<T>> expression, Func<Times> times);

        void Verify(Expression<Action<T>> expression, string failMessage);

        void Verify(Expression<Action<T>> expression, Times times, string failMessage);

        void Verify(Expression<Action<T>> expression, Func<Times> times, string failMessage);

        void Verify<TResult>(Expression<Func<T, TResult>> expression);

        void Verify<TResult>(Expression<Func<T, TResult>> expression, Times times);

        void Verify<TResult>(Expression<Func<T, TResult>> expression, Func<Times> times);

        void Verify<TResult>(Expression<Func<T, TResult>> expression, Func<Times> times, string failMessage);

        void Verify<TResult>(Expression<Func<T, TResult>> expression, string failMessage);

        void Verify<TResult>(Expression<Func<T, TResult>> expression, Times times, string failMessage);

        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression);

        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times);

        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Func<Times> times);

        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, string failMessage);

        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times, string failMessage);

        void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Func<Times> times, string failMessage);

        void VerifySet(Action<T> setterExpression);

        void VerifySet(Action<T> setterExpression, Times times);

        void VerifySet(Action<T> setterExpression, Func<Times> times);

        void VerifySet(Action<T> setterExpression, string failMessage);

        void VerifySet(Action<T> setterExpression, Times times, string failMessage);

        void VerifySet(Action<T> setterExpression, Func<Times> times, string failMessage);

        void VerifyAdd(Action<T> addExpression);

        void VerifyAdd(Action<T> addExpression, Times times);

        void VerifyAdd(Action<T> addExpression, Func<Times> times);

        void VerifyAdd(Action<T> addExpression, string failMessage);

        void VerifyAdd(Action<T> addExpression, Times times, string failMessage);

        void VerifyAdd(Action<T> addExpression, Func<Times> times, string failMessage);

        void VerifyRemove(Action<T> removeExpression);

        void VerifyRemove(Action<T> removeExpression, Times times);

        void VerifyRemove(Action<T> removeExpression, Func<Times> times);

        void VerifyRemove(Action<T> removeExpression, string failMessage);

        void VerifyRemove(Action<T> removeExpression, Times times, string failMessage);

        void VerifyRemove(Action<T> removeExpression, Func<Times> times, string failMessage);

        void VerifyNoOtherCalls();

        void Raise(Action<T> eventExpression, EventArgs args);

        void Raise(Action<T> eventExpression, params object[] args);

        Task RaiseAsync(Action<T> eventExpression, params object[] args);
    }
}
