using ACMTTU.NoteSharing.Shared.Neptune.Services;
using System;

namespace ACMTTU.NoteSharing.Shared.Neptune.Messages
{
    class CatalogHealth : GetRequest<ICatalogApplication>
    {
        public override Uri Uri => throw new NotImplementedException();
    }
    class CatalogHealthResponse : Response<ICatalogApplication, CatalogHealth> { }
}
