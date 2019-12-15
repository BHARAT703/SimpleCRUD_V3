using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Entities.Entities;
using System;

namespace SimpleCRUD.Infrastructure.DatabaseContext
{
    public class ApplicationContext : DbContext, IDisposable
    {
        public ApplicationContext(DbContextOptions options)
            : base(options: options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                //item.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder: modelBuilder);
        }

        public DbSet<User> Users { get; set; }                
    }
}
