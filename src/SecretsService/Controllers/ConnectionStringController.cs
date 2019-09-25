using ACMTTU.NoteSharing.SecretsService.Providers;
using ACMTTU.NoteSharing.Shared.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace ACMTTU.NoteSharing.SecretsService.Controllers
{
    [Route("api/connection")]
    [ApiController]
    public class ConnectionStringController : ControllerBase
    {
        private IEnvironmentReader environmentReader;
        public ConnectionStringController(IEnvironmentReader environmentReader)
        {
            this.environmentReader = environmentReader;
        }

        [HttpGet("{option}")]
        public ActionResult<EnvironmentVariablePayload> GetConnectionString(string option)
        {
            return environmentReader.GetEnvironmentVariableFromKeyVault(option);
        }
    }
}