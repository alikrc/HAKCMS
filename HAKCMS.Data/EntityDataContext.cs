namespace HAKCMS.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using HAKCMS.Model;

    public partial class EntityDataContext : DbContext
    {
        public EntityDataContext()
            : base("name=EntityDataModel")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityDataContext>());
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<TBL_Gallery> TBL_Gallery { get; set; }
        public virtual DbSet<TBL_Media> TBL_Media { get; set; }
        public virtual DbSet<TBL_Page> TBL_Page { get; set; }
        public virtual DbSet<TBL_PageGallery> TBL_PageGallery { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);
        }
    }
}

