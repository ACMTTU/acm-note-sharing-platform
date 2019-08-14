using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ACMTTU.NoteSharing.Platform.NoteService.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet("new")]
        public ActionResult<string> GetNewValues()
        {
            return "Hello World!";
        }
    }
}
