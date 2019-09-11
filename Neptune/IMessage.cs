using System;

namespace Neptune
{

    public interface IMessage<IS>
        where IS:IService
    {
    }
    public interface IResponse<IMessage>
    {

    }

}
