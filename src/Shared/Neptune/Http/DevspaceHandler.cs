using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ACMTTU.NoteSharing.Shared.Neptune.HTTP
{
    class DevspaceHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("azds-route-as"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("You must supply the azure dev space azds-route-as header")
                };
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
