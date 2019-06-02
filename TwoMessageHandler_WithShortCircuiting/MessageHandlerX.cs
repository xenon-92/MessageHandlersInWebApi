using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TwoMessageHandler_WithShortCircuiting
{
    public class MessageHandlerX : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers != null && request.Headers.Authorization != null && request.Headers.Authorization.Scheme == "Basic")
            {
                return request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                request.Content = new StringContent("this is not a basic Authentication");
                request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
                {
                    MaxAge =  new TimeSpan(999999)
                };
            }
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}