using System;
using System.Net.Http;

namespace Neptune.Services
{
    interface ClassApplication : IService { }
    class ClassApplicationService : MessageService<ClassApplication>
    {
        public ClassApplicationService(IHttpClientFactory client) : base(client) { }
    }
}
