using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Platform.NotesApplication.Services;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Controllers {
    [Route ("api/notes")]
    [ApiController]
    public class NotesController : PlatformBaseController {
        private NotesMetaDataService notesMetaDataService;

        /// <summary>
        /// Notes Controller, Handles all of the stuff for notes
        /// </summary>
        /// <param name="clientFactory"> An Http Client Factory</param>
        /// <param name="notesMetaDataService">Here is a service that I use to connect to the database</param>
        /// <returns></returns>
        public NotesController (IHttpClientFactory clientFactory, NotesMetaDataService notesMetaDataService) : base (clientFactory) {
            this.notesMetaDataService = notesMetaDataService;
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet ("{id}")]
        public ActionResult<string> GetValues (string id) {
            notesMetaDataService.DoStuff ();
            return $"Id value from URL: {id}";
        }

    }
}