using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MinimalApi.Models
{
    public class TaskContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public TaskContext(DbContextOptions<TaskContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(cat =>
            {
                cat.ToTable("Cat");
                cat.HasKey(p => p.CategoryId);

                cat.Property(p => p.Name).IsRequired().HasMaxLength(150);

                cat.Property(p => p.Description);
            });

            modelBuilder.Entity<Task>(t =>
            {
                t.ToTable("Tas");
                t.HasKey(tk => tk.TaskId);

                t.HasOne(tk => tk.Category).WithMany(tk => tk.Task).HasForeignKey(P => P.CategoryId);

                t.Property(tk => tk.Title).IsRequired(false).HasMaxLength(150);

                t.Property(tk => tk.Description);

                t.Property(tk => tk.PriorityTask);

                t.Property(tk => tk.CreationDate);

                t.Ignore(tk => tk.Abstract);
            });
        }
    }
}
