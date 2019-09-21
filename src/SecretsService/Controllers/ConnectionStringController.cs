using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.DataContracts;
using ACMTTU.NoteSharing.SecretsService.Providers;
using ACMTTU.NoteSharing.SecretsService.Models;

namespace ACMTTU.NoteSharing.SecretsService.Controllers
{
    [Route("api/connection")]
    [ApiController]
    public class ConnectionStringController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello";
        }

        [HttpGet("{option}")]
        public ActionResult<EnvironmentVariablePayload> GetConnectionString(string option)
        {
            // Grab the environment variable
            EnvironmentVariableOptions choice;
            if (!Enum.TryParse<EnvironmentVariableOptions>(option, out choice))
            {
                throw new Exception($"Unsupported Environment Variable: {option}. Please use the DataContracts library");
            }

            return EnvironmentReader.GetEnvironmentVariableFromKeyVault(choice);
        }
    }
}
