using AutoMapper;
using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private IOrderService _orderService;

        //xuất lỗi khi cần và lấy dữ liệu từ database
        public OrderController(IErrorService errorService, IOrderService orderService) : base(errorService)
        {
            this._orderService = orderService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll(keyword);//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                totalRow = model.Count(); //đếm
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var reponseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var paginationSet = new PaginationSet<OrderViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, OrderViewModel orderViewModel)
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
                    var neworder = new Order();
                    neworder.UpdateOrder(orderViewModel);
                    neworder.CreatedDate = DateTime.Now;

                    neworder.CustomerId = User.Identity.GetUserId();
                    neworder.CreatedBy = User.Identity.GetUserName();

                    _orderService.Add(neworder);
                    _orderService.Save();

                    var responseData = Mapper.Map<Order, OrderViewModel>(neworder);
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
                var model = _orderService.GetById(id);

                var reponseData = Mapper.Map<Order, OrderViewModel>(model); //lấy giữ liệu thông qua mapper

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, OrderViewModel orderViewModel)
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
                    var dbOrder = _orderService.GetById(orderViewModel.ID);
                    dbOrder.UpdateOrder(orderViewModel);
                    dbOrder.CreatedDate = DateTime.Now;

                    _orderService.Update(dbOrder);
                    _orderService.Save();

                    var responseData = Mapper.Map<Order, OrderViewModel>(dbOrder);
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
                    var oldProduct = _orderService.Delete(id); //xoá dữ liệu cũ

                    _orderService.Save();

                    var responseData = Mapper.Map<Order, OrderViewModel>(oldProduct);
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
                        _orderService.Delete(item); //xoá dữ liệu cũ
                    }

                    _orderService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listPages.Count);
                }

                return response;
            });
        }
    }
}