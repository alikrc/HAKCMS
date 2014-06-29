using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HAKCMS.Model.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        public virtual IEnumerable<RoleViewModel> Roles { get; set; }

        public IEnumerable<string> SelectedRoleIds { get; set; }

        //[MaxLength(15)]
        //[DataType(DataType.PhoneNumber)]
        //[Display(Name = "Phone Number")]
        //public virtual string PhoneNumber { get; set; }

        //[Display(Name = "Two Factor Enabled")]
        //public virtual bool TwoFactorEnabled { get; set; }

        //[Required]
        //[MaxLength(50)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Compare("Password")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //public string ConfirmPassword { get; set; }

        //public IList<string> UserRoleNames { get; set; }




    }




    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}