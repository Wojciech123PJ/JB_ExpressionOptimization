using JB_InternshipTask_2.Expressions;

namespace JB_InternshipTask_2.Implementations;

public class VariableExpression : IVariableExpression
{
    public string Name { get; }

    public VariableExpression(string name)
    {
        Name = name;
    }
    public override string ToString() => Name;
}