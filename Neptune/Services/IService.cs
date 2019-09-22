using System;
using System.Net.Http;

namespace Neptune
{
    public interface IService { }
    public abstract class MessageService<TService>
        where TService : IService
    {
        private readonly HttpClient client;
        public MessageService(HttpClient client)
        {
            this.client = client;
        }
        public TResponse GetRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : GetRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            throw new NotImplementedException();
        }
        public TResponse PostRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : PostRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            throw new NotImplementedException();
        }
        public TResponse PutRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : PutRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            throw new NotImplementedException();
        }
        public TResponse DeleteRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : DeleteRequest<TService>
            where TResponse : Response<TService, TMessage>
        {
            throw new NotImplementedException();
        }
    }
}