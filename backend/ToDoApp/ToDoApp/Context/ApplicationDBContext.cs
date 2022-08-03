using Microsoft.EntityFrameworkCore;
using ToDoApp.Configurations;
using ToDoApp.Context.Seeders;
using ToDoApp.Models.Entities;

namespace ToDoApp.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        #region DbSet
        public virtual DbSet<TodoEntity> todos { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
            modelBuilder.Seed();
        }
    }
}
