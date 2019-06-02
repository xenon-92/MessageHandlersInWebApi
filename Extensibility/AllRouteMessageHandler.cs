using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Extensibility
{
    public class AllRouteMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Request processed");
            var response =  await base.SendAsync(request, cancellationToken);
            Debug.WriteLine(" response processed");
            return response;
        }
    }
}