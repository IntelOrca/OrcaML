using OrcaML.Geometry;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class Svec2 : IShaderDataType<Svec2>
    {
        public static readonly IShaderDataTypeInfo StaticDataTypeInfo =
            new ShaderDataTypeInfo<Svec2>("vec2", typeof(Vector2f));

        public IShaderExpression Expression { get; }

        public IShaderDataTypeInfo DataTypeInfo => StaticDataTypeInfo;

        #region Swiggles

        public Sfloat x { get; }
        public Sfloat y { get; }
        public Svec2 xy => this;
        public Svec2 yx => new Svec2(y, x);

        public Sfloat s => x;
        public Sfloat t => y;
        public Svec2 st => xy;
        public Svec2 ts => yx;

        public Sfloat r => x;
        public Sfloat g => y;
        public Svec2 rg => xy;
        public Svec2 gr => yx;

        #endregion

        public Svec2() { }

        public Svec2(IShaderExpression representation)
        {
            Expression = representation;
            x = new Sfloat(new ShaderFieldExpression(this, "x"));
            y = new Sfloat(new ShaderFieldExpression(this, "y"));
        }

        public Svec2(Sfloat x, Sfloat y) : this(new ShaderCallExpression("vec2", x, y)) { }

        public Svec2(Sfloat xy) : this(xy, xy) { }

        public Svec2(Vector2f xy) : this(xy.X, xy.Y) { }

        public override string ToString()
        {
            return Expression.ToString();
        }

        public static implicit operator Svec2(Vector2f constant)
        {
            return new Svec2(constant);
        }

        public static Svec2 operator +(Svec2 left, Svec2 right)
        {
            return new Svec2(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Add, right));
        }

        public static Svec2 operator +(Svec2 left, Sfloat right)
        {
            return new Svec2(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Add, right));
        }

        public static Svec2 operator +(Sfloat left, Svec2 right)
        {
            return new Svec2(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Add, right));
        }

        public static Svec2 operator -(Svec2 left, Svec2 right)
        {
            return new Svec2(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Subtract, right));
        }

        public static Svec2 operator *(Svec2 left, Sfloat right)
        {
            return new Svec2(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Multiply, right));
        }

        public static Svec2 operator *(Sfloat left, Svec2 right)
        {
            return new Svec2(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Multiply, right));
        }
    }
}
