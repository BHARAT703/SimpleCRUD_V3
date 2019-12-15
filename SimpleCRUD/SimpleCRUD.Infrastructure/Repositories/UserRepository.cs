using SimpleCRUD.Entities.Entities;
using SimpleCRUD.Infrastructure.DatabaseContext;

namespace SimpleCRUD.Infrastructure.Repositories
{
    public interface IUserRepository : IBaseRepository<User> { }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext context;
        public UserRepository(ApplicationContext context)
            : base(context: context)
        {
            this.context = context;
        }
    }
}
