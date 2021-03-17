using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Model.Models;
using WebAPI.Service;
using System.Web.Script.Serialization;
using WebAPI.Web.Infrastructure.Core;
using WebAPI.Web.Infrastructure.Extensions;
using WebAPI.Web.Models;

namespace WebAPI.Web.API
{
    [RoutePrefix("api/feedback")]
    public class FeedbackController : ApiControllerBase
    {
        private IFeedbackService _feedbackService;

        public FeedbackController(IErrorService errorService, IFeedbackService feedbackService) : base(errorService)
        {
            this._feedbackService = feedbackService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _feedbackService.GetAll(keyword);//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                totalRow = model.Count(); //đếm
                var query = model.OrderByDescending(x => x.CreateDate).Skip(page * pageSize).Take(pageSize);
                var reponseData = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(query); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var paginationSet = new PaginationSet<FeedbackViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, FeedbackViewModel feedbackViewModel)
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
                    var newfeedbacks = new Feedback();
                    newfeedbacks.UpdateFeedback(feedbackViewModel);
                    newfeedbacks.CreateDate = DateTime.Now;

                    _feedbackService.Create(newfeedbacks);
                    _feedbackService.Save();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(newfeedbacks);
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
                var model = _feedbackService.GetById(id);

                var reponseData = Mapper.Map<Feedback, FeedbackViewModel>(model); //lấy giữ liệu thông qua mapper

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, FeedbackViewModel feedbackViewModel)
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
                    var dbfeedbacks = _feedbackService.GetById(feedbackViewModel.ID);
                    dbfeedbacks.UpdateFeedback(feedbackViewModel);
                    dbfeedbacks.CreateDate = DateTime.Now;

                    _feedbackService.Update(dbfeedbacks);
                    _feedbackService.Save();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(dbfeedbacks);
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
                    var oldProduct = _feedbackService.Delete(id); //xoá dữ liệu cũ

                    _feedbackService.Save();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(oldProduct);
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
                    var listPages = new JavaScriptSerializer().Deserialize<List<int>>(checkedPages);// huỷ dữ liệu số
                    foreach (var item in listPages)
                    {
                        _feedbackService.Delete(item); //xoá dữ liệu cũ
                    }

                    _feedbackService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listPages.Count);
                }

                return response;
            });
        }
    }
}