using Neptune.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neptune.Messages.Catalog
{
    class CatalogHealth : IGetRequest<ICatalogApplication>
    {
    }
    class CatalogHealthResponse : IResponse<ICatalogApplication, CatalogHealth>
    {
        public object contents()
        {
            throw new NotImplementedException();
        }
    }
}
