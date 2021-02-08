using System;
using System.Collections.Generic;
using WebAPI.Data.Infrastructure;
using WebAPI.Data.Repositories;
using WebAPI.Model.Models;

namespace WebAPI.Service
{
    public interface IContactDetailService
    {
        ContactDetail Add(ContactDetail contactDetail);

        void Update(ContactDetail contactDetail);

        ContactDetail Delete(int id);

        IEnumerable<ContactDetail> GetAll();

        IEnumerable<ContactDetail> GetAll(string keyword);

        ContactDetail GetById(int id);

        ContactDetail GetDefaultContact();

        void Save();
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _ContactDetailRepository;

        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository ContactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._ContactDetailRepository = ContactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail Add(ContactDetail contactDetail)
        {
            throw new NotImplementedException();
        }

        public ContactDetail Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContactDetail> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public ContactDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ContactDetail GetDefaultContact()
        {
            return _ContactDetailRepository.GetSingleByCondition(x => x.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail contactDetail)
        {
            _ContactDetailRepository.Update(contactDetail);
        }
    }
}