using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TwoMessageHandler.Models;

namespace TwoMessageHandler
{
    public class MessageHandler2 : DelegatingHandler
    {
        protected override async  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var header = request.Headers;
            var times = header.CacheControl.MaxAge;
            header.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                MaxAge = new TimeSpan(5000000)
            };
            var response = await base.SendAsync(request, cancellationToken);
            bool isSuccess = response.IsSuccessStatusCode;
            if (isSuccess)
            {
                Employee emp = new Employee
                {
                    Eid = 8,
                    EName = 8.ToString() + "Tudu",
                    EDepartment = 8.ToString() + "Railways"
                };
                HttpResponseMessage httpResponse = request.CreateResponse(System.Net.HttpStatusCode.Conflict,emp);
                return httpResponse;
            }
            return response;
        }
    }
}