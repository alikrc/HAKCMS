using HAKCMS.Data.Identity;
using HAKCMS.Model;
using HAKCMS.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HAKCMS.Web.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }

        public UserViewModel(ApplicationUser user)
        {
            // TODO: Complete member initialization
            this.Id = user.Id;
            this.UserName = user.UserName;
        }

        public string Id { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<IdentityUserRole> Roles { get; set; }
    }
}