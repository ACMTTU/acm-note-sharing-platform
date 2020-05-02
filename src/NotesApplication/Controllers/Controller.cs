using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.NotesApplication.Services;
using NotesApplication.Models;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : PlatformBaseController
    {
        private Container _notesContainer;
        private PartitionKey _partKey = new PartitionKey("notes");
        public NotesController(IHttpClientFactory factory, DBService db) : base(factory)
        {
            this._notesContainer = db.container;
        }

        /// <summary>
        /// Will create a new note with a generated Id
        /// </summary>
        /// <param name="note">note metadata</param>
        /// <returns></returns>
        [HttpPut("create")]
        public async Task<ActionResult<string>> CreateNote(Note note)
        {
            // This copies the given note, (which callers will pass in with JSON in their request), to a new Note object. The new Note object has a valid, newly generated ID.
            // The newly generated id is returned, allowing for callers to edit properties of the newly created note.
            Note n = await this._notesContainer.CreateItemAsync(new Note(Guid.NewGuid().ToString(), note.Name, note.Notes, note.CreatedAt, note.LastModified));
            return this.Ok(n.id);
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
            oldNote = await _notesContainer.ReadItemAsync<Note>(noteId, _partKey);

            if (oldNote.ownerId == userId) // verifies userId is allowed to modify noteId
            {
                await _notesContainer.ReplaceItemAsync<Note>(update, noteId);
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
        public async Task<Note> GetNote(string noteId, string userId)
        {
            Note note;
            note = await _notesContainer.ReadItemAsync<Note>(noteId, _partKey);

            if (note.ownerId == userId)
            {
                return note;
            }
            else
            {
                return null;
            }
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
                await _notesContainer.DeleteItemAsync<Note>(noteId, _partKey);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Verify permision to manipulate note
        /// </summary>
        /// <param name="noteId">The id of the note we want to manipulate</param>
        /// <param name="userId">The id of the user manipulating the note</param>
        /// <returns>if note exists and user has permision to manipulate it return true else return false.</returns>        
        private async Task<bool> verifyPermission(string noteId, string userId)
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
