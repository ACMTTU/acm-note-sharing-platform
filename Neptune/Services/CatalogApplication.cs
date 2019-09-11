using System;
using System.Collections.Generic;
using System.Text;

namespace Neptune.Services
{
    interface CatalogApplication : IService
    {
    }
    class CatalogApplicationService : IMessageService<CatalogApplication>
    {
        public R SendMessage<M, R>(M msg)
            where M : IMessage<CatalogApplication>
            where R : IResponse<M>
        {
            throw new NotImplementedException();
        }
    }
    class CatalogHealth : IMessage<CatalogApplication> { }
    class CatalogHealthResponse : IResponse<CatalogHealth> { }
}
