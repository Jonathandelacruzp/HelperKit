using System;
using System.Linq.Expressions;

namespace HelperKit.Builders
{
    public static class ParameterReplacer
    {
        // Produces an expression identical to 'expression'
        // except with 'source' parameter replaced with 'target' expression.     
        private static Expression Replace(Expression expression, ParameterExpression oldParameter, Expression newBody)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));
            _ = oldParameter ?? throw new ArgumentNullException(nameof(oldParameter));
            _ = newBody ?? throw new ArgumentNullException(nameof(newBody));
            return expression is LambdaExpression
                ? throw new InvalidOperationException("The search & replace operation must be performed on the body of the lambda.")
                : new ParameterReplacerVisitor(oldParameter, newBody).Visit(expression);
        }

        //Chains two lambda expressions together as in the following example:
        //given these inputs:
        //  parentExpression = customer => customer.PrimaryAddress;
        //  childExpression = address => address.Street;
        //produces:
        //  customer => customer.PrimaryAddress.Street;
        //this function only supports parents with a single input parameter, and children with a single output parameter
        //many more overloads could be added for other common delegate types
        public static Expression<Func<TA, TC>> ChainWith<TA, TB, TC>(this Expression<Func<TA, TB>> parentExpression, Expression<Func<TB, TC>> childExpression)
        {
            //could call Chain, but some of the checks are unnecessary since the inputs are strongly typed
            _ = parentExpression ?? throw new ArgumentNullException(nameof(parentExpression));
            _ = childExpression ?? throw new ArgumentNullException(nameof(childExpression));
            //since the lambda is strongly defined, we can be sure that there exists one and only one parameter on the parent and child expressions
            return Expression.Lambda<Func<TA, TC>>(
                Replace(childExpression.Body, childExpression.Parameters[0], parentExpression.Body),
                parentExpression.Parameters);
        }

        //Chains two lambda expressions together as in the following example:
        //given these inputs:
        //  parentExpression = (customers, index) => customers[index].PrimaryAddress;
        //  childExpression = address => Console.WriteLine(address.Street);
        //produces:
        //  (customers, index) => Console.WriteLine(customers[index].PrimaryAddress.Street);
        //this function supports parent expressions with any number of input parameters (including 0), and child expressions with no output value (Action<>s)
        //however, it is not strongly typed, and validity cannot be verified at compile time
        public static LambdaExpression Chain(LambdaExpression parentExpression, LambdaExpression childExpression)
        {
            _ = parentExpression ?? throw new ArgumentNullException(nameof(parentExpression));
            _ = childExpression ?? throw new ArgumentNullException(nameof(childExpression));
            if (parentExpression.ReturnType == typeof(void)) throw new ArgumentException("The parent expression must return a value.", nameof(parentExpression));
            if (childExpression.Parameters.Count != 1 || childExpression.Parameters[0].Type != parentExpression.ReturnType)
                throw new ArgumentException("The child expression must have a single parameter of the same type as the parent expression's return type.", nameof(childExpression));
            //this code could provide add a conversion between compatible types, but for now just throws an error; the types must be identical
            return Expression.Lambda(Replace(childExpression.Body, childExpression.Parameters[0], parentExpression.Body), parentExpression.Parameters);
        }

        private class ParameterReplacerVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _source;
            private readonly Expression _target;

            public ParameterReplacerVisitor(ParameterExpression source, Expression target)
            {
                _source = source;
                _target = target;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                // Replace the source with the target, visit other params as usual.
                return node.Equals(_source) ? _target : base.VisitParameter(node);
            }
        }
    }
}