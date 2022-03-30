using System;

namespace XCOM.Schema.Standard.System
{
    public abstract class XMDisposableResource : IDisposable
    {
        private bool isDisposed;

        /// <summary>
        /// 终结器
        /// </summary>
        ~XMDisposableResource()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            Dispose(true);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        public abstract void Dispose(bool disposing);
    }
}
