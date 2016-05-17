using OrcaML.Geometry;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class Svec3 : IShaderDataType<Svec3>
    {
        public static readonly IShaderDataTypeInfo StaticDataTypeInfo =
            new ShaderDataTypeInfo<Svec3>("vec3", typeof(Vector3f));

        public IShaderExpression Expression { get; }

        public IShaderDataTypeInfo DataTypeInfo => StaticDataTypeInfo;

        public Sfloat x { get; }
        public Sfloat y { get; }
        public Sfloat z { get; }

        public Svec3(IShaderExpression representation)
        {
            Expression = representation;
            x = new Sfloat(new ShaderFieldExpression(this, "x"));
            y = new Sfloat(new ShaderFieldExpression(this, "y"));
            z = new Sfloat(new ShaderFieldExpression(this, "z"));
        }

        public Svec3(Sfloat xyz) : this(xyz, xyz, xyz) { }

        public Svec3(Sfloat x, Sfloat y, Sfloat z) : this(new ShaderCallExpression("vec3", x, y, z)) { }

        public override string ToString()
        {
            return Expression.ToString();
        }
    }
}
