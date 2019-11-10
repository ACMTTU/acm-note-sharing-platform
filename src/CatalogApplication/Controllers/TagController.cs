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
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("{noteId}/users/{userId}")]
        public async Task<bool> UpdateTag(string noteId, string userId, Tag update)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <returns>Returns a list of tags that have been deleted</returns>
        [HttpDelete("{noteId}")]
        public async Task<List<Tag>> DeleteTags(string noteId)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <param name="userId">The ID of the user who wants to delete the tag</param>
        /// <returns></returns>
        [HttpDelete("{noteId}/users/{userId}")]
        public async Task<Tag> DeleteTag(string noteId, string userId)
        {

        }
    }
}