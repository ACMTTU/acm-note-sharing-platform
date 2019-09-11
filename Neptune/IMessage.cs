using System;

namespace Neptune
{

    public interface IRequest<IS>
        where IS : IService
    { }

    public interface IGetRequest<IS> : IRequest<IS>
        where IS : IService
    { }
    public interface IPostRequest<IS> : IRequest<IS>
    where IS : IService
    { }
    public interface IResponse<IMessage>
    {
        Object contents();
    }


}
