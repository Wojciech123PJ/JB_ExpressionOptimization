using JB_InternshipTask_2.Expressions;

namespace JB_InternshipTask_2.Implementations;

public class ConstantExpression : IConstantExpression
{
    public int Value { get; }

    public ConstantExpression(int value)
    {
        Value = value;
    }
    public override string ToString() => Value.ToString();
}
