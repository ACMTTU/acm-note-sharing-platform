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
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>A boolean indicating whether a tag is created or not</returns>
        [HttpPost]
        public async Task<bool> CreateNewTag(Tag tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tag.id = System.Guid.NewGuid().ToString();
                    await _dbService.tagContainer.CreateItemAsync(tag);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates a tag based off noteId, userId, and name.
        /// </summary>
        /// <param name="noteId">The noteId to look for tags in</param>
        /// <param name="userId">The userId the tag must have</param>
        /// <param name="name">The tag name to update</param>
        /// <returns></returns>
        [HttpPut("{noteId}/users/{userId}/names/{name}")]
        public Task<bool> UpdateTag(string noteId, string userId, string name, Tag update)
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
        public Task<List<Tag>> DeleteTags(string noteId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <param name="userId">The ID of the user who wants to delete the tag</param>
        /// <returns></returns>
        [HttpDelete("{noteId}/users/{userId}")]
        public Task<Tag> DeleteTag(string noteId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}