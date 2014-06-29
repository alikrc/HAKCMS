using HAKCMS.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HAKCMS.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseControllerForAdmin
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}