using HAKCMS.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using HAKCMS.Data.Identity;
using HAKCMS.Service.Identity;

namespace HAKCMS.Web.Areas
{
    public class IdentityManager
    {
        // Swap IdentityRole for IdentityRole:

        //RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

       // UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {

            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext _dbContext;// = new ApplicationDbContext();
        public ApplicationDbContext DbContext 
        {
            get
            {
                return _dbContext ?? HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _dbContext = value;
            }
        }


        //public bool RoleExists(string name)
        //{
        //    return RoleManager.RoleExists(name);
        //}


        //public bool CreateRole(string name)
        //{
        //    // Swap IdentityRole for IdentityRole:
        //    var idResult = RoleManager.Create(new IdentityRole(name));
        //    return idResult.Succeeded;
        //}


        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = UserManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = UserManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = UserManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(userId, role.RoleId);
            }
        }

        //public void DeleteRole(string roleId)
        //{
        //    var role = RoleManager.FindById(roleId);
        //    DbContext.Roles.Remove(role);
        //}
    }
}