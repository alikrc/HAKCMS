using HAKCMS.Data.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using HAKCMS.Model.ViewModel;
using System.Threading.Tasks;

namespace HAKCMS.Service.Identity
{
    public interface IApplicationRoleManager
    {
        ApplicationDbContext DatabaseContext { get; }

        RoleViewModel Get(string id);
        IEnumerable<RoleViewModel> GetAll();
        void Create(string roleName);
        void Update(RoleViewModel role);
        void Delete(string roleId);
        void Save();
        bool IsRoleExists(string roleName);
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>, IApplicationRoleManager
    {

        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {

        }


        private ApplicationDbContext _databaseContext;// = new ApplicationDbContext();
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


        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var dbContext = context.Get<ApplicationDbContext>();

            var roleStore = new RoleStore<IdentityRole>(dbContext);

            var manager = new ApplicationRoleManager(roleStore);

            return manager;
        }

        public RoleViewModel Get(string id)
        {
            var role = this.DatabaseContext.Roles.Find(id);

            var res = (role != null ? Mapper.Map<RoleViewModel>(role) : null);

            return res;
        }

        public IEnumerable<RoleViewModel> GetAll()
        {
            var roles = this.DatabaseContext.Roles;

            var res = Mapper.Map<List<RoleViewModel>>(roles);

            return res;
        }

        public void Create(string roleName)
        {
            this.Create(new IdentityRole(roleName));
        }

        public void Delete(string roleId)
        {
            var role = this.FindById(roleId);

            this.Delete(role);

        }

        public bool IsRoleExists(string roleName)
        {
            return this.RoleExists(roleName);
        }

        public void Update(RoleViewModel role)
        {
            var entity = this.DatabaseContext.Roles.Find(role.Id);
            entity.Name = role.Name;

            DatabaseContext.Entry(entity).State = EntityState.Modified;
            Save();
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
        #region helpers
        public async Task<string> GetRoleIdByRoleName(string roleName)
        {


            var role = await this.DatabaseContext.Roles.FirstOrDefaultAsync(w => w.Name == roleName);
            return role.Id;

        }
        #endregion

        public async Task<IEnumerable<string>> GetRoleIdsByRoleNames(IList<string> roleNames)
        {
            var res = new List<string>();

            foreach (var rn in roleNames)
            {
                var id = await GetRoleIdByRoleName(rn);
                res.Add(id);
            }

            return res;
        }
    }
}
