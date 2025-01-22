﻿using System.Linq.Expressions;

namespace ClinicalTrials.Application.Common.Helpers;

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));
        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);
        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);
        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(left, right), parameter);
    }
    
    private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
    {
        public override Expression Visit(Expression? node)
        {
            return (node == oldValue ? newValue : base.Visit(node)) ?? throw new InvalidOperationException();
        }
    }
}