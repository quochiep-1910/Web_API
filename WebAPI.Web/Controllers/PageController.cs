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
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        // GET: Page
        public ActionResult Index(string alias)
        {
            //get alias
            var page = _pageService.GetByAlias(alias);
            var model = Mapper.Map<Page, PageViewModel>(page);

            return View(model);
        }
    }
}