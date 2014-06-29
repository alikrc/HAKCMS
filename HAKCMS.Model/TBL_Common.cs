using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAKCMS.Model
{
    public class TBL_Common
    {
        public int MediaID { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
