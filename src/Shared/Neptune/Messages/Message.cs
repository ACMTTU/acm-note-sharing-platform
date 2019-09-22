using System;
using System.Net.Http;

namespace Neptune
{
    public abstract class Request<TService>
        where TService : IService
    {
        public abstract Uri Uri { get; }
    }
    public abstract class GetRequest<TService> : Request<TService>
        where TService : IService
    { }
    public abstract class PostRequest<TService> : Request<TService>
    where TService : IService
    {
        public HttpContent content;
    }
    public abstract class PutRequest<TService> : Request<TService>
    where TService : IService
    {
        public HttpContent content;
    }
    public abstract class DeleteRequest<TService> : Request<TService>
    where TService : IService
    { }
    public abstract class Response<TService, TMessage>
        where TMessage : Request<TService>
        where TService : IService
    {
        public abstract Object contents();
    }
}