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

        public R GetRequest<M, R>(M msg)
            where M : IGetRequest<IS>
            where R : IResponse<IS, M>
        {
            throw new NotImplementedException();
        }

        public R PostRequest<M, R>(M msg)
            where M : IPostRequest<IS>
            where R : IResponse<IS, M>
        {
            throw new NotImplementedException();
        }
    }
}
