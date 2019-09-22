using System;

namespace Neptune
{
    public interface IRequest<TService>
        where TService : IService
    { }

    public interface IGetRequest<TService> : IRequest<TService>
        where TService : IService
    { }
    public interface IPostRequest<TService> : IRequest<TService>
    where TService : IService
    { }

    public interface IPutRequest<TService> : IRequest<TService>
    where TService : IService
    { }
    public interface IDeleteRequest<TService> : IRequest<TService>
    where TService : IService
    { }

    public interface IResponse<TService, TMessage>
        where TMessage : IRequest<TService>
        where TService : IService
    {
        Object contents();
    }


}
