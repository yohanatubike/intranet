using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IntranetPortal.Models
{
    public partial class IntranetDBContext : DbContext
    {
        public IntranetDBContext()
        {
        }

        public IntranetDBContext(DbContextOptions<IntranetDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivitiesDetail> ActivitiesDetails { get; set; } = null!;
        public virtual DbSet<AssignedOfficersDetail> AssignedOfficersDetails { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Designation> Designations { get; set; } = null!;
        public virtual DbSet<FrontEndSlider> FrontEndSliders { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<StaffNotification> StaffNotifications { get; set; } = null!;
        public virtual DbSet<SupportsTicket> SupportsTickets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=IntranetDB;Username=postgres;Password=53401/T2005");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivitiesDetail>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("ActivitiesDetail_pkey");

                entity.ToTable("ActivitiesDetail");

                entity.Property(e => e.ActivityId)
                    .HasColumnName("ActivityID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ActivityCode).HasColumnType("character varying");

                entity.Property(e => e.ActivityName).HasColumnType("character varying");

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentCode).HasMaxLength(100);

                entity.Property(e => e.ImpelementationStatus).HasColumnType("character varying");

                entity.Property(e => e.Priority).HasColumnType("character varying");

                entity.Property(e => e.SectionCode).HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<AssignedOfficersDetail>(entity =>
            {
                entity.HasKey(e => e.AssignedOfficerDetailsId)
                    .HasName("AssignedOfficersDetails_pkey");

                entity.Property(e => e.AssignedOfficerDetailsId)
                    .HasColumnName("AssignedOfficerDetailsID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Pfnumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");

                entity.Property(e => e.UpdateBy).HasColumnType("character varying");

                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

                //entity.HasOne(d => d.Activity)
                //    .WithMany(p => p.AssignedOfficersDetails)
                //    .HasForeignKey(d => d.ActivityId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("OfficersActivityAssignment");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentCode)
                    .HasName("Departments_pkey");

                entity.Property(e => e.DepartmentCode).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DepartmentName).HasMaxLength(200);
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.DesignationCode)
                    .HasName("Positions_pkey");

                entity.Property(e => e.DesignationCode).HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentCode).HasColumnType("character varying");

                entity.Property(e => e.DesignationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DesignationID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.SectionCode).HasMaxLength(100);

                entity.Property(e => e.StaffDesignation).HasMaxLength(100);
            });

            modelBuilder.Entity<FrontEndSlider>(entity =>
            {
                entity.HasKey(e => e.SliderId)
                    .HasName("FrontEndSliders_pkey");

                entity.Property(e => e.SliderId)
                    .HasColumnName("SliderID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.ImageFile).HasMaxLength(200);

                entity.Property(e => e.PublishStatus).HasDefaultValueSql("false");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("PermissionID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.PermissionName).HasMaxLength(100);

                entity.Property(e => e.Pfnumber)
                    .HasMaxLength(100)
                    .HasColumnName("PFNumber");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CretaedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.SectionCode)
                    .HasName("Sections_pkey");

                entity.Property(e => e.SectionCode).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentCode).HasMaxLength(100);

                entity.Property(e => e.SectionId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.SectionName).HasMaxLength(100);
            });

            modelBuilder.Entity<StaffNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("StaffNotifications_pkey");

                entity.Property(e => e.NotificationId)
                    .HasColumnName("NotificationID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.NotificationType).HasColumnType("character varying");

                entity.Property(e => e.Pfnumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<SupportsTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("SupportsTickets_pkey");

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AttendedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentCode).HasColumnType("character varying");

                entity.Property(e => e.LodgedBy).HasColumnType("character varying");

                entity.Property(e => e.Status).HasColumnType("character varying");

                entity.Property(e => e.TicketTypes).HasColumnType("character varying");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.PFNumber)
                    .HasName("Users_pkey");

                entity.Property(e => e.PFNumber)
                    .HasMaxLength(100)
                    .HasColumnName("PFNumber");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DesignationCode).HasMaxLength(100);

                entity.Property(e => e.DutyStation).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MobileNumber).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.ReportingTo).HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.UserRoleId)
                    .HasColumnName("UserRoleID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.PFNumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
