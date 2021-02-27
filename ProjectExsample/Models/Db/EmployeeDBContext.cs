using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectExsample.Models.Db
{
    public partial class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<MasterGender> MasterGender { get; set; }
        public virtual DbSet<MasterPosition> MasterPosition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("  Data Source=HP-SSD\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True  ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.Surname).HasMaxLength(250);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Employees_MasterGender");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Employees_MasterPosition");
            });

            modelBuilder.Entity<MasterGender>(entity =>
            {
                entity.HasKey(e => e.GenderId);

                entity.Property(e => e.GenderId)
                    .HasColumnName("GenderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GenderName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterPosition>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.Property(e => e.PositionId)
                    .HasColumnName("PositionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PositionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
