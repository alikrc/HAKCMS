using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using HAKCMS.Service.Identity;
using HAKCMS.Model.ViewModel;
using System;
using HAKCMS.Web.Core;

namespace HAKCMS.Web.Areas.Admin.Controllers
{
    public class RoleController : BaseControllerForSuperAdmin
    {
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        public RoleController()
        {

        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            this.RoleManager = roleManager;
        }

        public ActionResult Index()
        {
            var roles = RoleManager.GetAll();

            if (Request.IsAjaxRequest())
            {
                //return Json(roles,JsonRequestBehavior.AllowGet);
                return PartialView("_RoleTableItems", roles);
            }
            else
            {
                return View(roles);
            }
        }


        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            string message = "Modal couldn't validated";
            if (ModelState.IsValid)
            {
                var exist = RoleManager.IsRoleExists(model.Name);
                if (exist)
                {
                    message = "That role name has already been used";
                    ViewBag.error = message;
                    return View(model);
                }
                else
                {
                    RoleManager.Create(model.Name);

                    TempData["message"] = "Success!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = message;
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var role = RoleManager.Get(id);
            return View(role);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                RoleManager.Update(model);


                TempData["message"] = "Success!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var role = RoleManager.Get(id);

        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}


        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    var role = RoleManager.Get(id);

        //    RoleManager.Delete(role.Id);

        //    TempData["message"] = "Success!";
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(string id)
        {
            var role = RoleManager.Get(id);
            var message = "Success!";
            try
            {
                RoleManager.Delete(role.Id);
            }
            catch (Exception ex)
            {
                message = "Error!" + ex.Message;

                //todo: log the error

                //throw;
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}