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
    public interface IFooterService
    {
        Footer Add(Footer Footer);

        void Update(Footer Footer);

        Footer Delete(string id);

        IEnumerable<Footer> GetAll();

        IEnumerable<Footer> GetAll(string keyword);

        Footer GetSingleByString(string id);

        void Save();
    }

    public class FooterService : IFooterService
    {
        private IFooterRepository _footerRepository;

        private IUnitOfWork _unitOfWork;

        public FooterService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer Add(Footer Footer)
        {
            return _footerRepository.Add(Footer);
        }

        public Footer Delete(string id)
        {
            return _footerRepository.Delete(id);
        }

        public Footer GetSingleByString(string id)
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public IEnumerable<Footer> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))//so sánh khác rỗng (nên dùng cái này thay cho [keyword !=null]
                return _footerRepository.GetMulti(x => x.ID.Contains(keyword));
            else
                return _footerRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer Footer)
        {
            _footerRepository.Update(Footer);
        }
    }
}