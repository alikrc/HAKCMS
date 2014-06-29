using HAKCMS.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using HAKCMS.Core.Extension;

namespace HAKCMS.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);

        T GetById(int id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private EntityDataContext _context;
        public EntityDataContext Context
        {
            get { return _context ?? (_context = DatabaseFactory.Get()); }
        }

        private readonly IDbSet<T> _dbSet;

        protected IDatabaseFactory DatabaseFactory { get; private set; }

        //ctor
        protected RepositoryBase(IDatabaseFactory dbFactory)
        {
            this.DatabaseFactory = dbFactory;
            this._dbSet = Context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T o in objects)
            {
                _dbSet.Remove(o);

                //_dbSet.Delete(o);
                //Context.Entry(o).State = EntityState.Modified;
            }


        }

        public virtual T GetById(int id)
        {
            if (_dbSet.IsDeletedRecord())
            {
                return null;
            }
            var o = _dbSet.Find(id);
            return o;
        }

        public virtual T GetById(string id)
        {
            if (_dbSet.IsDeletedRecord())
            {
                return null;
            }
            return _dbSet.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            if (_dbSet.IsDeletedRecord())
            {
                return null;
            }

            return _dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList().ExtractDeletedRecords<T>();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }
        /// <summary>
        /// Return a paged list of entities
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
        {
            var results = _dbSet.OrderBy(order).Where(where).GetPage(page).ToList();
            var total = _dbSet.Count(where);
            return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        }
    }
}
