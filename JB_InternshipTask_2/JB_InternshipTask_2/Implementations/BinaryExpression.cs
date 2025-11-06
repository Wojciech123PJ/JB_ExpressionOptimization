using JB_InternshipTask_2.Expressions;

namespace JB_InternshipTask_2.Implementations;

class BinaryExpression : IBinaryExpression
{
    public IExpression Left { get; }
    public IExpression Right { get; }
    public OperatorSign Sign { get; }

    public BinaryExpression(IExpression left, IExpression right, OperatorSign sign)
    {
        Left = left;
        Right = right;
        Sign = sign;
    }
    
    public override string ToString() => $"{Left} {Sign} {Right}";
    
}