using System;
using System.Net.Http;

namespace Neptune
{
    public interface IService { }
    public class MessageService<IS>
        where IS:IService
    {
        private HttpClient client;

        public MessageService(HttpClient client)
        {
            this.client = client;
        }

        R GetRequest<M, R>(M msg)
            where M : IGetRequest<IS>
            where R : IResponse<M>
        {
            throw new NotImplementedException();
        }

        R PostRequest<M, R>(M msg)
            where M : IPostRequest<IS>
            where R : IResponse<M>
        {
            throw new NotImplementedException();
        }
    }
}
