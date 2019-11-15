
using ACMTTU.NoteSharing.Shared.SDK.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Platform.ClassApplication.Services
{

    public class DatabaseService : PlatformBaseService
    {

        public Container classroomsContainer { get; private set; }

        public DatabaseService(IHttpClientFactory clientFactory) : base(clientFactory)
        {

        }

        public async override Task Setup()
        {

            DatabaseResponse databaseResponse = await this.dbClient.CreateDatabaseIfNotExistsAsync("Classroom");
            ContainerResponse containerResponse = await databaseResponse.Database.CreateContainerIfNotExistsAsync("Classrooms", "/Name");

            this.classroomsContainer = containerResponse.Container;
        }

    }

}

