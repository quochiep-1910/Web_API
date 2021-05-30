using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data.Infrastructure;
using WebAPI.Data.Repositories;
using WebAPI.Model.Models;

namespace WebAPI.Service
{
    public interface IMenuGroupService
    {
        MenuGroup GetByUrl(string Url);

        MenuGroup Add(MenuGroup menuGroup);

        void Update(MenuGroup menuGroup);

        MenuGroup Delete(int id);

        IEnumerable<MenuGroup> GetAll();

        IEnumerable<MenuGroup> GetAll(string keyword);

        MenuGroup GetById(int id);

        void Save();
    }

    public class MenuGroupService : IMenuGroupService
    {
        private IMenuGroupRepository _menuGroupRepository;

        private IUnitOfWork _unitOfWork;

        public MenuGroupService(IMenuGroupRepository menuGroupRepository, IUnitOfWork unitOfWork)
        {
            this._menuGroupRepository = menuGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public MenuGroup Add(MenuGroup menuGroup)
        {
            return _menuGroupRepository.Add(menuGroup);
        }

        public MenuGroup Delete(int id)
        {
            return _menuGroupRepository.Delete(id);
        }

        public IEnumerable<MenuGroup> GetAll()
        {
            return _menuGroupRepository.GetAll();
        }

        public IEnumerable<MenuGroup> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))//so sánh khác rỗng (nên dùng cái này thay cho [keyword !=null]
                return _menuGroupRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _menuGroupRepository.GetAll();
        }

        public MenuGroup GetById(int id)
        {
            return _menuGroupRepository.GetSingleById(id);
        }

        public MenuGroup GetByUrl(string Url)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MenuGroup menuGroup)
        {
            _menuGroupRepository.Update(menuGroup);
        }
    }
}