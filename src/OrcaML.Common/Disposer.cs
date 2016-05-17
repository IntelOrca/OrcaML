using System;

namespace OrcaML.Common
{
    public static class Disposer
    {
        public static void Dispose<T>(T instance)
        {
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public static void Dispose(IDisposable instance)
        {
            if (instance != null)
            {
                instance.Dispose();
            }
        }
    }
}
