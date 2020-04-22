﻿using System;
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

        private IDBService _dbService;

        public NotesController(IHttpClientFactory factory, IDBService dbService) : base(factory)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Creates new note with given name, contents, and date information
        /// </summary>
        /// <param name="name">Name of the note</param>
        /// <param name="notes">Contents of the note object</param>
        /// <param name="createdAt">Date of creation</param>
        /// <param name="lastModified">Date of last modification</param>
        /// <returns>ID of new note</returns>
        [HttpPut("createfromscratch")]
        public async Task<ActionResult<string>> CreateNote(string name, string[] notes, DateTime createdAt, DateTime lastModified)
        {
            // This creates a note given the usual constructor, with a newly generated ID.
            Note databased = await _dbService.CreateItem<Note>(new Note(Guid.NewGuid().ToString(), name, notes, createdAt, lastModified));
            return Ok(databased.id);

        }

        /// <summary>
        /// Creates a new note given a JSON note object
        /// </summary>
        /// <param name="note">The raw note object, not part of the database</param>
        /// <returns>ID of new note</returns>
        [HttpPut("create")]
        public async Task<ActionResult<string>> CreateNote(Note note)
        {
            // This copies the given note, (which callers will pass in with JSON in their request), to a new Note object. The new Note object has a valid, newly generated ID.
            // The newly generated id is returned, allowing for callers to edit properties of the newly created note. 
            Note databased = await _dbService.CreateItem<Note>(new Note(Guid.NewGuid().ToString(), note.Name, note.Notes, note.CreatedAt, note.LastModified));
            return Ok(databased.id);

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
            oldNote = await _dbService.ReadItem<Note>(noteId);

            if (oldNote.ownerId == userId) // verifies userId is allowed to modify noteId
            {
                await _dbService.ReplaceItem<Note>(update, noteId);
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
            note = await _dbService.ReadItem<Note>(noteId);

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
                await _dbService.DeleteItem<Note>(noteId);
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

}