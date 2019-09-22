using System;
using System.Net.Http;

namespace Neptune.Services
{
    interface CatalogApplication : IService
    {
    }
    class CatalogApplicationService : MessageService<CatalogApplication>
    {
        public CatalogApplicationService(HttpClient client) : base(client) { }
    }
    class CatalogHealth : IGetRequest<CatalogApplication>
    {
    }
    class CatalogHealthResponse : IResponse<CatalogApplication, CatalogHealth>
    {
        public object contents()
        {
            throw new NotImplementedException();
        }
    }
}
