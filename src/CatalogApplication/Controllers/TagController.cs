using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.CatalogApplication.Services;
using CatalogApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACMTTU.NoteSharing.Platform.CatalogApplication.Controllers
{
    [Route("api/catalog/tags")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly ConnectionService _dbService;
        public CatalogController(IHttpClientFactory factory, ConnectionService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Retrieve a list of tags by noteId.
        /// </summary>
        /// <param name="noteId">The note ID</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{noteId}")]
        public async Task<List<Tag>> GetTags(string noteId)
        {
            QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE c.noteId={noteId}");
            FeedIterator<Tag> iterator = _dbService.tagContainer.GetItemQueryIterator<Tag>(query);

            List<Tag> tags = new List<Tag>();

            while (iterator.HasMoreResults)
            {
                var resultSet = await iterator.ReadNextAsync();
                foreach (Tag tag in resultSet)
                {
                    tags.Add(tag);
                }
            }

            return tags;
        }

        /// <summary>
        /// Gets tags by noteId and userId
        /// </summary>
        /// <param name="noteId">The noteId to look for the tag</param>
        /// <param name="userId">The userId to look for the tag</param>
        /// <returns>A boolean indicating whether a tag is created or not</returns>
        [HttpGet("{noteId}/users/{userId}")]
        public async Task<List<Tag>> GetTagsByNoteAndUser(string noteId, string userId)
        {
            return await GetTags($"SELECT * FROM c WHERE c.noteId={noteId} AND c.userId={userId}");
        }

        /// <summary>
        /// Create a new tag. Pass everything other than id.
        /// </summary>
        /// <returns>OK if successful otherwise bad request</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewTag(Tag tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tag.id = System.Guid.NewGuid().ToString();
                    await _dbService.tagContainer.CreateItemAsync(tag);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates the tag created by the user
        /// </summary>
        /// <param name="noteId">The ID of the note that needs its tags updated</param>
        /// <param name="userId">The ID of the user requesting the update</param>
        /// <param name="name"> The name of the new tag </param>
        /// <param name="update">The body of the updated tag</param>
        /// <returns></returns>
        [HttpPut("{noteId}/users/{userId}/names/{name}")]
        public Task<IActionResult> UpdateTag(string noteId, string userId, string name, Tag update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes all the tags associated with a specific noteId. Useful for when a note
        /// is deleted.
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <returns>Returns a list of tags that have been deleted</returns>
        [HttpDelete("{noteId}")]
        public Task<IActionResult> DeleteTags(string noteId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a specific single tag. Useful for when a user wants to delete their own
        /// tag on a note.
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <param name="userId">The ID of the user who wants to delete the tag</param>
        /// <param name="name">The name of the tag to delete</param>
        /// <returns></returns>
        [HttpDelete("{noteId}/users/{userId}/name/{name}")]
        public Task<IActionResult> DeleteTag(string noteId, string userId, string name)
        {
            throw new NotImplementedException();
        }
    }
}