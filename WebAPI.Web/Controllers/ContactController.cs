using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Model.Models;
using WebAPI.Service;
using WebAPI.Web.Models;

namespace WebAPI.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _ContactDetailService;

        public ContactController(IContactDetailService ContactDetailService)
        {
            _ContactDetailService = ContactDetailService;
        }

        // GET: ContactDetail
        public ActionResult Index()
        {
            var model = _ContactDetailService.GetDefaultContact();
            var contact = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(contact);
        }
    }
}