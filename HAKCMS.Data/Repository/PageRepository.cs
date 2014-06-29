using HAKCMS.Data.Infrastructure;
using HAKCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAKCMS.Data.Repository
{
    public interface IPageRepository : IRepository<TBL_Page>
    {

    }

    public class PageRepository : RepositoryBase<TBL_Page>, IPageRepository
    {
        public PageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
