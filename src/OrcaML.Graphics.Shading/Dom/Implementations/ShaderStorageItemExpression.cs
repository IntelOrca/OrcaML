using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    internal class ShaderStorageItemExpression : IShaderStorageItemExpression
    {
        public IEnumerable<IShaderNode> Children => null;

        public IShaderStorageItem StorageItem { get; }

        public ShaderStorageItemExpression(IShaderStorageItem storageItem)
        {
            StorageItem = storageItem;
        }

        public override string ToString()
        {
            return $"{StorageItem.Name}";
        }
    }
}
