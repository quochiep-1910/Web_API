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
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SildeController : ApiControllerBase
    {
        private ISlideService _sildeService;

        //xuất lỗi khi cần và lấy dữ liệu từ database
        public SildeController(IErrorService errorService, ISlideService sildeService) : base(errorService)
        {
            this._sildeService = sildeService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _sildeService.GetAll(keyword);//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                totalRow = model.Count(); //đếm
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var reponseData = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(query); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var paginationSet = new PaginationSet<SlideViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel slideViewModel)
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
                    var newslide = new Slide();
                    newslide.UpdateSlide(slideViewModel);

                    _sildeService.Add(newslide);
                    _sildeService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(newslide);
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
                var model = _sildeService.GetById(id);

                var reponseData = Mapper.Map<Slide, SlideViewModel>(model); //lấy giữ liệu thông qua mapper

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel slideViewModel)
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
                    var dbSlide = _sildeService.GetById(slideViewModel.ID);
                    dbSlide.UpdateSlide(slideViewModel);

                    _sildeService.Update(dbSlide);
                    _sildeService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(dbSlide);
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
                    var oldProduct = _sildeService.Delete(id); //xoá dữ liệu cũ

                    _sildeService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(oldProduct);
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
                    var listSlides = new JavaScriptSerializer().Deserialize<List<int>>(checkedPages);// huỷ dữ liệu số
                    foreach (var item in listSlides)
                    {
                        _sildeService.Delete(item); //xoá dữ liệu cũ
                    }

                    _sildeService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listSlides.Count);
                }

                return response;
            });
        }
    }
}