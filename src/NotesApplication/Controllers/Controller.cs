using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using NotesApplication;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Controllers {
    [Route ("api/notes")]
    [ApiController]
    public class NotesController : PlatformBaseController {
        /// <summary>
        /// Notes Controller, Handles all of the stuff for notes
        /// </summary>
        /// <param name="clientFactory"> An Http Client Factory</param>
        /// <returns></returns>
        public NotesController (IHttpClientFactory clientFactory) : base (clientFactory) {
        }

        /// <summary>
        /// After checking if a user has permissions to delete a note 
        /// deletes the note and all meta data assosiated with note 
        /// </summary>
        /// <param name="noteId">The note we are trying to delete</param>
        /// <param name="userId">The user trying to delete the note</param>
        /// <returns>if note exists and user has permision to delete return Sucess else return failure</returns>
        [HttpDelete("{noteId}/users/${userId}")]
        public async Task DeleteNote(string noteId, string userId)
        {
            using (CosmosClient dbClient = await this.GetDatatbaseClient())
            {
                var dbRef = await dbClient.CreateDatabaseIfNotExistsAsync("NotesApplication");
                var containerRef = await dbRef.Database.CreateContainerIfNotExistsAsync("Notes", "/Name");
            }

        }

        /// <summary>
        /// Edits given meta data assosiated with note
        /// We will also check to see if the user has permissions to edit this note
        /// </summary>
        /// <param name="noteId">The id of the note to edit</param>
        /// <param name="userId">The id of the user editting the note</param>
        /// <param name="update">A partial object of the type Note, with the changes</param>
        /// <returns>if note exists and user has permision to edit return Sucess else return failure.</returns>
        [HttpPut("{noteId}/users/{userId}")]
        public async Task EditNote(string noteId, string userId, Note update)
        {

        }

        /// <summary>
        /// get all notes associated with user
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>if user exists return Sucess else return failure.</returns>
        [HttpGet("users/{user_id}")]
        public async Task GetUsersNotes(string user_id)
        {

        }

        /// <summary>
        /// get a note object given the note id
        /// </summary>
        /// <param name="noteId">The id of the not we want</param>
        /// <returns>if note exists and user has permision to get it return Sucess else return failure.</returns>
        [HttpGet("{noteId}/users/${userId}")]
        public async Task GetNote(string noteId, string userId)
        {

        }
    }

}