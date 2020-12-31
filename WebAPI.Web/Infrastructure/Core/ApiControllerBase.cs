using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Model.Models;
using WebAPI.Service;

namespace WebAPI.Web.Infrastructure.Core

{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke(); //Invoke: Cho phép sử dụng thread(luồng) từ main-thread để xử lý
                                              //tránh trường hợp bị crash or unsafe bất cứ lúc nào khi các thread tranh chấp tài nguyên
                                              //Còn gọi là uỷ quyền *Delegate
            }
            catch (DbEntityValidationException ex)//Ngoại lệ xác thực thực thể
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Thực thể của loại \"{eve.Entry.Entity.GetType().Name}\" ở trạng thái \"{eve.Entry.State}\" có lỗi xác thực sau.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Thuộc tính: \"{ve.PropertyName}\", Lỗi: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                //ghi lỗi vào db
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Ghi lỗi vào database
        /// </summary>
        /// <param name="ex"></param>
        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreateDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }
    }
}