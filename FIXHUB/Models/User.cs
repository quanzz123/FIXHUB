using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();

    public virtual ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();

    public virtual ICollection<HistoryStep> HistorySteps { get; set; } = new List<HistoryStep>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PointTransaction> PointTransactionReceivers { get; set; } = new List<PointTransaction>();

    public virtual ICollection<PointTransaction> PointTransactionSenders { get; set; } = new List<PointTransaction>();

    public virtual ICollection<RepairGuide> RepairGuides { get; set; } = new List<RepairGuide>();

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    public virtual ICollection<StepComment> StepComments { get; set; } = new List<StepComment>();

    public virtual ICollection<StoreOrder> StoreOrders { get; set; } = new List<StoreOrder>();

    public virtual ICollection<Technician> Technicians { get; set; } = new List<Technician>();

    public virtual ICollection<TopUpTransaction> TopUpTransactions { get; set; } = new List<TopUpTransaction>();

    public virtual UserPoint? UserPoint { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
