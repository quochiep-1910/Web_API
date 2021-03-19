using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebAPI.Model.Models;
using WebAPI.Service;
using WebAPI.Web.Models;

namespace WebAPI.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;
        private IProductService _productService;
        private IMenuService _menuService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService,
            IProductService productService, IMenuService menuService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
            _menuService = menuService;
        }

        // GET: Home
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            //slide
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;

            //product
            var lastestProductModel = _productService.GetLastest(20);
            var topSaleProductModel = _productService.GetHotProduct(20);
            var topHotCount = _productService.GetHotCount(3);

            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            var topHotCountProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topHotCount);

            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;
            homeViewModel.HotCountProducts = topHotCountProductViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]//không được gọi trực tiếp
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var menuModel = _menuService.ListByGroupID(1);

            return PartialView(menuModel);
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            //list tag
            ViewBag.ListTag = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetAllListTag(5));

            var model = _productCategoryService.GetAllByParentId();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}