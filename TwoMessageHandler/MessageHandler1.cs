using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TwoMessageHandler
{
    public class MessageHandler1 : DelegatingHandler
    {
        private static List<string> userNames = new List<string>
        {
            { "tony"},
            { "rogers"},
            { "hulk"},
            { "strange"},
            { "scarlet witch"},
            { "spiderman"},
            {"thanos" }         
        };
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var headers = request.Headers;
            var body = request.Content;
            if (headers.Authorization.Parameter != null && headers.Authorization.Scheme == "Basic")
            {
                Encoding encode = Encoding.GetEncoding(57003);
                byte[] credentialsInBits = Convert.FromBase64String(headers.Authorization.Parameter);
                string[] crdentailsDecoded = encode.GetString(credentialsInBits).Split(':');
                string username = crdentailsDecoded[0];
                string Password = crdentailsDecoded[1];
                bool isValid = false;
                for (int i = 0; i < userNames.Count; i++)
                {
                    if (userNames[i] == username)
                    {
                        isValid = true;
                        break;
                    }
                }               
                if (isValid)
                {
                    //do something
                    headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        
                        MaxAge = new TimeSpan(12000)
                    };
                }
            }
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}