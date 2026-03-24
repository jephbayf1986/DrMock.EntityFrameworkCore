using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DrMock.EfCore.Helpers
{
    internal sealed class ExpressionDeconstructor<T> : ExpressionVisitor
    {
        private readonly ICollection<Action<T>> _actions;

        public ExpressionDeconstructor()
        {
            _actions = new List<Action<T>>();
        }

        public void Extract(Expression<Func<T, bool>> predicate)
        {
            Visit(predicate.Body);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.AndAlso)
            {
                Visit(node.Left);
                Visit(node.Right);
                return node;
            }

            if (node.NodeType == ExpressionType.Or)
            {
                Visit(node.Left); // Only need to do one
                return node;
            }

            if (node.NodeType == ExpressionType.Equal)
            {
                if (IsProperty(node.Left, out string leftProperty) && IsValue(node.Right, out object rightConstant))
                {
                    TryCreateAction(leftProperty, rightConstant);
                }
                else if (IsValue(node.Left, out object leftConstant) && IsProperty(node.Right, out string rightProperty))
                {
                    TryCreateAction(rightProperty, leftConstant);
                }
            }

            throw new NotSupportedException("Unsuppported operator");
        }

        public IEnumerable<Action<T>> GetActionExpressions()
        {
            return _actions;
        }

        private void TryCreateAction(string propertyName, object constantValue)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var property = Expression.Property(param, propertyName);

            var constant = Expression.Constant(constantValue, property.Type);

            var assignment = Expression.Assign(property, constant);

            var actionExpression = Expression.Lambda<Action<T>>(assignment, param);

            var action = actionExpression.Compile();

            _actions.Add(action);
        }

        private bool IsProperty(Expression expression, out string propertyName)
        {
            propertyName = null;

            if (expression is MemberExpression memEx && memEx.Member is PropertyInfo)
            {
                propertyName = memEx.Member.Name;

                return typeof(T).GetProperty(propertyName) != null;
            }

            return false;
        }

        private bool IsValue(Expression expression, out object value)
        {
            value = null;

            if (expression is ConstantExpression constEx)
            {
                value = constEx.Value;

                return true;
            }

            if (expression is MemberExpression memEx && !(memEx is PropertyInfo))
            {
                value = Expression.Lambda(memEx).Compile().DynamicInvoke();
                return true;
            }

            if (expression is UnaryExpression)
            {

            }

            return false;
        }

    }
}