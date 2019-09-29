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
    class NewItemRequest : PostRequest<Inventory>
    {
        public string itemName;
        private Uri uri = new Uri("http://do.notclick.me/inventory");
        public NewItemRequest(string itemName)
        {
            this.itemName = itemName;
        }
        public override Uri Uri { get => uri; set => this.uri = value; }
    }
    class NewItemResponse : Response<Inventory, NewItemRequest>
    {
        public int count;
        public string itemName;
        public int itemId;
    }
    class Example
    {
        public async Task<int> count(InventoryService service)
        {
            var message = new NewItemRequest("screwdriver");
            var response = await service.PostRequest<NewItemRequest, NewItemResponse>(message);
            return response.count;
        }
    }
}
