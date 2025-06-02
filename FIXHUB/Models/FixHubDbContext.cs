using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FIXHUB.Models;

public partial class FixHubDbContext : DbContext
{
    public FixHubDbContext()
    {
    }

    public FixHubDbContext(DbContextOptions<FixHubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ForumComment> ForumComments { get; set; }

    public virtual DbSet<ForumPost> ForumPosts { get; set; }

    public virtual DbSet<GuideCategory> GuideCategories { get; set; }

    public virtual DbSet<GuideStep> GuideSteps { get; set; }

    public virtual DbSet<HistoryStep> HistorySteps { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsComment> NewsComments { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PointTransaction> PointTransactions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<RepairGuide> RepairGuides { get; set; }

    public virtual DbSet<RequestAssignment> RequestAssignments { get; set; }

    public virtual DbSet<RequestFeedback> RequestFeedbacks { get; set; }

    public virtual DbSet<RequestImage> RequestImages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<StepComment> StepComments { get; set; }

    public virtual DbSet<StoreOrder> StoreOrders { get; set; }

    public virtual DbSet<Teardown> Teardowns { get; set; }

    public virtual DbSet<TeardownComponent> TeardownComponents { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<TopUpTransaction> TopUpTransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPoint> UserPoints { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ForumComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__ForumCom__C3B4DFAA7A9A8C10");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__ForumComm__PostI__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ForumComm__UserI__5441852A");
        });

        modelBuilder.Entity<ForumPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__ForumPos__AA1260384A14343A");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ForumPosts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ForumPost__UserI__4F7CD00D");
        });

        modelBuilder.Entity<GuideCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__GuideCat__19093A2B325D23C5");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImgUrl).IsUnicode(false);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_GuideCategories_GuideCategories");
        });

        modelBuilder.Entity<GuideStep>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("PK__GuideSte__243433374C0050E9");

            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.ModifyId).HasColumnName("ModifyID");
            entity.Property(e => e.Title).HasMaxLength(500);

            entity.HasOne(d => d.Guide).WithMany(p => p.GuideSteps)
                .HasForeignKey(d => d.GuideId)
                .HasConstraintName("FK__GuideStep__Guide__38996AB5");

            entity.HasOne(d => d.Modify).WithMany(p => p.InverseModify)
                .HasForeignKey(d => d.ModifyId)
                .HasConstraintName("FK_GuideSteps_GuideSteps");
        });

        modelBuilder.Entity<HistoryStep>(entity =>
        {
            entity.ToTable("HistoryStep");

            entity.Property(e => e.HistoryStepId).HasColumnName("HistoryStepID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ImgUrl).HasMaxLength(250);
            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Step).WithMany(p => p.HistorySteps)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("FK_HistoryStep_GuideSteps");

            entity.HasOne(d => d.User).WithMany(p => p.HistorySteps)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_HistoryStep_Users");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Icon).IsUnicode(false);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Menus_Menus");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__954EBDD3006831F9");

            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsPublished).HasDefaultValue(false);
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.ViewCount).HasDefaultValue(0);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.News)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__News__CreatedBy__2BFE89A6");
        });

        modelBuilder.Entity<NewsComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__NewsComm__C3B4DFAA60623B23");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsApproved).HasDefaultValue(true);
            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.News).WithMany(p => p.NewsComments)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsComme__NewsI__30C33EC3");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__NewsComme__Paren__32AB8735");

            entity.HasOne(d => d.User).WithMany(p => p.NewsComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsComme__UserI__31B762FC");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12043856F1");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Transaction).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Notificat__Trans__43A1090D");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__40C49C62");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C45D183E7");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__4BAC3F29");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__4CA06362");
        });

        modelBuilder.Entity<PointTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__PointTra__55433A6B0AB6A4EE");

            entity.ToTable("PointTransaction");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.Receiver).WithMany(p => p.PointTransactionReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK_PointTransaction_Receiver");

            entity.HasOne(d => d.Sender).WithMany(p => p.PointTransactionSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PointTransaction_Sender");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDCA828FE4");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductName).HasMaxLength(255);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__Products__Produc__440B1D61");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__3224ECEE9C0E96B6");

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<RepairGuide>(entity =>
        {
            entity.HasKey(e => e.GuideId).HasName("PK__RepairGu__E77EE03E4BFCB70F");

            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ImgUrl).IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany(p => p.RepairGuides)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__RepairGui__Categ__33D4B598");

            entity.HasOne(d => d.User).WithMany(p => p.RepairGuides)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RepairGuides_Users");
        });

        modelBuilder.Entity<RequestAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__RequestA__32499E5702BA2E73");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestAssignments)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__RequestAs__Reque__6D0D32F4");

            entity.HasOne(d => d.Technician).WithMany(p => p.RequestAssignments)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("FK__RequestAs__Techn__6E01572D");
        });

        modelBuilder.Entity<RequestFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__RequestF__6A4BEDF620EA444C");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestFeedbacks)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__RequestFe__Reque__74AE54BC");
        });

        modelBuilder.Entity<RequestImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__RequestI__7516F4EC767F8B41");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestImages)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__RequestIm__Reque__71D1E811");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A14DCFBA5");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(50);
        });

        modelBuilder.Entity<ServiceRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__ServiceR__33A8519A372078DA");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PreferredDate).HasColumnType("datetime");
            entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.ServiceTypeId)
                .HasConstraintName("FK__ServiceRe__Servi__656C112C");

            entity.HasOne(d => d.User).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ServiceRe__UserI__6477ECF3");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.HasKey(e => e.ServiceTypeId).HasName("PK__ServiceT__8ADFAA0CE5F62CC2");

            entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<StepComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__StepComm__C3B4DFCACDBFE691");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_StepComments_Parent");

            entity.HasOne(d => d.Step).WithMany(p => p.StepComments)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StepComments_Step");

            entity.HasOne(d => d.User).WithMany(p => p.StepComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StepComments_User");
        });

        modelBuilder.Entity<StoreOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__StoreOrd__C3905BAF43BF02EC");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.StoreOrders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__StoreOrde__UserI__47DBAE45");
        });

        modelBuilder.Entity<Teardown>(entity =>
        {
            entity.HasKey(e => e.TeardownId).HasName("PK__Teardown__E753E203A5B42575");

            entity.Property(e => e.TeardownId).HasColumnName("TeardownID");
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceName).HasMaxLength(255);
            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.Model).HasMaxLength(100);

            entity.HasOne(d => d.Guide).WithMany(p => p.Teardowns)
                .HasForeignKey(d => d.GuideId)
                .HasConstraintName("FK__Teardowns__Guide__3B75D760");
        });

        modelBuilder.Entity<TeardownComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__Teardown__D79CF02ED535F6D7");

            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.ComponentName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.TeardownId).HasColumnName("TeardownID");

            entity.HasOne(d => d.Teardown).WithMany(p => p.TeardownComponents)
                .HasForeignKey(d => d.TeardownId)
                .HasConstraintName("FK__TeardownC__Teard__3F466844");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.TechnicianId).HasName("PK__Technici__301F82C1A62B1D58");

            entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");
            entity.Property(e => e.Expertise).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Technicians)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Technicia__UserI__6A30C649");
        });

        modelBuilder.Entity<TopUpTransaction>(entity =>
        {
            entity.HasKey(e => e.TopUpId).HasName("PK__TopUpTra__84146BC77A0B976A");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionCode).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.TopUpTransactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TopUpTran__UserI__32767D0B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC06422586");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534EDF28240").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserPoint>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserPoin__1788CC4CA758F6C8");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.PointBalance).HasDefaultValue(0);

            entity.HasOne(d => d.User).WithOne(p => p.UserPoint)
                .HasForeignKey<UserPoint>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPoint__UserI__2EA5EC27");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__UserProf__290C8884FA5C5FE5");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AvatarUrl).HasMaxLength(255);
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserProfi__UserI__2F10007B");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A55A700BB17");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__RoleI__2C3393D0");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
