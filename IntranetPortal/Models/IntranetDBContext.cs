using IntranetPortal.Models.Planning;
using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<ActivitiesComment> ActivitiesComments { get; set; } = null!;
        public virtual DbSet<ActivitiesDetail> ActivitiesDetails { get; set; } = null!;
        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<AssignedOfficersDetail> AssignedOfficersDetails { get; set; } = null!;
        public virtual DbSet<BusinessCard> BusinessCards { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Designation> Designations { get; set; } = null!;
        public virtual DbSet<Documentation> Documentations { get; set; } = null!;
        public virtual DbSet<FrontEndSlider> FrontEndSliders { get; set; } = null!;
        public virtual DbSet<Meeting> Meetings { get; set; } = null!;
        public virtual DbSet<MeetingComment> MeetingComments { get; set; } = null!;
        public virtual DbSet<MeetingInvitation> MeetingInvitations { get; set; } = null!;
        public virtual DbSet<NewsEvent> NewsEvents { get; set; } = null!;
        public virtual DbSet<NotificationComment> NotificationComments { get; set; } = null!;
        public virtual DbSet<SupportIncharge> SupportIncharge { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionCategory> PermissionCategories { get; set; } = null!;
        public virtual DbSet<AuditingDetail> AuditingDetail { get; set; } = null!;
        public virtual DbSet<QuizeManager> QuizeManagers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
        public virtual DbSet<ServiceSubCategory> ServiceSubCategories { get; set; } = null!;
        public virtual DbSet<StaffNotification> StaffNotifications { get; set; } = null!;
        public virtual DbSet<SupportsTicket> SupportsTickets { get; set; } = null!;
        public virtual DbSet<SupportsTicketsComment> SupportsTicketsComments { get; set; } = null!;
        public virtual DbSet<TasacForm> TasacForms { get; set; } = null!;
        public virtual DbSet<TasacLibrary> TasacLibraries { get; set; } = null!;
        public virtual DbSet<TasacSystem> TasacSystems { get; set; } = null!;
        public virtual DbSet<Tip> Tips { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Objective> Objectives { get; set; } = null!;
        public virtual DbSet<Strategy> Strategies { get; set; } = null!;
        public virtual DbSet<Target> Targets { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Indicator> Indicators { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=IntranetDB;Username=postgres;Password=53401/T2005");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivitiesComment>(entity =>
            {
                entity.HasKey(e => e.ActivitiesCommentsId)
                    .HasName("ActivitiesComments_pkey");

                entity.Property(e => e.ActivitiesCommentsId)
                    .ValueGeneratedNever()
                    .HasColumnName("ActivitiesCommentsID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            });

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
                entity.Property(e => e.Title).HasColumnType("character varying");
                entity.Property(e => e.PublishStatus).HasColumnType("character varying");
                entity.Property(e => e.PublishedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.PublishdBy).HasColumnType("character varying");
                entity.Property(e => e.ExternalDetails).HasColumnType("character varying");



            });
            modelBuilder.Entity<AuditingDetail>(entity =>
            {
                entity.HasKey(e => e.AuditingDetailID)
                    .HasName("AuditingDetailID_pkey");

         


                entity.Property(e => e.DepartmentCode).HasColumnType("character varying");

                entity.Property(e => e.SectionCode).HasColumnType("character varying");

                entity.Property(e => e.RecordedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");


                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");



            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.ArticleId)
                    .HasColumnName("ArticleID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.Title).HasColumnType("character varying");

                entity.Property(e => e.Url)
                    .HasColumnType("character varying")
                    .HasColumnName("url");
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
            });

            modelBuilder.Entity<BusinessCard>(entity =>
            {
                entity.HasKey(e => e.Pfnumber)
                    .HasName("BusinessCards_pkey");

                entity.Property(e => e.Pfnumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");

                entity.Property(e => e.Email).HasColumnType("character varying");

                entity.Property(e => e.Fax).HasColumnType("character varying");

                entity.Property(e => e.FirstName).HasColumnType("character varying");

                entity.Property(e => e.LastName).HasColumnType("character varying");

                entity.Property(e => e.MiddleName).HasColumnType("character varying");

                entity.Property(e => e.MobileNumber).HasColumnType("character varying");

                entity.Property(e => e.Telephone).HasColumnType("character varying");

                entity.Property(e => e.Title).HasColumnType("character varying");
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

                entity.Property(e => e.DesignationCode).HasMaxLength(200);

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

            modelBuilder.Entity<Documentation>(entity =>
            {
                entity.Property(e => e.Status).HasColumnName("status");
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

            modelBuilder.Entity<SupportIncharge>(entity =>
            {
                entity.HasKey(e => e.SupportInchargeID)
                    .HasName("SupportIncharge_pkey");

                entity.Property(e => e.SupportInchargeID)
                    .HasColumnName("SupportInchargeID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AssignedBy).HasColumnType("character varying");
                entity.Property(e => e.AssignedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.InchargeName).HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("false");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });
            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.MeetingId)
                    .HasColumnName("MeetingID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.DepartmentCode).HasColumnType("character varying");

                entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Location).HasColumnType("character varying");

                entity.Property(e => e.MeetingCode).HasColumnType("character varying");

                entity.Property(e => e.SectionCode).HasColumnType("character varying");

                entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Status).HasColumnType("character varying");

                entity.Property(e => e.Title).HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<MeetingComment>(entity =>
            {
                entity.Property(e => e.MeetingCommentId)
                    .HasColumnName("MeetingCommentID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.MeetingId).HasColumnName("MeetingID");

                entity.Property(e => e.Pfnumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");
            });

            modelBuilder.Entity<MeetingInvitation>(entity =>
            {
                entity.ToTable("MeetingInvitation");

                entity.Property(e => e.MeetingInvitationId)
                    .HasColumnName("MeetingInvitationID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AcceptanceStatus).HasColumnType("character varying");
                entity.Property(e => e.InvitedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.UpdateDate).HasColumnType("timestamp without time zone");
               
                entity.Property(e => e.InvitedBy).HasColumnType("character varying");

                entity.Property(e => e.IsFocalPerson).HasDefaultValueSql("false");

                entity.Property(e => e.MeetingId).HasColumnName("MeetingID");

                entity.Property(e => e.Pfnumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<NewsEvent>(entity =>
            {
                entity.HasKey(e => e.NewsEventsId)
                    .HasName("NewsEvents_pkey");

                entity.Property(e => e.NewsEventsId)
                    .HasColumnName("NewsEventsID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.Status).HasColumnType("character varying");
                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
                entity.Property(e => e.Title).HasColumnType("character varying");
                entity.Property(e => e.IsTopNews).HasDefaultValueSql("false");
            });

            modelBuilder.Entity<NotificationComment>(entity =>
            {
                entity.HasKey(e => e.NitificationCommentId)
                    .HasName("NotificationComment_pkey");

                entity.ToTable("NotificationComment");

                entity.Property(e => e.NitificationCommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("NitificationCommentID");

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("PermissionID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address).HasColumnType("character varying");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.PermissionName).HasMaxLength(100);

                entity.Property(e => e.Pfnumber)
                    .HasMaxLength(100)
                    .HasColumnName("PFNumber");

                entity.Property(e => e.Status).HasDefaultValueSql("true");
            });

            modelBuilder.Entity<PermissionCategory>(entity =>
            {
                entity.Property(e => e.PermissionCategoryId)
                    .HasColumnName("PermissionCategoryID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address).HasColumnType("character varying");

                entity.Property(e => e.PermissionName).HasColumnType("character varying");
            });

            modelBuilder.Entity<QuizeManager>(entity =>
            {
                entity.HasKey(e => e.QuizManagerId)
                    .HasName("QuizeManager_pkey");

                entity.ToTable("QuizeManager");

                entity.Property(e => e.QuizManagerId)
                    .HasColumnName("QuizManagerID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.Status).HasDefaultValueSql("false");
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

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ServiceCategory");

                entity.Property(e => e.ServiceCategoryCode).HasColumnType("character varying");

                entity.Property(e => e.ServiceCategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ServiceCategoryID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ServiceName).HasColumnType("character varying");
            });

            modelBuilder.Entity<ServiceSubCategory>(entity =>
            {
                entity.ToTable("ServiceSubCategory");

                entity.Property(e => e.ServiceSubCategoryId)
                    .HasColumnName("ServiceSubCategoryID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ServiceCategoryCode).HasColumnType("character varying");

                entity.Property(e => e.ServiceSubCategoryName).HasColumnType("character varying");
            });

            modelBuilder.Entity<StaffNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("StaffNotification_pkey");

                entity.Property(e => e.NotificationId)
                    .HasColumnName("NotificationID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.EnableComment).HasDefaultValueSql("false");

                entity.Property(e => e.Pfnumber)
                    .HasColumnType("character varying")
                    .HasColumnName("PFNumber");

                entity.Property(e => e.Status).HasColumnType("character varying");

                entity.Property(e => e.Subject).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<SupportsTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("SupportsTickets_pkey");

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AssignedBy).HasColumnType("character varying");
                entity.Property(e => e.AttendedBy).HasColumnType("character varying");
                entity.Property(e => e.AssignedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.AttendedDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.AssignedStatus).HasColumnType("character varying");

                entity.Property(e => e.AssignedTo).HasMaxLength(100);

                entity.Property(e => e.ClosedBy).HasColumnType("character varying");

                entity.Property(e => e.ClosedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentCode).HasColumnType("character varying");

                entity.Property(e => e.LodgedBy).HasColumnType("character varying");

                entity.Property(e => e.ServiceCategoryCode).HasColumnType("character varying");

                entity.Property(e => e.Status).HasColumnType("character varying");

                entity.Property(e => e.TicketTypes).HasColumnType("character varying");
            });

            modelBuilder.Entity<SupportsTicketsComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("SupportsTicketsComments_pkey");

                entity.Property(e => e.CommentId)
                    .HasColumnName("CommentID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");
            });

            modelBuilder.Entity<TasacForm>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("Forms_pkey");

                entity.Property(e => e.FormId).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.DepartmentCode).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.FileUrl).HasColumnType("character varying");

                entity.Property(e => e.FormName).HasColumnType("character varying");

                entity.Property(e => e.Status).HasColumnType("character varying");
            });

            modelBuilder.Entity<TasacLibrary>(entity =>
            {
                entity.HasKey(e => e.LibraryId);
            });

            modelBuilder.Entity<TasacSystem>(entity =>
            {
                entity.HasKey(e => e.SystemId);
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.HasKey(e => e.TipsId)
                    .HasName("Tips_pkey");

                entity.Property(e => e.TipsId)
                    .ValueGeneratedNever()
                    .HasColumnName("TipsID");

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Title).HasColumnType("character varying");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.PFNumber)
                    .HasName("Users_pkey");

                entity.Property(e => e.PFNumber)
                    .HasMaxLength(100)
                    .HasColumnName("PFNumber");
                entity.Property(e => e.BirthMonth)
                    .HasColumnName("BirthMonth")
                    .HasColumnType("int");

                entity.Property(e => e.BirthDay)
                  .HasColumnName("BirthDay")
                  .HasColumnType("int");
                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
               
                entity.Property(e => e.DesignationCode).HasMaxLength(100);
                entity.Property(e => e.PictureUrl).HasColumnType("character varying");
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

            modelBuilder.Entity<Objective>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Code)
                    .HasColumnType("character varying")
                    .HasColumnName("Code");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("Description");

            });

            modelBuilder.Entity<Strategy>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Code)
                    .HasColumnType("character varying")
                    .HasColumnName("Code");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("Description");

            });

            modelBuilder.Entity<Indicator>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Value)
                    .HasColumnType("double precision")
                    .HasColumnName("Value");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("Description");

            });

            modelBuilder.Entity<Target>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");
                entity.Property(e => e.IndicatorId).HasColumnType("integer");


                entity.Property(e => e.CreatedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Code)
                    .HasColumnType("character varying")
                    .HasColumnName("Code");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("Description");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
