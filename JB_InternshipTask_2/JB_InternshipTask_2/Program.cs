using JB_InternshipTask_2.Expressions;
using JB_InternshipTask_2.Implementations;

// sin(7 * (2 + x)) - 7 * (2 + x) + cos(x)

public static class OptimizeExpression
{
    public static IExpression Optimize(IExpression expression)
    {
        var cache = new Dictionary<string, IExpression>();
        return RecursiveOptimize(expression, cache);
    }

    private static IExpression RecursiveOptimize(IExpression expression, Dictionary<string, IExpression> cache)
    {
        switch (expression)
        {
            case IConstantExpression constant:
            {
                string key = constant.ToString();
                if (cache.TryGetValue(key, out var cachedExpression))
                {
                    return cachedExpression;
                }
                cache.Add(key, constant);
                return constant;
            }
            case IVariableExpression variable:
            {
                string key = variable.ToString();
                if (cache.TryGetValue(key, out var cachedExpression))
                {
                    return cachedExpression;
                }
                cache.Add(key, variable);
                return variable;
            }
            case IBinaryExpression binaryExpression:
            {
                var left = RecursiveOptimize(binaryExpression.Left, cache);
                var right = RecursiveOptimize(binaryExpression.Right, cache);
                
                string key = $"{left} {binaryExpression.Sign} {right}";

                if (cache.TryGetValue(key, out var cachedExpression))
                {
                    return cachedExpression;
                }

                var newBinaryExpression = new BinaryExpression(left, right, binaryExpression.Sign);
                cache.Add(key, newBinaryExpression);
                return newBinaryExpression;
            }
            case IFunction functionExpression:
            {
                var argument = RecursiveOptimize(functionExpression.Argument, cache);
                
                string key = $"{functionExpression.Kind} {argument}";

                if (cache.TryGetValue(key, out var cachedExpression))
                {
                    return cachedExpression;
                }
                var newFunctionExpression = new FunctionExpression(functionExpression.Kind, argument);
                cache.Add(key, newFunctionExpression);
                return newFunctionExpression;
            }
            default:
                throw new ArgumentException($"Unknown expression type: {expression.GetType()}");
        }
    }
}