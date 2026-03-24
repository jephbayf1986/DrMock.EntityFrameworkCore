using System;
using System.Linq.Expressions;

namespace DrMock.EfCore.Exceptions
{
    public class DrMockExpressionException : Exception
    {
        private DrMockExpressionException(Expression expression, string message)
            : base ($"The following exception occurred with expression '{expression}': {message}")
        {
        }

        internal static DrMockExpressionException UnsupportedOperation(Expression expression)
        {
            return new DrMockExpressionException(expression, "Expression contains an unsupported action - example expected: 'x => x.Id == 2 && x.Name == \"Joe Bloggs\" && x.ManagerName == \"Jill Smith\"'");
        }

        internal static DrMockExpressionException UnsupportedUnaryOperation(Expression expression)
        {
            return new DrMockExpressionException(expression, "Expression contains an unsupported Unary action - supported Unarys are: Not(!) and Negate(-)");
        }

        internal static DrMockExpressionException RootParamaterNotFound(Expression expression)
        {
            return new DrMockExpressionException(expression, "Unable to find an expression root parameter (eg. x => ...");
        }
    }
}