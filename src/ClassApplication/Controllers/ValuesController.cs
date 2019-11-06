using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.ClassApplication.Services;
using ClassApplication.Models;

namespace ACMTTU.NoteSharing.Platform.ClassApplication.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : PlatformBaseController
    {

        private Container classesContainer;
        public ClassController(IHttpClientFactory factory, DatabaseService databaseService) : base(factory)
        {
            this.classesContainer = databaseService.classroomsContainer;
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="classroom">Has a ClassId, Name, and Description</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateClassroom(Classroom classroom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">classroom id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetClassroom(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">classroom id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteClassroom(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">classroom id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateClassroom(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="classID">classroom id</param>
        ///<param name="userID">user id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpPut("{classID}/Users/{userID}")]
        public async Task<ActionResult<string>> AddUserToClass(string classID, string userID)
        {
            throw new NotImplementedException();
        }

    }
}