using System.Collections.Generic;
using WebAPI.Data.Infrastructure;
using WebAPI.Data.Repositories;
using WebAPI.Model.Models;

namespace WebAPI.Service
{
    public interface ITagService
    {
        Tag Add(Tag tag);

        void Update(Tag tag);

        Tag Delete(int id);

        IEnumerable<Tag> GetAll();

        IEnumerable<Tag> GetAll(string keyword);

        Tag GetById(int id);

        void Save();
    }

    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;

        private IUnitOfWork _unitOfWork;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Tag Add(Tag tag)
        {
            return _tagRepository.Add(tag);
        }

        public Tag Delete(int id)
        {
            return _tagRepository.Delete(id);
        }

        public IEnumerable<Tag> GetAll()
        {
            var model = _tagRepository.GetAll();
            return model;
        }

        public IEnumerable<Tag> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))//so sánh khác rỗng (nên dùng cái này thay cho [keyword !=null]
                return _tagRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _tagRepository.GetAll();
        }

        public Tag GetById(int id)
        {
            return _tagRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }
    }
}