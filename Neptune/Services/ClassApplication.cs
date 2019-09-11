namespace Neptune.Services
{
    interface ClassApplication : IService { }
    class ClassApplicationService : IMessageService<ClassApplication>
    {
        public R SendMessage<M, R>(M msg)
            where M : IMessage<ClassApplication>
            where R : IResponse<M>
        {
            throw new System.NotImplementedException();
        }
    }

}
