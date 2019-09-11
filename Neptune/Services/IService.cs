namespace Neptune
{
    public interface IService { }
    public interface IMessageService<IS>
        where IS:IService
    {
        R SendMSG<M, R>(M msg)
            where M:IMessage<IS>
            where R:IResponse<M>;
    }
}
