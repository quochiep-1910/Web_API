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

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
             {
                 var model = _productCategoryService.GetAll();
                 var reponseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model); //lấy giữ liệu thông qua mapper và duyệt từng phần từ
                 var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                 return response;
             });
        }
    }
}