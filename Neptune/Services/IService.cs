using System;
using System.Net.Http;

namespace Neptune
{
    public interface IService { }
    public class MessageService<TService>
        where TService : IService
    {
        private HttpClient client;

        public MessageService(HttpClient client)
        {
            this.client = client;
        }

        public TResponse GetRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : IGetRequest<TService>
            where TResponse: IResponse<TService, TMessage>
        {
            throw new NotImplementedException();
        }

        public TResponse PostRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : IPostRequest<TService>
            where TResponse : IResponse<TService, TMessage>
        {
            throw new NotImplementedException();
        }

        public TResponse PutRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : IPutRequest<TService>
            where TResponse : IResponse<TService, TMessage>
        {
            throw new NotImplementedException();
        }

        public TResponse DeleteRequest<TMessage, TResponse>(TMessage msg)
            where TMessage : IDeleteRequest<TService>
            where TResponse : IResponse<TService, TMessage>
        {
            throw new NotImplementedException();
        }
    }
}
