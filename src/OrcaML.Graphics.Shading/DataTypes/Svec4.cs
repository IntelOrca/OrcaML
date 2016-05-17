using OrcaML.Geometry;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class Svec4 : IShaderDataType<Svec4>
    {
        public static readonly IShaderDataTypeInfo StaticDataTypeInfo =
            new ShaderDataTypeInfo<Svec4>("vec4", typeof(Vector4f));

        public IShaderExpression Expression { get; }

        public IShaderDataTypeInfo DataTypeInfo => StaticDataTypeInfo;

        #region Swiggles

        public Sfloat x { get; }
        public Sfloat y { get; }
        public Sfloat z { get; }
        public Sfloat w { get; }
        public Svec3 xyz => new Svec3(x, y, z);

        public Sfloat r => x;
        public Sfloat g => y;
        public Sfloat b => z;
        public Sfloat a => w;
        public Svec3 rgb => xyz;

        #endregion

        public Svec4() { }

        public Svec4(IShaderExpression representation)
        {
            Expression = representation;
            x = new Sfloat(new ShaderFieldExpression(this, "x"));
            y = new Sfloat(new ShaderFieldExpression(this, "y"));
            z = new Sfloat(new ShaderFieldExpression(this, "z"));
            w = new Sfloat(new ShaderFieldExpression(this, "w"));
        }

        public Svec4(Sfloat x, Sfloat y, Sfloat z, Sfloat w)
            : this(new ShaderCallExpression("vec4", x, y, z, w))
        {
        }

        public Svec4(Sfloat xyzw) : this(xyzw, xyzw, xyzw, xyzw) { }
        public Svec4(Svec2 xy, Sfloat z, Sfloat w) : this(xy.x, xy.y, z, w) { }
        public Svec4(Svec3 xyz, Sfloat w) : this(xyz.x, xyz.y, xyz.z, w) { }

        public static Svec4 operator * (Svec4 left, Sfloat right)
        {
            return new Svec4(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Multiply, right));
        }

        public override string ToString()
        {
            return Expression.ToString();
        }
    }
}
