using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PerRouteMessageHandler
{
    /*for routes matching only api2/ControllerName*/
    public class PerRouteMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var header = request.Headers.Host;
            var response = await base.SendAsync(request, cancellationToken);
            var Response = request.CreateResponse(System.Net.HttpStatusCode.Forbidden, header);
            return Response;
        }
    }
}