using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using UserApplication.Models;

namespace ACMTTU.NoteSharing.Platform.UserApplication.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : PlatformBaseController
    {
        public UserController(IHttpClientFactory factory) : base(factory) { }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetValues(string id)
        {
            using (CosmosClient dbClient = await this.GetDatatbaseClient())
            {
                await dbClient.CreateDatabaseIfNotExistsAsync("UserApplication");
            }

            return $"Id value from URL: {id}";
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        public Task<ActionResult<string>> CreateNewUser(UserInfo user)
        {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Delete user
        /// This should only delete user information.
        /// This shouldn't delete any notes or comments.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult<string>> DeleteUser(string id)
        {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Updates a User by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult<string>> UpdateUser(string id, UserInfo update)
        {
            throw new NotImplementedException();
        }
    }
}