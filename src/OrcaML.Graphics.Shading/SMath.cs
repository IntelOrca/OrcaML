using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public static class SMath
    {
        public static Sfloat dot(Svec2 a, Svec2 b)
        {
            return new Sfloat(new ShaderCallExpression("dot", a, b));
        }

        public static Svec2 floor(Svec2 xy)
        {
            return new Svec2(new ShaderCallExpression("floor", xy));
        }

        public static Svec2 fract(Svec2 xy)
        {
            return new Svec2(new ShaderCallExpression("fract", xy));
        }

        public static Sfloat mix(Sfloat a, Sfloat b, Sfloat t)
        {
            return new Sfloat(new ShaderCallExpression("mix", a, b, t));
        }

        public static Svec4 mix(Svec4 a, Svec4 b, Sfloat t)
        {
            return new Svec4(new ShaderCallExpression("mix", a, b, t));
        }

        public static Svec2 sin(Svec2 xy)
        {
            return new Svec2(new ShaderCallExpression("sin", xy));
        }

        public static Sfloat smoothstep(Sfloat a, Sfloat b, Sfloat c)
        {
            return new Sfloat(new ShaderCallExpression("smoothstep", a, b, c));
        }
    }
}
