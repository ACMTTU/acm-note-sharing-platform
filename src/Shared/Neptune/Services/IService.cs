using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Neptune
{
    public interface IService { }
    public abstract class MessageService<TService>
        where TService : IService
    {
        private readonly HttpClient client;
        public MessageService(IHttpClientFactory clientFactory)
        {
            this.client = clientFactory.CreateClient();
        }
        public async Task<TResponse> GetRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : GetRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            var response = await client.GetAsync(msg.Uri.ToString());
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            TResponse r = JsonConvert.DeserializeObject<TResponse>(result);
            return r;
        }
        public async Task<TResponse> PostRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : PostRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            var response = await client.PostAsync(msg.Uri.ToString(), msg.content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            TResponse r = JsonConvert.DeserializeObject<TResponse>(result);
            return r;
        }
        public async Task<TResponse> PutRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : PutRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            var response = await client.PutAsync(msg.Uri.ToString(), msg.content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            TResponse r = JsonConvert.DeserializeObject<TResponse>(result);
            return r;
        }
        public async Task<TResponse> DeleteRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : DeleteRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            var response = await client.DeleteAsync(msg.Uri.ToString());
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            TResponse r = JsonConvert.DeserializeObject<TResponse>(result);
            return r;
        }
    }
}