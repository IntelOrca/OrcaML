namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderAssignmentStatement : IShaderStatement
    {
        IShaderStorageItem Destination { get; }
        IShaderExpression Source { get; }
    }
}
