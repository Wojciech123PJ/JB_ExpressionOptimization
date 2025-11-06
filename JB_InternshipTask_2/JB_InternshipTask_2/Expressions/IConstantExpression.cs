namespace JB_InternshipTask_2.Expressions;

interface IConstantExpression : IExpression
{
    int Value { get; }
}