﻿using AppSync.Web.Models.Entity_Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppSync.Web.Models.Context
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    }
    public partial class AppDbContext
    {
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<CompanyGroup> CompanyGroups { get; set; } = null!;
        public virtual DbSet<MenuItem> MenuItems { get; set; } = null!;
        public virtual DbSet<Otpverification> Otpverifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(e => e.MenuItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("MenuItemID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParentMenuItemId).HasColumnName("ParentMenuItemID");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.ParentMenuItem)
                    .WithMany(p => p.InverseParentMenuItem)
                    .HasForeignKey(d => d.ParentMenuItemId)
                    .HasConstraintName("FK_MenuItems_ParentMenuItemID");
            });

            modelBuilder.Entity<CompanyGroup>(entity =>
            {
                entity.ToTable("CompanyGroup");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.GroupName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Address1).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Esino)
                    .HasMaxLength(255)
                    .HasColumnName("ESINo");

                entity.Property(e => e.FaxNo).HasMaxLength(255);

                entity.Property(e => e.HeadOffice).HasMaxLength(255);

                entity.Property(e => e.InvolvingIndustry).HasMaxLength(255);

                entity.Property(e => e.KeyDesignation).HasMaxLength(255);

                entity.Property(e => e.KeyPerson).HasMaxLength(255);

                entity.Property(e => e.KeyPersonAddress).HasMaxLength(255);

                entity.Property(e => e.KeyPersonDob)
                    .HasMaxLength(255)
                    .HasColumnName("KeyPersonDOB");

                entity.Property(e => e.KeyPersonPhNo).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PanNo).HasMaxLength(255);

                entity.Property(e => e.Pfno)
                    .HasMaxLength(255)
                    .HasColumnName("PFNo");

                entity.Property(e => e.PhoneNo).HasMaxLength(255);

                entity.Property(e => e.RegNo).HasMaxLength(255);

                entity.Property(e => e.RegistrationDate).HasMaxLength(255);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Otpverification>(entity =>
            {
                entity.ToTable("OTPVerification");

                entity.Property(e => e.ExpirationTime).HasColumnType("datetime");

                entity.Property(e => e.FromEmail).HasMaxLength(256);

                entity.Property(e => e.Otp)
                    .HasMaxLength(10)
                    .HasColumnName("OTP");

                entity.Property(e => e.SentOn).HasColumnType("datetime");

                entity.Property(e => e.ToEmail).HasMaxLength(256);

                entity.Property(e => e.Type)
                    .HasMaxLength(256)
                    .HasColumnName("TYPE");

                entity.Property(e => e.UserId).HasMaxLength(256);
            });

            SeedInitialData(modelBuilder);
        }

        protected void SeedInitialData(ModelBuilder modelBuilder)
        {
            const string ADMIN_ID = "6544a9ce-b9a5-42ba-8fdc-9f498c14a745";

            const string ROLE_ID = "b8feecc3-3e04-4dff-a74f-b14e77ede662";

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,

                UserName = "admin@secureapp.com",
                NormalizedUserName = "admin@secureapp.com".ToUpper(),
                Email = null,
                NormalizedEmail = null,
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            // Add the "FirstTimeLogin" claim to the admin user
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = ADMIN_ID,
                ClaimType = "FirstTimeLogin",
                ClaimValue = "True"
            });
        }

    }
}
