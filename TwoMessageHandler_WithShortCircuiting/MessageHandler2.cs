using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TwoMessageHandler_WithShortCircuiting
{
    public class MessageHandler2 : DelegatingHandler
    {
        protected override  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization.Scheme == "Basic")
            {
                var response = new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent("Denied by MesageHandler2"),
                    RequestMessage = new HttpRequestMessage()
                    {
                        Method = new HttpMethod("Post"),

                    }
                };
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            var Response =  base.SendAsync(request, cancellationToken);
            return Response;
        }
    }
}