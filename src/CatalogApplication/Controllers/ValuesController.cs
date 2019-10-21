using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Platform.CatalogApplication.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : PlatformBaseController
    {
        public CatalogController(IHttpClientFactory factory) : base(factory) { }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("tags/{id}")]
        public async Task<ActionResult<string>> GetValues(string id)
        {

            return $"Id value from URL: {id}";
        }
    }
}