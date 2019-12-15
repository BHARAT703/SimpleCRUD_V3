using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Infrastructure.DatabaseContext;
using System.Linq;

namespace SimpleCRUD.Infrastructure.Uow
{
    public interface IUnitOfWork
    {
        void Commit();
        void RejectChanges();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
