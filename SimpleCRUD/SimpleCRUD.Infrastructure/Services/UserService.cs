using SimpleCRUD.Entities.Entities;
using SimpleCRUD.Infrastructure.Repositories;
using SimpleCRUD.Infrastructure.Uow;

namespace SimpleCRUD.Infrastructure.Services
{
    public interface IUserService : IBaseServiceWithFullAudit<User> { }

    public class UserService : BaseServiceWithFullAudit<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repository)
            : base(unitOfWork, repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }
    }
}
