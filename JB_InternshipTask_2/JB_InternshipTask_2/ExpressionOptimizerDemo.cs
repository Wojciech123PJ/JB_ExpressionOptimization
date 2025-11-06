
using JB_InternshipTask_2.Expressions;
using JB_InternshipTask_2.Implementations;

namespace ExpressionOptimizerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new VariableExpression("x");
            var two = new ConstantExpression(2);
            var seven = new ConstantExpression(7);
            // sin(7 * (2 + x))
            var sinPart = new FunctionExpression(
                FunctionKind.Sin,
                new BinaryExpression(
                    seven,
                    new BinaryExpression(two, x, OperatorSign.Plus),
                    OperatorSign.Multiply));

            // 7 * (2 + x)
            var secondMul = new BinaryExpression(
                seven,
                new BinaryExpression(two, x, OperatorSign.Plus),
                OperatorSign.Multiply);

            // sin(7*(2+x)) - 7*(2+x)
            var first = new BinaryExpression(sinPart, secondMul, OperatorSign.Minus);

            // // (2 + x)
            // var twoPlusX = new BinaryExpression(two, x, OperatorSign.Plus);
            //
            // // 7 * (2 + x)
            // var sevenTimesTwoPlusX = new BinaryExpression(seven, twoPlusX, OperatorSign.Multiply);
            //
            // // sin(7 * (2 + x))
            // var sinPart = new FunctionExpression(FunctionKind.Sin, sevenTimesTwoPlusX);
            //
            // // cos(x)
            var cosPart = new FunctionExpression(FunctionKind.Cos, x);
            //
            // // sin(7*(2+x)) - 7*(2+x)
            // var first = new BinaryExpression(sinPart, sevenTimesTwoPlusX, OperatorSign.Minus);

            // sin(7*(2+x)) - 7*(2+x) + cos(x)
            IExpression expr = new BinaryExpression(first, cosPart, OperatorSign.Plus);

            // Console.WriteLine("Oryginalne drzewo:");
            // Print(expr);
            //
            // Console.WriteLine("\n--- OPTIMIZATION ---");
            // var optimized = OptimizeExpression.Optimize(expr);
            //
            // Print(optimized);
            //
            // Console.WriteLine("\n--- TEST REUSE ---");
            //
            // var origBin = ((IBinaryExpression)((IBinaryExpression)optimized).Left);
            // var leftSub = ((IFunction)origBin.Left).Argument;     
            // var rightSub = origBin.Right;                       
            //
            // Console.WriteLine("Is 7*(2+x) in sin(...) and on the right the same object?");
            // Console.WriteLine(Object.ReferenceEquals(leftSub, rightSub));

            // True
            
            var beforeLeft = ((IFunction)((IBinaryExpression)((IBinaryExpression)expr).Left).Left).Argument;
            var beforeRight = ((IBinaryExpression)((IBinaryExpression)((IBinaryExpression)expr).Left)).Right;
            
            Console.WriteLine("Before optimization same reference? " +
                              Object.ReferenceEquals(beforeLeft, beforeRight));
            
            var optimized = OptimizeExpression.Optimize(expr);
            
            var afterLeft = ((IFunction)((IBinaryExpression)((IBinaryExpression)optimized).Left).Left).Argument;
            var afterRight = ((IBinaryExpression)((IBinaryExpression)((IBinaryExpression)optimized).Left)).Right;
            
            Console.WriteLine("After optimization same reference? " +
                              Object.ReferenceEquals(afterLeft, afterRight));

        }

        static void Print(IExpression expr)
        {
            var seen = new Dictionary<IExpression, int>();
            int counter = 1;
            PrintRecursive(expr, seen, ref counter, "");
        }

        static void PrintRecursive(IExpression expr, Dictionary<IExpression, int> seen, ref int counter, string indent)
        {
            if (!seen.TryGetValue(expr, out int id))
            {
                id = counter++;
                seen[expr] = id;
            }

            // Oznaczamy każdy węzeł jego ID-em
            string prefix = $"[{id}] ";

            switch (expr)
            {
                case IConstantExpression c:
                    Console.WriteLine($"{indent}{prefix}Const({c.Value})");
                    break;

                case IVariableExpression v:
                    Console.WriteLine($"{indent}{prefix}Var({v.Name})");
                    break;

                case IBinaryExpression b:
                    Console.WriteLine($"{indent}{prefix}{b.Sign}");
                    PrintRecursive(b.Left, seen, ref counter, indent + "  ");
                    PrintRecursive(b.Right, seen, ref counter, indent + "  ");
                    break;

                case IFunction f:
                    Console.WriteLine($"{indent}{prefix}{f.Kind}");
                    PrintRecursive(f.Argument, seen, ref counter, indent + "  ");
                    break;
            }
        }

    }
}
