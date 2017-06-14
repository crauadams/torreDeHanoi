using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace HanoiAPI.Model
{
    public static class ApiResponse
    {
        public static HttpResponseMessage Success(this HttpRequestMessage source)
        {
            return source.CreateResponse(new KeyValuePair<string, string>("status", "success"));
        }

        public static HttpResponseMessage Success<T>(this HttpRequestMessage source, T responseContent) where T : class
        {
            return source.CreateResponse<T>(HttpStatusCode.OK, responseContent);
        }

        public static HttpResponseMessage NotFound(this HttpRequestMessage source)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}