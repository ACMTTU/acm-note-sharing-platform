using System;
using System.Net.Http;

namespace Neptune.Services
{
    interface ICatalogApplication : IService
    {
    }
    class CatalogApplicationService : MessageService<ICatalogApplication>
    {
        public CatalogApplicationService(IHttpClientFactory client) : base(client) { }
    }
}
