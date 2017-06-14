using HanoiAPI.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HanoiAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage ProcessResponse(Action action)
        {
            try
            {
                action.Invoke();
                return Request.Success();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex, true));
            }
        }

        public HttpResponseMessage ProcessResponse<TResult>(Func<TResult> action) where TResult : class
        {
            try
            {
                return Request.Success(action());
            }
            catch (KeyNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex, true));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.InnerException, true));

                return Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex, true));
            }
        }
    }
}