using System;
using System.Net.Http;

namespace ACMTTU.NoteSharing.Shared.Neptune.Services
{
    interface ICatalogApplication : IService
    {
    }
    class CatalogApplicationService : MessageService<ICatalogApplication>
    {
        public CatalogApplicationService(IHttpClientFactory clientFactory) : base(clientFactory) { }
    }
}
