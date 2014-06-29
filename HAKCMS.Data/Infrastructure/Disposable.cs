using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAKCMS.Data.Infrastructure
{
    public class Disposable:IDisposable
    {
        private bool _isDisposed;

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }


        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
