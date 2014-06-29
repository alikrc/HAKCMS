using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HAKCMS.Model.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name="Role Name")]
        public string Name { get; set; }
    }
}