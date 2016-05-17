using System;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLException : Exception
    {
        public OpenGLException() { }
        public OpenGLException(string message) : base(message) { }
        public OpenGLException(string message, Exception inner) : base(message, inner) { }

        public static void ThrowIfZero(int id, string functionName)
        {
            if (id == 0)
            {
                throw new OpenGLException($"{functionName} returned 0.");
            }
        }

        public static void ThrowIfNegativeOne(int id, string functionName)
        {
            if (id == -1)
            {
                throw new OpenGLException($"{functionName} returned -1.");
            }
        }
    }
}
