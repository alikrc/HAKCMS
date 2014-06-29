namespace HAKCMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetRoles = new HashSet<AspNetRoles>();
        }

        public string Id { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public bool? TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool? Lockoutenabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(128)]
        public string ConfirmationToken { get; set; }

        public bool? IsConfirmed { get; set; }

        public DateTime? LastPasswordFailureDate { get; set; }

        public int? PasswordFailuresSinceLastSuccess { get; set; }

        public DateTime? PasswordChangedDate { get; set; }

        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }

        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}
