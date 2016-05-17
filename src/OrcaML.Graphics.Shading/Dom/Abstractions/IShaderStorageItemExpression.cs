namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderStorageItemExpression : IShaderExpression
    {
        IShaderStorageItem StorageItem { get; }
    }
}
