using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;
using HAKCMS.Data.Identity;
using HAKCMS.Service.Identity;
using HAKCMS.Model.ViewModel;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using HAKCMS.Web.Core;

namespace HAKCMS.Web.Areas.Admin.Controllers
{
    public class UserController : BaseControllerForSuperAdmin
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UserController()
        {

        }

        public UserController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            var users = UserManager.GetAll();
            if (Request.IsAjaxRequest())
            {
                //return Json(roles,JsonRequestBehavior.AllowGet);
                return PartialView("_UserTableItems", users);
            }
            else
            {
                return View(users);
            }
        }


        // GET: Admin/User/Create
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            string message = "Modal couldn't validated";
            if (ModelState.IsValid)
            {
                var exist = UserManager.IsUserExists(model.Email);
                if (exist)
                {
                    message = "That email has already been used!";
                    ModelState.AddModelError("", message);
                    return View(model);
                }

                var result = await UserManager.Create(model);

                if (result.Succeeded)
                {
                    TempData["message"] = "Success!";
                    return RedirectToAction("Index");
                }
                else
                {
                    message = "An error occured.";
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

            }
            ViewBag.error = message;
            return View(model);
        }

        // GET: Admin/User/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.Get(id);
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    UserManager.Update(model);
                    TempData["message"] = "Success!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "An error occured when update occurs: " + e.Message);
                }

            }

            //if comes here take roles
            model.Roles = UserManager.GetRoles();

            return View(model);
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(string id)
        {
            var user = await UserManager.Get(id);
            var message = "Success!";
            try
            {
                UserManager.Delete(user.Id);
            }
            catch (Exception ex)
            {
                message = "Error!" + ex.Message;

                //todo: log the error

                //throw;
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
    }
}
