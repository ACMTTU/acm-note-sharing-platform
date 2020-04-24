using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using ACMTTU.NoteSharing.Platform.UserApplication.Services;
using Microsoft.Azure.Cosmos;
using UserApplication.Models;

namespace ACMTTU.NoteSharing.Platform.UserApplication.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ConnectionService _dbService;
        public UserController(IHttpClientFactory factory, ConnectionService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Create a new user info. Pass everything
        /// </summary>
        /// <returns>OK if successful otherwise bad request</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(UserInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //searches the database if user already exists
                    await _dbService.userContainer.ReadItemAsync<UserInfo>(user.id, new PartitionKey(user.id));
                    //if it finds it in the database
                    return BadRequest("USER ALREADY EXISTS");
                }
            }

            //catch the exception of the readitemasync
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                await _dbService.userContainer.CreateItemAsync(user);
                return Ok("USER IS ADDED SUCCESSFULLY");
            }

            //If it didn't go through all the test cases
            return BadRequest("Body is invalid");


        }

        /// <summary>
        /// Delete user
        /// This should only delete user information.
        /// This shouldn't delete any notes or comments.
        /// </summary>
        /// <param name="userId">The ID of the User that is going to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/userId/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {

            try
            {
                await _dbService.userContainer.DeleteItemAsync<UserInfo>(userId, new PartitionKey(userId));
                return Ok("User successfully deleted.");

            }
            catch
            {
                return BadRequest("userId does not exist.");
            }

        }
        /*
                /// <summary>
                /// Updates a User by ID
                /// </summary>
                /// <param name="id"></param>
                /// <param name="update"></param>
                /// <returns></returns>
                [HttpPut]
                [Route("put/{update}")]
                public Task<ActionResult<string>> UpdateUser(string id, UserInfo update)
                {
                    throw new NotImplementedException();
                }
                */
    }
}