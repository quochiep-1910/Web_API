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
    public interface IMenuService
    {
        Menu GetByUrl(string Url);

        Menu Add(Menu menu);

        void Update(Menu menu);

        Menu Delete(int id);

        IEnumerable<Menu> GetAll();

        IEnumerable<Menu> GetAll(string keyword);

        Menu GetById(int id);

        List<Menu> ListByGroupID(int group);

        void Save();
    }

    public class MenuService : IMenuService
    {
        private IMenuRepository _menuRepository;

        private IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            this._menuRepository = menuRepository;
            this._unitOfWork = unitOfWork;
        }

        public Menu Add(Menu menu)
        {
            return _menuRepository.Add(menu);
        }

        public Menu Delete(int id)
        {
            return _menuRepository.Delete(id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public IEnumerable<Menu> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))//so sánh khác rỗng (nên dùng cái này thay cho [keyword !=null]
                return _menuRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _menuRepository.GetAll();
        }

        public Menu GetByUrl(string Url)
        {
            return _menuRepository.GetSingleByCondition(x => x.URL == Url);
        }

        public Menu GetById(int id)
        {
            return _menuRepository.GetSingleById(id);
        }

        public List<Menu> ListByGroupID(int group)
        {
            return _menuRepository.ListByGroupID(group);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Menu menu)
        {
            _menuRepository.Update(menu);
        }
    }
}