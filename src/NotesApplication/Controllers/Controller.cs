using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.NotesApplication.Services;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : PlatformBaseController
    {
        private Container NotesContainer;
        public NotesController(IHttpClientFactory factory, DBService db) : base(factory)
        {
            NotesContainer = db.newContainer;
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>


        // [HttpGet("{id}")]
        // public async Task<ActionResult<string>> GetValues(string id)
        // {
        //     using (CosmosClient dbClient = await this.GetDatatbaseClient())
        //     {
        //         await dbClient.CreateDatabaseIfNotExistsAsync("NotesApplication");
        //     }

        //     return $"Id value from URL: {id}";
        // }




    }
}