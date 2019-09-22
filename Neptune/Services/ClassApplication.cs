using System.Net.Http;

namespace Neptune.Services
{
    interface ClassApplication : IService { }
    class ClassApplicationService : MessageService<ClassApplication>
    {
        public ClassApplicationService(HttpClient client) : base(client) { }
    }
}
