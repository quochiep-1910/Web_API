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
    [RoutePrefix("api/menugroup")]
    public class MenuGroupController : ApiControllerBase
    {
        private IMenuGroupService _menuGroupService;

        //xuất lỗi khi cần và lấy dữ liệu từ database
        public MenuGroupController(IErrorService errorService, IMenuGroupService menuGroupService) : base(errorService)
        {
            this._menuGroupService = menuGroupService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _menuGroupService.GetAll(keyword);//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                totalRow = model.Count(); //đếm
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var reponseData = Mapper.Map<IEnumerable<MenuGroup>, IEnumerable<MenuGroupViewModel>>(query); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var paginationSet = new PaginationSet<MenuGroupViewModel>()
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

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _menuGroupService.GetAll();//lấy về toàn bộ số bản ghi và từ khoá tìm kiếm

                var reponseData = Mapper.Map<IEnumerable<MenuGroup>, IEnumerable<MenuGroupViewModel>>(model); //lấy giữ liệu thông qua mapper và duyệt từng phần từ

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, MenuGroupViewModel menuGroupViewModel)
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
                    var newMenu = new MenuGroup();
                    newMenu.UpdateMenuGroups(menuGroupViewModel);

                    _menuGroupService.Add(newMenu);
                    _menuGroupService.Save();

                    var responseData = Mapper.Map<MenuGroup, MenuGroupViewModel>(newMenu);
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
                var model = _menuGroupService.GetById(id);

                var reponseData = Mapper.Map<MenuGroup, MenuGroupViewModel>(model); //lấy giữ liệu thông qua mapper

                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, MenuGroupViewModel menuGroupViewModel)
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
                    var dbMenuGroups = _menuGroupService.GetById(menuGroupViewModel.ID);
                    dbMenuGroups.UpdateMenuGroups(menuGroupViewModel);

                    _menuGroupService.Update(dbMenuGroups);
                    _menuGroupService.Save();

                    var responseData = Mapper.Map<MenuGroup, MenuGroupViewModel>(dbMenuGroups);
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
                    var oldProduct = _menuGroupService.Delete(id); //xoá dữ liệu cũ

                    _menuGroupService.Save();

                    var responseData = Mapper.Map<MenuGroup, MenuGroupViewModel>(oldProduct);
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
                        _menuGroupService.Delete(item); //xoá dữ liệu cũ
                    }

                    _menuGroupService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listPages.Count);
                }

                return response;
            });
        }
    }
}