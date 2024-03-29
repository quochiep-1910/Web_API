﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebAPI.Model.Models;
using WebAPI.Service;
using WebAPI.Web.Infrastructure.Core;
using WebAPI.Web.Infrastructure.Extensions;
using WebAPI.Web.Models;

namespace WebAPI.Web.API
{
    [RoutePrefix("api/productcategory")]
    //[Authorize]
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
                     newProductCategory.CreatedDate = DateTime.Now;
                     newProductCategory.CreatedBy = User.Identity.Name;

                     _productCategoryService.Add(newProductCategory);
                     _productCategoryService.Save();

                     var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                     response = request.CreateResponse(HttpStatusCode.Created, responseData);
                 }

                 return response;
             });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);

                var reponseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model); //lấy giữ liệu thông qua mapper

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
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
                    var dbProductCategory = _productCategoryService.GetById(productCategoryViewModel.ID);
                    dbProductCategory.UpdateProductCategory(productCategoryViewModel);
                    dbProductCategory.UpdatedDate = DateTime.Now;
                    dbProductCategory.CreatedBy = User.Identity.Name;
                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.Save();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var oldProductCategory = _productCategoryService.Delete(id); //xoá dữ liệu cũ

                    _productCategoryService.Save();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletenulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProductCategories)
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
                    var listProductCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategories);// huỷ dữ liệu số
                    foreach (var item in listProductCategory)
                    {
                        _productCategoryService.Delete(item); //xoá dữ liệu cũ
                    }

                    _productCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }

                return response;
            });
        }

        [HttpPut]
        [Route("changestatus")]
        public HttpResponseMessage ChangeStatus(HttpRequestMessage request, int id) //id trong Models.EF.User truyền kiểu gì thì truyền y như vậy
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = _productCategoryService.ChangeStatus(id);
                response = request.CreateResponse(HttpStatusCode.OK, result);
                _productCategoryService.Save();
                return response;
            });
        }
    }
}