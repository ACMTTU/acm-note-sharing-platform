using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ACMTTU.NoteSharing.Platform.UserApplication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET api
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            return new string[] { "value3", "value4" };
        }
    }
}