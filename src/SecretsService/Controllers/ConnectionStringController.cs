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

        /// <summary>
        /// Grabs a connection string from Azure Key Vault to use in database or
        /// Storage Clients
        /// </summary>
        /// <param name="option">Must be "Database" or "BlobStorage" without the quotes</param>
        /// <returns>The connection string</returns>
        [HttpGet("{option}")]
        public ActionResult<EnvironmentVariablePayload> GetConnectionString(string option)
        {
            return environmentReader.GetEnvironmentVariableFromKeyVault(option);
        }
    }
}