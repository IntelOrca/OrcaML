namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderStatementVisitor
    {
        bool VisitAssignment(IShaderAssignmentStatement statement);
        bool VisitDiscard(IShaderDiscardStatement statement);
        bool VisitIf(IShaderIfStatement statement);
        bool VisitElse(IShaderIfStatement statement);
        bool VisitDo(IShaderDoStatement statement);
        bool VisitWhile(IShaderWhileStatement statement);
        void ExitIf(IShaderIfStatement statement);
        void ExitElse(IShaderIfStatement statement);
        void ExitDo(IShaderDoStatement statement);
        void ExitWhile(IShaderWhileStatement statement);
    }
}
