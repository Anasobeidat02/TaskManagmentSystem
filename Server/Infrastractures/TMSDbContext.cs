using Domains.Entities;
using Domains.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastractures;

public class TMSDbContext : DbContext
{
    public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.TaskItem)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                FullName = "admin",
                EmailAddress = "info@google.com",
                TaskItem = null,
                PasswordHash = "$2a$11$Vbl4wvQjt1pfIwbNO6MlBOxYzwBgNwZFD6JTbJC8iWpu3.rrCfUTS",
                UpdatedAt = null
            }
        );

        #region Seed Data

        modelBuilder.Entity<TaskPriorty>().HasData(
            new TaskPriorty() { Id = 1, Title = "Low" },
            new TaskPriorty() { Id = 2, Title = "Medium" },
            new TaskPriorty() { Id = 3, Title = "High"},
            new TaskPriorty() { Id = 4, Title = "Urgent"}
        );

        modelBuilder.Entity<Domains.Entities.TaskStatus>().HasData(
            new Domains.Entities.TaskStatus() { Id = 1, Title = "Pending" },
            new Domains.Entities.TaskStatus() { Id = 2, Title = "InProgress" },
            new Domains.Entities.TaskStatus() { Id = 3, Title = "Completed" },
            new Domains.Entities.TaskStatus() { Id = 4, Title = "OnHold" },
            new Domains.Entities.TaskStatus() { Id = 5, Title = "Cancelled" }
        );

        #endregion 
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<TaskPriorty> TaskPriorties { get; set; }
    public DbSet<Domains.Entities.TaskStatus> TaskStatuses { get; set; }
}
