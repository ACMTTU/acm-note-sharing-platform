using Neptune.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neptune.Messages.Catalog
{
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
