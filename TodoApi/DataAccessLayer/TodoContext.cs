using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.DataAccessLayer
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) {}

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>(todoItem =>
            {
                todoItem.Property(item => item.TaskTittle).IsRequired();
                todoItem.Property(item => item.Date).IsRequired();
            });

            modelBuilder.Entity<TodoItem>()
                .Property(s => s.Id)
                 .ValueGeneratedNever();
        }
    }
}
