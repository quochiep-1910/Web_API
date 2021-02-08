using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
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

        public ProductController(IProductService productService, IProductCategoryService productCategoryService
            )
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }

        // GET: Product
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var viewModel = Mapper.Map<Product, ProductViewModel>(productModel);

            var relatedProduct = _productService.GetRelatedProducts(id, 5);
            ViewBag.RelatedProduct = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

            //list moreImage
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(viewModel.MoreImages);
            ViewBag.MoreImages = listImages;

            //list tags(mapper)
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(id));

            //viewcount
            _productService.IncreaseView(id);

            return View(viewModel);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult ListByTag(string TagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;

            var productModel = _productService.GetListProductByTag(TagId, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)(Math.Ceiling((double)totalRow / pageSize)); //chuyển đổi sang int rồi làm tròn lên

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(TagId));

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