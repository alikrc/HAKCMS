using HAKCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAKCMS.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        EntityDataContext Get();
    }

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private EntityDataContext _context;
        public EntityDataContext Get()
        {
            return _context ?? (_context = new EntityDataContext());
        }

        protected override void DisposeCore()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

}
