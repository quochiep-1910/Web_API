using AutoMapper;
using System.Collections.Generic;
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
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategoryService.GetAll();
                var listPostCategoryViewModel = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryViewModel);

                return response;
            });
        }

        /// <summary>
        /// Tạo ra đối tượng postCategory
        /// </summary>
        /// <param name="request"></param>
        /// <param name="postCategory"></param>
        /// <returns></returns>
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 if (ModelState.IsValid)
                 {
                     //xuất ra lỗi
                     request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }
                 else //nếu thành công thì thêm đối tượng
                 {
                     PostCategory newPostCategory = new PostCategory(); //tạo 1 đối tượng PostCategory
                     newPostCategory.UpdatePostCategory(postCategoryViewModel); //copy tất cả giá trị của postCategoryViewModel sang newPostCategory

                     var category = _postCategoryService.Add(newPostCategory);
                     _postCategoryService.Save();

                     response = request.CreateResponse(HttpStatusCode.Created, category);
                 }
                 return response;
             });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    //xuất ra lỗi
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else //nếu thành công thì thêm đối tượng
                {
                    var postCategoryDb = _postCategoryService.GetById(postCategoryViewModel.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryViewModel);

                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    //xuất ra lỗi
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else //nếu thành công thì thêm đối tượng
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}