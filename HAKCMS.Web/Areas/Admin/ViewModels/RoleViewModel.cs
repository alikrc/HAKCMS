using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HAKCMS.Web.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {
        public string RoleName { get; set; }

        public string Id { get; set; }

        public RoleViewModel() { }
        public RoleViewModel(IdentityRole role)
        {
            this.RoleName = role.Name;
            this.Id = role.Id;
        }
    }
}