using HAKCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAKCMS.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _dbFactory;
        private EntityDataContext _context;

        public EntityDataContext Context
        {
            get { return _context ?? (_context = _dbFactory.Get()); }
        }

        public UnitOfWork(IDatabaseFactory dbFac)
        {
            this._dbFactory = dbFac;
        }

        public void Commit()
        {
            Context.Commit();
        }
    }

}
