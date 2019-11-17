﻿using System;
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

        private async Task<List<Tag>> GetTags(string query)
        {
            QueryDefinition queryDef = new QueryDefinition(query);
            FeedIterator<Tag> iterator = _dbService.tagContainer.GetItemQueryIterator<Tag>(queryDef);

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
        /// Retrieve a list of tags by noteId.
        /// </summary>
        /// <param name="noteId">The note ID</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{noteId}")]
        public async Task<List<Tag>> GetTagsByNote(string noteId)
        {
            return await GetTags($"SELECT * FROM c WHERE c.noteId={noteId}");
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

        /// Updates a tag based off of id. 
        /// </summary>
        /// <param name="id">The id to look for the specific tag</param>
        /// <param name="tag">the updated version of the tag</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ActionName("Edit")]
        public async Task<ActionResult> UpdateTag(string id, Tag tag)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _dbService.tagContainer.UpsertItemAsync<Tag>(tag, new PartitionKey(id));
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }

            }
            return null;
        }

        /// <summary>
        /// Deletes all the tags associated with a specific noteId. Useful for when a note
        /// is deleted.
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <returns>Returns a list of tags that have been deleted</returns>
        [HttpDelete("notes/{noteId}")]
        public Task<IActionResult> DeleteTags(string noteId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a specific single tag. Useful for when a user wants to delete their own
        /// tag on a note.
        /// </summary>
        /// <param name="id">The id associated with the tag to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _dbService.tagContainer.DeleteItemAsync<Tag>(id, new PartitionKey(id));
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            return null;
        }
    }
}
