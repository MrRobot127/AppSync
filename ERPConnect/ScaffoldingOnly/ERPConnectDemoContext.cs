using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERPConnect.Web.ScaffoldingOnly
{
    public partial class ERPConnectDemoContext : DbContext
    {
        public ERPConnectDemoContext()
        {
        }

        public ERPConnectDemoContext(DbContextOptions<ERPConnectDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DELL-PC\\SQLEXPRESS2019;Initial Catalog=ERPConnectDemo;Persist Security Info=True;User ID=sa;Password=Sh@un12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
