using System.Reflection;

namespace OrcaML.Graphics.Shading.Dom
{
    internal class ShaderStorageItem : IShaderStorageItem
    {
        public string Name { get; }
        public IShaderDataTypeInfo DataTypeInfo { get; }
        public StorageItemKind Kind { get; }
        public StorageModifierKind ModifierKind { get; }

        public string GeneratedName => Name;

        private ShaderStorageItem(string name,
                                  IShaderDataTypeInfo dataTypeInfo,
                                  StorageItemKind kind,
                                  StorageModifierKind modifierKind)
        {
            Name = name;
            DataTypeInfo = dataTypeInfo;
            Kind = kind;
            ModifierKind = modifierKind;
        }

        public static ShaderStorageItem CreateLocal(string name, IShaderDataTypeInfo dataTypeInfo)
        {
            return new ShaderStorageItem(name, dataTypeInfo, StorageItemKind.Local, StorageModifierKind.InputOutput);
        }

        public static ShaderStorageItem CreateVertex(FieldInfo fieldInfo)
        {
            string name = fieldInfo.Name;
            IShaderDataTypeInfo dataTypeInfo = DataTypeHelper.GetDataTypeInfo(fieldInfo.FieldType);
            return new ShaderStorageItem(name, dataTypeInfo, StorageItemKind.Vertex, StorageModifierKind.Input);
        }

        public static ShaderStorageItem CreateUniform(PropertyInfo propertyInfo)
        {
            string name = propertyInfo.Name;
            IShaderDataTypeInfo dataTypeInfo = DataTypeHelper.GetDataTypeInfo(propertyInfo.PropertyType);
            return new ShaderStorageItem(name, dataTypeInfo, StorageItemKind.Uniform, StorageModifierKind.Input);
        }

        public static ShaderStorageItem CreateTransferInput(string name, IShaderDataTypeInfo dataTypeInfo)
        {
            return new ShaderStorageItem(name, dataTypeInfo, StorageItemKind.Input, StorageModifierKind.Input);
        }

        public static ShaderStorageItem CreateTransferOutput(string name, IShaderDataTypeInfo dataTypeInfo)
        {
            return new ShaderStorageItem(name, dataTypeInfo, StorageItemKind.Output, StorageModifierKind.Output);
        }

        public static ShaderStorageItem CreateOutputPosition()
        {
            string name = SpecialStorageItems.Position;
            IShaderDataTypeInfo dataTypeInfo = Svec4.StaticDataTypeInfo;
            return new ShaderStorageItem(name, dataTypeInfo, StorageItemKind.Special, StorageModifierKind.Output);
        }

        public static ShaderStorageItem CreateOutputColour()
        {
            IShaderDataTypeInfo dataTypeInfo = Svec4.StaticDataTypeInfo;
            return new ShaderStorageItem("0", dataTypeInfo, StorageItemKind.Output, StorageModifierKind.Output);
        }

        public override string ToString()
        {
            return $"{Kind}: {ModifierKind} {DataTypeInfo} {Name}";
        }
    }
}
