using System;
using System.Linq.Expressions;

namespace DrMock.EfCore.Exceptions
{
    /// <summary>
    /// DrMock Expression Exception
    /// Occurs when an expression is passed into Exists(...) method.
    /// Example expressions which are supported:
    /// x => x.Id == jobId
    /// x => x.EmployeeId == employee1Id || x.EmployeeId == employee2Id
    /// x => x.FirstName == "Joe" && x.LastName == "Bloggs"
    /// x => !x.IsManager
    /// x => x.Manager.Name == "Jill Smith"
    /// </summary>
    public class DrMockExpressionException : Exception
    {
        private DrMockExpressionException(Expression expression, string message)
            : base ($"The following exception occurred with expression '{expression}': {message}")
        {
        }

        internal static DrMockExpressionException UnsupportedOperation(Expression expression)
        {
            return new DrMockExpressionException(expression, "Expression contains an unsupported action - example expected: 'x => x.Id == 2 && x.Name == \"Joe Bloggs\" && x.Manager.Name == \"Jill Smith\"'");
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