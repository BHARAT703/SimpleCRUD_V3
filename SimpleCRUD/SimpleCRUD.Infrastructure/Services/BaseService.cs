using SimpleCRUD.Entities.Helpers;
using SimpleCRUD.Infrastructure.Repositories;
using SimpleCRUD.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleCRUD.Infrastructure.Services
{
    public interface IBaseServiceWithFullAudit<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T GetById(int Id);
        T GetById(string Id);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Response<T> Insert(T model);
        Response<T> Update(T model);
        Response<T> SoftDelete(T model);
        Response<T> HardDelete(T model);
    }
    public class BaseServiceWithFullAudit<T> : IBaseServiceWithFullAudit<T> where T : class, IFullAuditedEntity, new()
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BaseServiceWithFullAudit(IUnitOfWork unitOfWork, IBaseRepository<T> repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method will return all the records for given enity
        /// foreign key entities will not be mapped
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll() => _repository.GetAllTheRecords();

        /// <summary>
        /// This method will return all the records for given enity
        /// foreign key entities will be mapped
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties) => _repository.GetAllTheRecords(includeProperties);

        /// <summary>
        /// This method will return enity itself for given Id.
        /// foreign key entities will not be mapped
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual T GetById(int Id) => _repository.GetSingle(Id);

        public virtual T GetById(string Id) => _repository.GetSingle(Id);

        /// <summary>
        /// This method will return enity for given condition using lamda prediction.
        /// foreign key entities will be mapped, have to pass includeProperties to make it working
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.GetSingle(predicate, includeProperties);
        }
        public virtual Response<T> Insert(T model)
        {
            try
            {
                model.CreationDateTime = DateTime.UtcNow;
                model.CreationUserId = 1;

                _repository.Add(model);
                _unitOfWork.Commit();
                return new Response<T>(true, model);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                return new Response<T>(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        public virtual Response<T> Update(T model)
        {
            try
            {
                model.LastModificationDateTime = DateTime.UtcNow;
                model.LastModificationUserId = 1;

                _repository.Update(model);
                _unitOfWork.Commit();
                return new Response<T>(true, model);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                return new Response<T>(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        public virtual Response<T> SoftDelete(T model)
        {
            try
            {
                model.DeletionFlag = true;
                model.DeletionUserId = 1;
                model.DeletionDateTime = DateTime.UtcNow;

                _repository.SoftDelete(model);
                _unitOfWork.Commit();
                return new Response<T>(true, model);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                return new Response<T>(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public virtual Response<T> HardDelete(T model)
        {
            try
            {
                _repository.HardDelete(model);
                _unitOfWork.Commit();
                return new Response<T>(true, model);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                return new Response<T>(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }

    public interface IBaseService<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T GetById(int Id);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Response<T> Insert(T model);
        Response<T> Update(T model);
        bool HardDelete(T model);
    }
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<T> repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method will return all the records for given enity
        /// foreign key entities will not be mapped
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll() => _repository.GetAllTheRecords();

        /// <summary>
        /// This method will return all the records for given enity
        /// foreign key entities will be mapped
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties) => _repository.GetAllTheRecords(includeProperties);

        /// <summary>
        /// This method will return enity itself for given Id.
        /// foreign key entities will not be mapped
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual T GetById(int Id) => _repository.GetSingle(Id);

        /// <summary>
        /// This method will return enity for given condition using lamda prediction.
        /// foreign key entities will be mapped, have to pass includeProperties to make it working
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.GetSingle(predicate, includeProperties);
        }
        public virtual Response<T> Insert(T model)
        {
            try
            {
                _repository.Add(model);
                _unitOfWork.Commit();
                return new Response<T>(true, model);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                return new Response<T>(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        public virtual Response<T> Update(T model)
        {
            try
            {
                _repository.Update(model);
                _unitOfWork.Commit();
                return new Response<T>(true, model);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                return new Response<T>(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public virtual bool HardDelete(T model)
        {
            try
            {
                _repository.HardDelete(model);
                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                _unitOfWork.RejectChanges();
                return false;
            }
        }
    }
}
