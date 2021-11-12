using System.Linq.Expressions;

namespace HelperKit.Builders;

/// <summary>
/// PredicateBuilder
/// </summary>
public static class PredicateBuilder
{
    /// <summary>
    /// PredicateBuilder with parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> New<T>(bool value = true) => _ => value;

    /// <summary>
    /// PredicateBuilder for True result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> True<T>() => _ => true;

    /// <summary>
    /// PredicateBuilder for False result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> False<T>() => _ => false;

    /// <summary>
    /// PredicateBuilder for Or operator
    /// </summary>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
    }

    /// <summary>
    /// PredicateBuilder for And operator
    /// </summary>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }
}
