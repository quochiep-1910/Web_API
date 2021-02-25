using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Common.Exceptions;
using WebAPI.Data.Infrastructure;
using WebAPI.Data.Repositories;
using WebAPI.Model.Models;

namespace WebAPI.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationGroup> GetAll();

        ApplicationGroup Add(ApplicationGroup applicationGroup);

        void Update(ApplicationGroup applicationGroup);

        ApplicationGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        void Save();
    }

    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _applicationGroupRepository;
        private IApplicationUserGroupRepository _applicationUserGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationGroupService(IApplicationGroupRepository applicationGroupRepository, IUnitOfWork unitOfWork,
            IApplicationUserGroupRepository applicationUserGroupRepository)
        {
            this._applicationGroupRepository = applicationGroupRepository;
            this._applicationUserGroupRepository = applicationUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationGroup Add(ApplicationGroup applicationGroup)
        {
            if (_applicationGroupRepository.CheckContains(x => x.Name == applicationGroup.Name))
            {
                throw new NameDuplicatedException("Tên không được trùng");
            }
            return _applicationGroupRepository.Add(applicationGroup);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _applicationUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _applicationUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var appGroup = this._applicationGroupRepository.GetSingleById(id);
            return _applicationGroupRepository.Delete(appGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _applicationGroupRepository.GetAll();
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _applicationGroupRepository.GetAll();
            if (!String.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _applicationGroupRepository.GetSingleById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _applicationGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return _applicationGroupRepository.GetListUserByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup applicationGroup)
        {
            if (_applicationGroupRepository.CheckContains(x => x.Name == applicationGroup.Name && x.ID != applicationGroup.ID))
            {
                throw new NameDuplicatedException("Tên không được trùng");
            }
            _applicationGroupRepository.Update(applicationGroup);
        }
    }
}