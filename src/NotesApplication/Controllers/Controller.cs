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
        private PartitionKey partKey = new PartitionKey("notes");
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
        public async Task<IActionResult> EditNote(string noteId, string userId, Note update)
        {
            Note oldNote;
            oldNote = await NotesContainer.ReadItemAsync<Note>(noteId, partKey);

            if (oldNote.ownerId == userId) // verifies userId is allowed to modify noteId
            {
                await NotesContainer.ReplaceItemAsync<Note>(update, noteId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// get a note object given the note id
        /// </summary>
        /// <param name="noteId">The id of the not we want</param>
        /// <param name="userId">The id of the user submiting the request</param>
        /// <returns>if note exists and user has permision to get it return Sucess else return failure.</returns>
        [HttpGet("{noteId}/users/${userId}")]
        public async Task<ActionResult<Note>> GetNote(string noteId, string userId)
        {
            Note note;
            note = await NotesContainer.ReadItemAsync<Note>(noteId, partKey);

            if (note.ownerId == userId)
            {
                return Ok(note);
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// get a all notes for a given user
        /// </summary>
        /// <param name="userId">The id of the user submiting the request</param>
        /// <returns>if notes exist and user has permission to get it return Success else return failure.</returns>
        [HttpGet("users/${userId}")]
        public async Task<ActionResult<Note[]>> GetUserNotes(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete note object after verifying permisions
        /// </summary>
        /// <param name="noteId">The id of the note we want to delete</param>
        /// <param name="userId">The id of the user deleting the note</param>
        /// <returns>if note exists and user has permision to delete it return Sucess else return failure.</returns>
        [HttpDelete("{noteId}/users/{userId}")]
        public async Task<IActionResult> DeleteNote(string noteId, string userId)
        {
            if (await verifyPermission(noteId, userId) == true)
            {
                await NotesContainer.DeleteItemAsync<Note>(noteId, partKey);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<bool> verifyPermission(string noteId, string userId)
        {
            Note deleteNote = await GetNote(noteId, userId);

            if (deleteNote.ownerId == userId) // verifies userId matches note ID
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}