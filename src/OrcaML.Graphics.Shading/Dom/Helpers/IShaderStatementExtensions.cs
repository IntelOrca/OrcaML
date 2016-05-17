using System.Collections.Generic;
using System.Linq;

namespace OrcaML.Graphics.Shading.Dom.Helpers
{
    public static class IShaderStatementExtensions
    {
        public static void Visit(this IShaderStatement statement, IShaderStatementVisitor visitor)
        {
            VisitNode(statement, visitor);
        }

        private static void VisitNode(IShaderNode node, IShaderStatementVisitor visitor)
        {
            var statement = node as IShaderStatement;

            if (statement is IShaderIfStatement)
            {
                IShaderIfStatement ifStatement = (IShaderIfStatement)statement;
                if (visitor.VisitIf(ifStatement))
                {
                    IEnumerable<IShaderNode> children = ifStatement.IfBlocks[0].Children;
                    VisitChildren(children, visitor);
                    visitor.Exit(ifStatement);
                }
                if (ifStatement.ElseBlock != null && visitor.VisitElse(ifStatement))
                {
                    IEnumerable<IShaderNode> children = ifStatement.ElseBlock.Children;
                    VisitChildren(children, visitor);
                    visitor.ExitElse(ifStatement);
                }
            }
            else
            {
                if (statement == null || visitor.Visit(statement))
                {
                    IEnumerable<IShaderNode> children = node.Children;
                    VisitChildren(children, visitor);
                }
                if (statement != null)
                {
                    visitor.Exit(statement);
                }
            }
        }

        private static void VisitChildren(IEnumerable<IShaderNode> children, IShaderStatementVisitor visitor)
        {
            if (children != null)
            {
                foreach (IShaderNode child in children)
                {
                    VisitNode(child, visitor);
                }
            }
        }

        private static bool Visit(this IShaderStatementVisitor visitor, IShaderStatement statement)
        {
            if (statement is IShaderAssignmentStatement) { return visitor.VisitAssignment((IShaderAssignmentStatement)statement); }
            if (statement is IShaderDiscardStatement)    { return visitor.VisitDiscard((IShaderDiscardStatement)statement); }
            if (statement is IShaderIfStatement)         { return visitor.VisitIf((IShaderIfStatement)statement); }
            if (statement is IShaderDoStatement)         { return visitor.VisitDo((IShaderDoStatement)statement); }
            if (statement is IShaderWhileStatement)      { return visitor.VisitWhile((IShaderWhileStatement)statement); }
            return false;
        }

        public static void Exit(this IShaderStatementVisitor visitor, IShaderStatement statement)
        {
            if      (statement is IShaderIfStatement)    { visitor.ExitIf((IShaderIfStatement)statement); }
            else if (statement is IShaderDoStatement)    { visitor.ExitDo((IShaderDoStatement)statement); }
            else if (statement is IShaderWhileStatement) { visitor.ExitWhile((IShaderWhileStatement)statement); }
        }
    }
}
