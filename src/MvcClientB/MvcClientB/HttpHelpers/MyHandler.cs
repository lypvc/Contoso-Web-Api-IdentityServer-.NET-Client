using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MvcClientB.HttpHelpers
{
    public class MyHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> values = new List<string>() { "1,20"};

            request.Headers.Add("Pagination", values);      
            return base.SendAsync(request, cancellationToken);
        }
    }
}
