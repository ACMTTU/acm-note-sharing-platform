using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.NotesApplication.Services;
using NotesApplication;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Controllers
{
    [Route("api/notes")]
    [ApiController]

    public class NotesController : PlatformBaseController
    {
        private Container NotesContainer;
        public NotesController(IHttpClientFactory factory, DBService db) : base(factory)
        {
            NotesContainer = db.container;
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
        public async Task<bool> EditNote(string noteId, string userId, Note update)
        {
            Note oldNote = await GetNote(noteId, userId);

            if (oldNote.ownerId == userId) // verifies userId is allowed to modify noteId
            {
                await NotesContainer.ReplaceItemAsync<Note>(update, noteId);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// get a note object given the note id
        /// </summary>
        /// <param name="noteId">The id of the not we want</param>
        /// <returns>if note exists and user has permision to get it return Sucess else return failure.</returns>
        [HttpGet("{noteId}/users/${userId}")]
        public Task<Note> GetNote(string noteId, string userId)
        {
        }

        /// <summary>
        /// Delete note object after verifying permisions
        /// </summary>
        /// <param name="noteId">The id of the note we want to delete</param>
        /// <param name="userId">The id of the user deleting the note</param>
        /// <returns>if note exists and user has permision to delete it return Sucess else return failure.</returns>
        [HttpDelete("{noteId}/users/{userId}")]
        public async Task<bool> DeleteNote(string noteId, string userId)
        {
            Note deleteNote = await GetNote(noteId, userId);

            if (deleteNote.ownerId == userId) // verifies userId is allowed to delete note
            {
                await NotesContainer.DeleteItemAsync<Note>(noteId, new PartitionKey(noteId));
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}