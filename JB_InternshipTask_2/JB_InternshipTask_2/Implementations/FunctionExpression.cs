using JB_InternshipTask_2.Expressions;

namespace JB_InternshipTask_2.Implementations;

class FunctionExpression : IFunction
{
    public FunctionKind Kind { get; }
    public IExpression Argument { get; }
    
    public FunctionExpression(FunctionKind kind, IExpression arg)
    {
        Kind = kind; 
        Argument = arg;
    } 
    
    public override string ToString() => $"{Kind} {Argument}";
    // public override string ToString() => $"{Kind}({Argument})";
}