using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ACMTTU.NoteSharing.Platform.ClaimsService.Controllers
{
    [Route("api/claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> GetValues(string id)
        {
            return new string[] { $"value{id}" };
        }
    }
}