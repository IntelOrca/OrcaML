using System;

namespace OpenML.Common
{
    public static class Guard
    {
        public static void ArgumentNotNull(string parameterName, object argumentValue)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ArgumentInRange(string parameterName, int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
    }
}
