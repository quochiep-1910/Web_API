using AutoMapper;
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
    [RoutePrefix("api/footer")]
    public class FooterController : ApiControllerBase
    {
        private IFooterService _footerService;

        public FooterController(IErrorService errorService, IFooterService footerService) : base(errorService)
        {
            this._footerService = footerService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _footerService.GetAll(keyword);//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                totalRow = model.Count(); //đếm
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var reponseData = Mapper.Map<IEnumerable<Footer>, IEnumerable<FooterViewModel>>(query); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var paginationSet = new PaginationSet<FooterViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, FooterViewModel footerViewModel)
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
                    var newpages = new Footer();
                    newpages.UpdateFooter(footerViewModel);

                    _footerService.Add(newpages);
                    _footerService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(newpages);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                Footer model = _footerService.GetSingleByString(id);

                var reponseData = Mapper.Map<Footer, FooterViewModel>(model); //lấy giữ liệu thông qua mapper

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, FooterViewModel footerViewModel)
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
                    Footer dbPages = _footerService.GetSingleByString(footerViewModel.ID);
                    dbPages.UpdateFooter(footerViewModel);

                    _footerService.Update(dbPages);
                    _footerService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(dbPages);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
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
                    var oldProduct = _footerService.Delete(id); //xoá dữ liệu cũ

                    _footerService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletenulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPages)
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
                    var listFooter = new JavaScriptSerializer().Deserialize<List<string>>(checkedPages);// huỷ dữ liệu số
                    foreach (var item in listFooter)
                    {
                        _footerService.Delete(item.ToString()); //xoá dữ liệu cũ
                    }

                    _footerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listFooter.Count);
                }

                return response;
            });
        }
    }
}