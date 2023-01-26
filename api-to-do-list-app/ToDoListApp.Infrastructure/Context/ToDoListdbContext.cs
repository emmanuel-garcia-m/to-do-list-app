using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Domain.Models;
using DomainApp = ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Context
{
    public class ToDoListdbContext : DbContext
    {
        public ToDoListdbContext(DbContextOptions<ToDoListdbContext> options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;                        
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdatedDate = DateTime.Now;                       
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>()
               .Property(b => b.description).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<DomainApp.ToDoList>()
                .Property(b => b.description).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<DomainApp.ToDoList>()
                .HasMany(t => t.TasksList)
                .WithOne(t => t.ToDoList)
                .HasForeignKey(t => t.ToDoListId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

           
        }

        public DbSet<DomainApp.ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }


    }
}
