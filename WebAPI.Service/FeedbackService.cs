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
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        IEnumerable<Feedback> GetAll();

        IEnumerable<Feedback> GetAll(string keyword);

        void Update(Feedback feedback);

        Feedback Delete(int id);

        Feedback GetById(int id);

        void Save();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _FeedbackRepository;

        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository FeedbackRepository, IUnitOfWork unitOfWork)
        {
            this._FeedbackRepository = FeedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return _FeedbackRepository.Add(feedback);
        }

        public Feedback Delete(int id)
        {
            return _FeedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _FeedbackRepository.GetAll();
        }

        public IEnumerable<Feedback> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))//so sánh khác rỗng (nên dùng cái này thay cho [keyword !=null]
                return _FeedbackRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _FeedbackRepository.GetAll();
        }

        public Feedback GetById(int id)
        {
            return _FeedbackRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _FeedbackRepository.Update(feedback);
        }
    }
}