using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TwoMessageHandler_WithShortCircuiting
{
    public class MessageHandler1 : DelegatingHandler
    {
        private static List<string> usernames = new List<string>
        {
            {"IronMan" },
            {"CaptainAmerica" },
            {"Hulk" },
            {"Thor" },
            {"BlackWidow" },
            {"HawkEye" },
            {"ScarletWitch" },
            {"BlackPanther" },
            {"Vision" }
        };
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpRequestHeaders header = request.Headers;
            //var header.Authorization.Parameter = header.Authorization.Parameter;
            if (header.Authorization != null && header.Authorization.Scheme == "Basic")
            {
                Encoding encode = Encoding.GetEncoding(57003);
                byte[] credentialsnByte = Convert.FromBase64String(header.Authorization.Parameter);
                string[] decodedCredentials = encode.GetString(credentialsnByte).Split(':');
                string username = decodedCredentials[0];
                string password = decodedCredentials[1];
                bool isValid = false;                
                foreach (var uName in usernames)
                {
                    if (uName == username)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (isValid)
                {
                    header.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan(12000),
                        NoCache = true,
                    };
                    
                }
            }
            else
            {
                header.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = new TimeSpan(152333),
                    NoCache = false
                };
            }
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}