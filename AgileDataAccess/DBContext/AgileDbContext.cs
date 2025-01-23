using AgileDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgileDataAccess.DBContext
{
    public class AgileDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Entities.TaskItem> Tasks { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<TaskTeam> TaskTeams { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AgileDbContext(DbContextOptions<AgileDbContext> options)
        : base(options)
            {
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasMany(e => e.ProjectTeams)
                      .WithOne(pt => pt.Project)
                      .HasForeignKey(pt => pt.ProjectID);
                entity.HasMany(e => e.Tasks)
                      .WithOne(t => t.Project)
                      .HasForeignKey(t => t.ProjectID);
                entity.HasMany(e => e.Sprints)
                      .WithOne(s => s.Project)
                      .HasForeignKey(s => s.ProjectID);
            });

            modelBuilder.Entity<Entities.TaskItem>(entity =>
            {
                entity.HasKey(e => e.TaskID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasOne(e => e.Sprint)
                      .WithMany(s => s.Tasks)
                      .HasForeignKey(e => e.SprintID);
                entity.HasMany(e => e.TaskTeams)
                      .WithOne(tt => tt.Task)
                      .HasForeignKey(tt => tt.TaskID);
            });

            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.HasKey(e => e.SprintID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ProjectTeam>(entity =>
            {
                entity.HasKey(e => e.ProjectTeamID);
                entity.HasOne(e => e.Project)
                      .WithMany(p => p.ProjectTeams)
                      .HasForeignKey(e => e.ProjectID);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.ProjectTeams)
                      .HasForeignKey(e => e.UserID);
            });

            modelBuilder.Entity<TaskTeam>(entity =>
            {
                entity.HasKey(e => e.TaskTeamID);
                entity.HasOne(e => e.Task)
                      .WithMany(t => t.TaskTeams)
                      .HasForeignKey(e => e.TaskID);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.TaskTeams)
                      .HasForeignKey(e => e.UserID);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommentID);
                entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(e => e.UserID);
            });
        }
    }
}
