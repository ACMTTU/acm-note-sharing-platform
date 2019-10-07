using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ACMTTU.NoteSharing.Platform.UserApplication.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetValues(string id)
        {
            return new string[] { $"value{id}" };
        }
    }
}