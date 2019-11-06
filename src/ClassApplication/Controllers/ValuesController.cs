﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.ClassApplication.Services;

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
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetValues(string id)
        {
            await this.classesContainer.CreateItemAsync<dynamic>(new Object() { });
            return ("Created an object");
        }

        /*------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// This call is used to add a Note to the Classroom
        ///     using the Note ID generated by the Notes Applicaton
        /// </summary>
        /// <param name="classId">Id of the classroom</param>
        /// <param name="notesId">Id of the note being added
        [HttpPut("{classId}/notes/{notesId}")]
        public async Task<ActionResult<string>> AddNoteToClass(string classId, string notedId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This call is used to remove a Note from a Classroom
        ///     using the Note ID generated by the Notes Application
        /// </summary>
        /// <param name="classId">Id of the classroom</param>
        /// <param name="notesId">Id of the note being added
        [HttpDelete("{classId}/notes/{notesId}")]
        public async Task<ActionResult<string>> RemoveNoteFromClass(string classId, string notedId)
        {
            throw new NotImplementedException();
        }

        /*------------------------------------------------------------------------------------------------*/
    }
}