using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Common;
using WebAPI.Model.Models;
using WebAPI.Service;
using WebAPI.Web.Infrastructure.Core;
using WebAPI.Web.Models;

namespace WebAPI.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }

        // GET: Product
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;

            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)(Math.Ceiling((double)totalRow / pageSize)); //chuyển đổi sang int rồi làm tròn lên

            //var category = _productCategoryService.GetById(id);
            //ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var pagiationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Maxpage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,

                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(pagiationSet);
        }

        /// <summary>
        /// lấy ra danh sách sản phẩm để tìm kiếm
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);

            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;

            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)(Math.Ceiling((double)totalRow / pageSize)); //chuyển đổi sang int rồi làm tròn lên

            ViewBag.Keyword = keyword;

            var pagiationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Maxpage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,

                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(pagiationSet);
        }
    }
}