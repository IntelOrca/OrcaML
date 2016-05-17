using System.Text;

namespace OrcaML.Graphics.OpenGL.Extensions
{
    internal static class StringBuilderExtensions
    {
        public static void RemoveEnd(this StringBuilder sb, int length)
        {
            sb.Remove(sb.Length - length, length);
        }
    }
}
