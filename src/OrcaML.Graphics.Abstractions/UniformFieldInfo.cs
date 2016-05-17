namespace OrcaML.Graphics.Abstractions
{
    public class UniformFieldInfo : IShaderDataItem
    {
        public string Name { get; }
        public int Components { get; }
        public ShaderDataType Type { get; }

        public UniformFieldInfo(string name, int components, ShaderDataType type)
        {
            Name = name;
            Components = components;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Name}: {Type}*{Components}";
        }
    }
}
