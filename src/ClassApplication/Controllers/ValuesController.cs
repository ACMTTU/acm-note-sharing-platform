using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.ClassApplication.Services;

namespace ACMTTU.NoteSharing.Platform.ClassApplication.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : PlatformBaseController
    {

        private Container classesContainer;
        public ClassController(IHttpClientFactory factory, DatabaseService databaseService) : base(factory)
        {
            this.classesContainer = databaseService.classroomsContainer;
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetValues(string id)
        {
            await this.classesContainer.CreateItemAsync<dynamic>(new Object() { });
            return ("Created an object");
        }
    }
}