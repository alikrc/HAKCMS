using HAKCMS.Data.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using HAKCMS.Model.ViewModel;
using AutoMapper;
using System.Data.Entity;
using System;
using System.Net;
using Microsoft.Owin.Security;

namespace HAKCMS.Service.Identity
{
    public interface IApplicationUserManager
    {
        ApplicationDbContext DatabaseContext { get; }

        IEnumerable<UserViewModel> GetAll();
        Task<UserViewModel> Get(string id);
        Task<IdentityResult> Create(RegisterViewModel user);
        void Update(UserViewModel user);
        void Delete(string id);
        void Save();
        bool IsUserExists(string email);
    }
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        private ApplicationDbContext _databaseContext;
        public ApplicationDbContext DatabaseContext
        {
            get
            {
                var res = _databaseContext ?? HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
                return res;
            }
            private set
            {
                _databaseContext = value;
            }
        }

        private ApplicationRoleManager _applicationRoleManager;

        public ApplicationRoleManager ApplicationRoleManager
        {
            get { return _applicationRoleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _applicationRoleManager = value; }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            var users = this.DatabaseContext.Users;

            var res = Mapper.Map<List<UserViewModel>>(users);

            //if (res != null)
            //{
            //    var roles = this.ApplicationRoleManager.GetAll();

            //    foreach (var userVm in res)
            //    {
            //        userVm.SelectedRoleIds = this.GetRoles(userVm.Id);
            //    }
            //}

            return res;
        }

        public async Task<UserViewModel> Get(string id)
        {
            var user = this.DatabaseContext.Users.Find(id);

            var res = (user != null ? Mapper.Map<UserViewModel>(user) : null);

            if (res != null)
            {
                var roles = this.GetRoles();

                var roleNames = await this.GetRolesAsync(id);

                var userRoleIds = await ApplicationRoleManager.GetRoleIdsByRoleNames(roleNames);

                res.Roles = roles;

                res.SelectedRoleIds = userRoleIds;
            }

            return res;
        }

        public IEnumerable<RoleViewModel> GetRoles()
        {
            return this.ApplicationRoleManager.GetAll();
        }

        public void Update(UserViewModel model)
        {

            var entity = this.DatabaseContext.Users.Find(model.Id);

            entity.Roles.Clear();

            if (model.SelectedRoleIds != null)
            {
                foreach (var id in model.SelectedRoleIds)
                {
                    entity.Roles.Add(new IdentityUserRole() { RoleId = id, UserId = model.Id });
                }
            }

            Mapper.Map(model, entity);


            DatabaseContext.Entry(entity).State = EntityState.Modified;

            Save();
        }

        public async Task<IdentityResult> Create(RegisterViewModel model)
        {
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
            IdentityResult result = await this.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: false);
            }
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return result;
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";


        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(this));
        }

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private bool HasPassword()
        //{
        //    var user = this.FindById(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        return user.PasswordHash != null;
        //    }
        //    return false;
        //}

        //private void SendEmail(string email, string callbackUrl, string subject, string message)
        //{
        //    // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //}

        //public enum ManageMessageId
        //{
        //    ChangePasswordSuccess,
        //    SetPasswordSuccess,
        //    RemoveLoginSuccess,
        //    Error
        //}

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //private class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //        : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
        #endregion



        public void Delete(string userId)
        {
            var user = this.FindById(userId);

            //should check if has dependencies eg claims
            this.Delete(user);
        }


        public bool IsUserExists(string email)
        {
            var res = this.FindByEmail(email);

            return (res == null ? false : true);
        }
        public void Save()
        {
            this.DatabaseContext.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            DatabaseContext.Dispose();
            base.Dispose(disposing);
        }

    }
}
