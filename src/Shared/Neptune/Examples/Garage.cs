using ACMTTU.NoteSharing.Shared.Neptune.Messages;
using ACMTTU.NoteSharing.Shared.Neptune.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACMTTU.NoteSharing.Shared.Neptune.Services
{
    interface Inventory : IService { }
    class InventoryService : MessageService<Inventory>
    {
        public InventoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }
    }
    class GetCountMessage : GetRequest<Inventory>
    {
        private Uri uri;
        public GetCountMessage(string itemName)
        {
            this.uri = new Uri($"http://do.notclick.me/inventory/{itemName}/count");
        }
        public override Uri Uri { get => uri; set => this.uri = value; }
    }
    class GetCountResponse : Response<Inventory, GetCountMessage>
    {
        public int count;
        public string itemName;
        public int itemId;
    }
    class Example
    {
        public async Task<int> count(InventoryService service)
        {
            var message = new GetCountMessage("screwdriver");
            var response = await service.GetRequest<GetCountMessage, GetCountResponse>(message);
            return response.count;
        }
    }
}
