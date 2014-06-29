using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace HAKCMS.Data
{
    public partial class EntityDataContext : DbContext
    {
        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
