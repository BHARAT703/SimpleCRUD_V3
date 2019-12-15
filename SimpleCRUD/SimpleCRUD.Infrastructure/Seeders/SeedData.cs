using SimpleCRUD.Infrastructure.DatabaseContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Infrastructure.Seeders
{
    public static class SeedData
    {
        public static async Task EnsurePopulated(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.AddRange(new Entities.Entities.User() { Name = "Bharat", Email = "Test@test.com", CreationDateTime = DateTime.Now, CreationUserId = 1 });
                context.Users.AddRange(new Entities.Entities.User() { Name = "Karan", Email = "Test@test.com", CreationDateTime = DateTime.Now, CreationUserId = 1 });

                await context.SaveChangesAsync();
            }


        }
    }
}
