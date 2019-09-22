using Neptune.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neptune.Messages.Catalog
{
    class CatalogHealth : GetRequest<ICatalogApplication>
    {
        public override Uri Uri => throw new NotImplementedException();
    }
    class CatalogHealthResponse : Response<ICatalogApplication, CatalogHealth>
    {
        public override object contents()
        {
            throw new NotImplementedException();
        }
    }
}
