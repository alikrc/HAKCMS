using HAKCMS.Service;
using HAKCMS.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HAKCMS.Web.Areas.Admin.Controllers
{
    public class PageController :BaseControllerForAdmin
    {
        IPageService PageService;

        public PageController(IPageService PageService)
        {
            this.PageService = PageService;
        }

        //
        // GET: /Admin/Page/
        public ActionResult Index()
        {
            var PageList = PageService.GetAll();
            return View(PageList);
        }

        //
        // GET: /Admin/Page/Details/5
        public ActionResult Details(int id)
        {
            var Page = PageService.Get(id);
            if (Page == null)
            {
                return HttpNotFound();
            }
            return View(Page);
        }

        //
        // GET: /Admin/Page/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Page/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Page/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Page/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Page/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Page/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
