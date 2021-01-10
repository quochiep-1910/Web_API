using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Model.Models;
using WebAPI.Service;
using WebAPI.Web.Infrastructure.Core;
using WebAPI.Web.Infrastructure.Extensions;
using WebAPI.Web.Models;

namespace WebAPI.Web.API
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        //xuất lỗi khi cần và lấy dữ liệu từ database
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                var reponseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
             {
                 int totalRow = 0;
                 var model = _productCategoryService.GetAll(keyword);//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                 totalRow = model.Count(); //đếm
                 var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                 var reponseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                 var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                 {
                     Items = reponseData,
                     Page = page,
                     TotalCount = totalRow,
                     TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                 };
                 var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                 return response;
             });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 if (!ModelState.IsValid)
                 {
                     //trả về lỗi để bên ngoài bắt được sự kiện lỗi này
                     response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                 }
                 else
                 {
                     var newProductCategory = new ProductCategory();
                     newProductCategory.UpdateProductCategory(productCategoryViewModel);

                     _productCategoryService.Add(newProductCategory);
                     _productCategoryService.Save();

                     var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                     response = request.CreateResponse(HttpStatusCode.Created, responseData);
                 }

                 return response;
             });
        }
    }
}