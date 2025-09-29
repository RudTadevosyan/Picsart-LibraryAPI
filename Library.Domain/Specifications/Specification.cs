using System.Linq.Expressions;

namespace Library.Domain.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public Specification<T> And(Specification<T> other)
    {
        return new AndSpecification<T>(this, other);
    }
}

//for starting point to build with And the specs 
public class DefaultSpecification<T>: Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression() => _ => true;
}

public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();

        // Ensure parameter consistency
        var param = Expression.Parameter(typeof(T), "book");
        var leftBody = Expression.Invoke(leftExpr, param);
        var rightBody = Expression.Invoke(rightExpr, param);
        var andExpr = Expression.AndAlso(leftBody, rightBody);

        return Expression.Lambda<Func<T, bool>>(andExpr, param);
    }
}