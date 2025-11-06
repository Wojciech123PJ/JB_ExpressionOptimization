namespace JB_InternshipTask_2.Expressions;

interface IFunction : IExpression
{
    FunctionKind Kind { get; } 
    IExpression Argument { get; }
}