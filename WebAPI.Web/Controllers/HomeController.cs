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
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;
        private IProductService _productService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService,
            IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
        }

        // GET: Home
        public ActionResult Index()
        {
            //slide
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;

            //product
            var lastestProductModel = _productService.GetLastest(3);
            var topSaleProductModel = _productService.GetHotProduct(2);

            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]//không được gọi trực tiếp
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}