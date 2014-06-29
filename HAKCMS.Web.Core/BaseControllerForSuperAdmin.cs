using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HAKCMS.Web.Core
{
    [Authorize(Roles = "SuperAdmin")]
    public abstract class BaseControllerForSuperAdmin : Controller 
    {
    }
}