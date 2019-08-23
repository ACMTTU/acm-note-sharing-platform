using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ACMTTU.NoteSharing.Platform.AuthApplication.Controllers
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
