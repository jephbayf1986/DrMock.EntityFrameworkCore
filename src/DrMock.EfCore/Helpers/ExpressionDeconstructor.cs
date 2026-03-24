using DrMock.EfCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DrMock.EfCore.Helpers
{
    internal sealed class ExpressionDeconstructor<T> : ExpressionVisitor
    {
        private readonly Dictionary<string, Action<T>> _actions;

        public ExpressionDeconstructor()
        {
            _actions = new Dictionary<string, Action<T>>();
        }

        public void Extract(Expression<Func<T, bool>> predicate)
        {
            Visit(predicate.Body);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.AndAlso || node.NodeType == ExpressionType.And)
            {
                Visit(node.Left);
                Visit(node.Right);
                return node;
            }

            if (node.NodeType == ExpressionType.OrElse || node.NodeType == ExpressionType.Or)
            {
                Visit(node.Left); // Only need to do one
                return node;
            }

            if (node.NodeType == ExpressionType.Equal)
            {
                if (IsProperty(node.Left, out string leftProperty) && IsValue(node.Right, out object rightConstant))
                {
                    var success = TryCreateAction(leftProperty, rightConstant);

                    if (success) return node;
                }
                else if (IsNestedProperty(node.Left, out ICollection<string> leftNested, out string leftFinalProp) && IsValue(node.Right, out object rightSubConstant))
                {
                    var success = TryCreateAction(leftNested, leftFinalProp, rightSubConstant);

                    if (success) return node;
                }
                else if (IsValue(node.Left, out object leftConstant) && IsProperty(node.Right, out string rightProperty))
                {
                    var success = TryCreateAction(rightProperty, leftConstant);

                    if (success) return node;
                }
                else if (IsValue(node.Left, out object leftSubConstant) && IsNestedProperty(node.Right, out ICollection<string> rightNested, out string rightFinalProp))
                {
                    var success = TryCreateAction(rightNested, rightFinalProp, leftSubConstant);

                    if (success) return node;
                }
            }

            throw DrMockExpressionException.UnsupportedOperation(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Not)
            {
                var operand = node.Operand;

                if (IsProperty(node.Operand, out string propertyName))
                {
                    var success = TryCreateAction(propertyName, false);

                    if (success) return node;
                }

                throw DrMockExpressionException.UnsupportedUnaryOperation(node);
            }

            return node;
        }


        public IEnumerable<Action<T>> GetActionExpressions()
        {
            return _actions.Select(x => x.Value);
        }

        private bool TryCreateAction(string propertyName, object constantValue)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var property = Expression.Property(param, propertyName);

            var constant = Expression.Constant(constantValue, property.Type);

            var assignment = Expression.Assign(property, constant);

            var actionExpression = Expression.Lambda<Action<T>>(assignment, param);

            try
            {

                var action = actionExpression.Compile();

                _actions.Add(actionExpression.ToString(), action);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryCreateAction(IEnumerable<string> nestedObjectNames, string propertyName, object constantValue) 
        {
            var param = Expression.Parameter(typeof(T), "x");

            Expression previousLevel = param;
            Expression finalNestedObject = param;

            foreach (var nestedName in nestedObjectNames)
            {
                finalNestedObject = Expression.Property(previousLevel, nestedName);

                previousLevel = finalNestedObject;
            }

            var nestedProperty = Expression.Property(finalNestedObject, propertyName);

            var constant = Expression.Constant(constantValue, nestedProperty.Type);

            var assignment = Expression.Assign(nestedProperty, constant);

            var actionExpression = Expression.Lambda<Action<T>>(assignment, param);

            try
            {

                var action = actionExpression.Compile();

                _actions.Add(actionExpression.ToString(), action);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsProperty(Expression expression, out string propertyName)
        {
            propertyName = null;

            if (expression is MemberExpression memEx 
                && memEx.Member is PropertyInfo 
                && memEx.Expression.NodeType == ExpressionType.Parameter)
            {
                propertyName = memEx.Member.Name;

                return typeof(T).GetProperty(propertyName) != null;
            }

            return false;
        }

        public bool IsNestedProperty(Expression expression, out ICollection<string> nestedProperties, out string finalPropertyName) 
        {
            nestedProperties = new List<string>();
            finalPropertyName = string.Empty;

            if (expression is MemberExpression memEx 
                && memEx.Member is PropertyInfo 
                && memEx.Expression.NodeType == ExpressionType.MemberAccess
                && memEx.Expression is MemberExpression innerMemEx)
            {
                if (innerMemEx.Expression.NodeType == ExpressionType.Parameter)
                {
                    var param = innerMemEx.Expression as ParameterExpression;

                    Type subType = innerMemEx.Type;

                    var createNewSub = Expression.New(subType);

                    var assignNewSub = Expression.Assign(memEx.Expression, createNewSub);

                    var actionExpression = Expression.Lambda<Action<T>>(assignNewSub, param);

                    var actionText = actionExpression.ToString();

                    try
                    {
                        if (!_actions.ContainsKey(actionText))
                        {
                            var action = actionExpression.Compile();

                            _actions.Add(actionText, action);
                        }
                    }
                    catch
                    {
                        return false;
                    }

                    nestedProperties.Add(innerMemEx.Member.Name);
                    finalPropertyName = memEx.Member.Name;

                    return true;
                }
                
                if (innerMemEx.Expression.NodeType == ExpressionType.MemberAccess && IsNestedProperty(innerMemEx, out ICollection<string> innerNested, out string innerFinalPropName))
                {
                    var param = GetRootParameter(memEx);

                    Type subType = innerMemEx.Type;

                    var createNewSub = Expression.New(subType);

                    var assignNewSub = Expression.Assign(memEx.Expression, createNewSub);

                    var actionExpression = Expression.Lambda<Action<T>>(assignNewSub, param);

                    var actionText = actionExpression.ToString();

                    try
                    {
                        if (!_actions.ContainsKey(actionText))
                        {
                            var action = actionExpression.Compile();

                            _actions.Add(actionText, action);
                        }
                    }
                    catch
                    {
                        return false;
                    }

                    nestedProperties = innerNested;
                    nestedProperties.Add(innerMemEx.Member.Name);
                    finalPropertyName = memEx.Member.Name;

                    return true;
                }
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

            if (expression is UnaryExpression unaEx && unaEx.NodeType == ExpressionType.Negate)
            {
                if (IsValue(unaEx.Operand, out value))
                {
                    value = TryNegateValue(value, unaEx.Type);
                    return true;
                } 
            }

            return false;
        }

        private static object TryNegateValue(object value, Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            switch (Type.GetTypeCode(type)) 
            {
                case TypeCode.SByte:
                    return 0 - (sbyte)value;
                case TypeCode.Int16:
                    return 0 - (short)value;
                case TypeCode.Int32:
                    return 0 - (int)value;
                case TypeCode.Int64:
                    return 0 - (long)value;
                case TypeCode.Double:
                    return 0 - (double)value;
                case TypeCode.Decimal:
                    return 0 - (decimal)value;
                default:
                    return value;
            }
        }

        private ParameterExpression GetRootParameter(MemberExpression member)
        {
            Expression current = member;

            while (current is MemberExpression mem)
            {
                current = mem.Expression;
            }

            return current as ParameterExpression
                ?? throw DrMockExpressionException.RootParamaterNotFound(member);
        }
    }
}