namespace JB_InternshipTask_2.Expressions;

interface IBinaryExpression : IExpression
{
    IExpression Left { get; } 
    IExpression Right { get; }
    OperatorSign Sign { get; }
}