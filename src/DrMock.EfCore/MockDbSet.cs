using DrMock.EfCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrMock.EfCore
{
    public sealed class MockDbSet<TEntity> : IMoqDirect<DbSet<TEntity>> where TEntity : class
    {
        private readonly Mock<DbSet<TEntity>> _mock;

        internal MockDbSet(Mock<DbSet<TEntity>> mock)
        {
            _mock = mock;
        }

        public void Raise(Action<DbSet<TEntity>> eventExpression, EventArgs args)
        {
            _mock.Raise(eventExpression, args);
        }

        public void Raise(Action<DbSet<TEntity>> eventExpression, params object[] args)
        {
            _mock.Raise(eventExpression, args);
        }

        public Task RaiseAsync(Action<DbSet<TEntity>> eventExpression, params object[] args)
        {
            return _mock.RaiseAsync(eventExpression, args);
        }

        public ISetup<DbSet<TEntity>> Setup(Expression<Action<DbSet<TEntity>>> expression)
        {
            return _mock.Setup(expression);
        }

        public ISetup<DbSet<TEntity>, TResult> Setup<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression)
        {
            return _mock.Setup(expression);
        }

        public ISetup<DbSet<TEntity>> SetupAdd(Action<DbSet<TEntity>> addExpression)
        {
            return _mock.SetupAdd(addExpression);
        }

        public Mock<DbSet<TEntity>> SetupAllProperties()
        {
            return _mock.SetupAllProperties();
        }

        public ISetupGetter<DbSet<TEntity>, TProperty> SetupGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression)
        {
            return _mock.SetupGet(expression);
        }

        public Mock<DbSet<TEntity>> SetupProperty<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> property)
        {
            return _mock.SetupProperty(property);
        }

        public Mock<DbSet<TEntity>> SetupProperty<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> property, TProperty initialValue)
        {
            return _mock.SetupProperty(property, initialValue);
        }

        public ISetup<DbSet<TEntity>> SetupRemove(Action<DbSet<TEntity>> removeExpression)
        {
            return _mock.SetupRemove(removeExpression);
        }

        public ISetupSequentialResult<TResult> SetupSequence<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression)
        {
            return _mock.SetupSequence(expression);
        }

        public ISetupSequentialAction SetupSequence(Expression<Action<DbSet<TEntity>>> expression)
        {
            return _mock.SetupSequence(expression);
        }

        public ISetupSetter<DbSet<TEntity>, TProperty> SetupSet<TProperty>(Action<DbSet<TEntity>> setterExpression)
        {
            return _mock.SetupSet<TProperty>(setterExpression);
        }

        public ISetup<DbSet<TEntity>> SetupSet(Action<DbSet<TEntity>> setterExpression)
        {
            return _mock.SetupSet(setterExpression);
        }

        public void Verify(Expression<Action<DbSet<TEntity>>> expression)
        {
            _mock.Verify(expression);
        }

        public void Verify(Expression<Action<DbSet<TEntity>>> expression, Times times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<DbSet<TEntity>>> expression, Func<Times> times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<DbSet<TEntity>>> expression, string failMessage)
        {
            _mock.Verify(expression, failMessage);
        }

        public void Verify(Expression<Action<DbSet<TEntity>>> expression, Times times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void Verify(Expression<Action<DbSet<TEntity>>> expression, Func<Times> times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression)
        {
            _mock.Verify(expression);
        }

        public void Verify<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression, Times times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression, Func<Times> times)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression, Func<Times> times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void Verify<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression, string failMessage)
        {
            _mock.Verify(expression, failMessage);
        }

        public void Verify<TResult>(Expression<Func<DbSet<TEntity>, TResult>> expression, Times times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyAdd(Action<DbSet<TEntity>> addExpression)
        {
            _mock.VerifyAdd(addExpression);
        }

        public void VerifyAdd(Action<DbSet<TEntity>> addExpression, Times times)
        {
            _mock.VerifyAdd(addExpression, times);
        }

        public void VerifyAdd(Action<DbSet<TEntity>> addExpression, Func<Times> times)
        {
            _mock.VerifyAdd(addExpression, times);
        }

        public void VerifyAdd(Action<DbSet<TEntity>> addExpression, string failMessage)
        {
            _mock.VerifyAdd(addExpression, failMessage);
        }

        public void VerifyAdd(Action<DbSet<TEntity>> addExpression, Times times, string failMessage)
        {
            _mock.VerifyAdd(addExpression, times, failMessage);
        }

        public void VerifyAdd(Action<DbSet<TEntity>> addExpression, Func<Times> times, string failMessage)
        {
            _mock.VerifyAdd(addExpression, times, failMessage);
        }

        public void VerifyGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression)
        {
            _mock.VerifyGet(expression);
        }

        public void VerifyGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression, Times times)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression, Func<Times> times)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression, string failMessage)
        {
            _mock.Verify(expression, failMessage);
        }

        public void VerifyGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression, Times times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyGet<TProperty>(Expression<Func<DbSet<TEntity>, TProperty>> expression, Func<Times> times, string failMessage)
        {
            _mock.Verify(expression, times);
        }

        public void VerifyNoOtherCalls()
        {
            _mock.VerifyNoOtherCalls();
        }

        public void VerifyRemove(Action<DbSet<TEntity>> removeExpression)
        {
            _mock.VerifyRemove(removeExpression);
        }

        public void VerifyRemove(Action<DbSet<TEntity>> removeExpression, Times times)
        {
            _mock.VerifyRemove(removeExpression, times);
        }

        public void VerifyRemove(Action<DbSet<TEntity>> removeExpression, Func<Times> times)
        {
            _mock.VerifyRemove(removeExpression, times);
        }

        public void VerifyRemove(Action<DbSet<TEntity>> removeExpression, string failMessage)
        {
            _mock.VerifyRemove(removeExpression, failMessage);
        }

        public void VerifyRemove(Action<DbSet<TEntity>> removeExpression, Times times, string failMessage)
        {
            _mock.VerifyRemove(removeExpression, times, failMessage);
        }

        public void VerifyRemove(Action<DbSet<TEntity>> removeExpression, Func<Times> times, string failMessage)
        {
            _mock.VerifyRemove(removeExpression, times, failMessage);
        }

        public void VerifySet(Action<DbSet<TEntity>> setterExpression)
        {
            _mock.VerifySet(setterExpression);
        }

        public void VerifySet(Action<DbSet<TEntity>> setterExpression, Times times)
        {
            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<DbSet<TEntity>> setterExpression, Func<Times> times)
        {
            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<DbSet<TEntity>> setterExpression, string failMessage)
        {
            _mock.VerifySet(setterExpression, failMessage);
        }

        public void VerifySet(Action<DbSet<TEntity>> setterExpression, Times times, string failMessage)
        {
            _mock.VerifySet(setterExpression, times);
        }

        public void VerifySet(Action<DbSet<TEntity>> setterExpression, Func<Times> times, string failMessage)
        {
            _mock.VerifySet(setterExpression, times);
        }

        public ISetupConditionResult<DbSet<TEntity>> When(Func<bool> condition)
        {
            return _mock.When(condition);
        }

        public DbSet<TEntity> Object
        {
            get
            {
                return _mock.Object;
            }
        }
    }
}
