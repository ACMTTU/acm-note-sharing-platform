using System;
using System.Linq;
using System.Net;
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

        [HttpGet]
        [Route("get/noteId/{noteId}")]
        public async Task<List<Tag>> GetTags(string noteId)
        {
<<<<<<< HEAD
            QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE c.noteId= '" + noteId + "'");
=======
            string sqlQueryText = "SELECT * FROM c WHERE c.noteId = @noteId";
            QueryDefinition query = new QueryDefinition(sqlQueryText).WithParameter("@noteId", noteId);

>>>>>>> 25342bfbc39f627ea4dbdd817e1e8292b9276e8f
            FeedIterator<Tag> iterator = _dbService.tagContainer.GetItemQueryIterator<Tag>(query);

            List<Tag> tags = new List<Tag>();

            while (iterator.HasMoreResults)
            {
                FeedResponse<Tag> resultSet = await iterator.ReadNextAsync();
                foreach (Tag tag in resultSet)
                {
                    tags.Add(tag);
                }
            }

            return tags;
        }

        /// <summary>
        /// Retrieve a list of noteId by tag name.
        /// </summary>
        /// <param name="name">The tag name</param>
        /// <returns>A list of noteId determined by the parameter passed</returns>
        [HttpGet]
        [Route("noteId/{name}")]
        public async Task<List<string>> GetNotesByTagName(string name)
        {
            string sqlQueryText = "SELECT * FROM c WHERE c.name= @name";
            QueryDefinition query = new QueryDefinition(sqlQueryText).WithParameter("@name", name);

            FeedIterator<Tag> iterator = _dbService.tagContainer.GetItemQueryIterator<Tag>(query);

            List<string> tagName = new List<string>();

            while (iterator.HasMoreResults)
            {
                FeedResponse<Tag> resultSet = await iterator.ReadNextAsync();
                foreach (Tag tag in resultSet)
                {
                    tagName.Add(tag.noteId);
                }
            }

            return tagName;
        }

        /// <summary>
        /// Create a new tag. Pass everything other than id.
        /// </summary>
        /// <returns>OK if successful otherwise bad request</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewTag(TagDto tagDto)
        {
            Tag tag = new Tag();

            try
            {
                if (ModelState.IsValid)
                {

                    tag.name = tagDto.name;
                    tag.noteId = tagDto.noteId;
                    tag.userId = tagDto.userId;
                    tag.hidden = tagDto.hidden;
                    tag.isSystem = false;

                    //We need it to be known since ReadItemAsync only checks for id
                    tag.id = tag.name + "." + tag.noteId + "." + tag.userId;

                    //searches the database if the tagResponse is there
                    ItemResponse<Tag> tagResponse = await _dbService.tagContainer.ReadItemAsync<Tag>(tag.id, new PartitionKey(tag.noteId));
                    //if it finds it in the database
                    return BadRequest("TAG ALREADY EXISTS");
                }
            }

            //catch the exception of the tagResponse
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                await _dbService.tagContainer.CreateItemAsync(tag);
                return Ok("TAG IS ADDED SUCCESSFULLY");
            }

            //If it didn't go through all the test cases
            return BadRequest("Body is invalid");

        }

        /// <summary>
        /// Deletes all the tags associated with a specific noteId. Useful for when a note
        /// is deleted.
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <returns>Returns a list of tags that have been deleted</returns>
        [HttpDelete]
        [Route("delete/noteId/{noteId}")]
        public async Task<IActionResult> DeleteTags(string noteId)
        {
            //gets the list of tags after calling the GetTags function
            List<Tag> tags = GetTags(noteId).Result;

            if (!tags.Any()) //Checks to see if tags list is either null or empty
                return BadRequest("noteId does not exist");

            foreach (Tag tag in tags) //deletes them 1 by 1
            {
                await _dbService.tagContainer.DeleteItemAsync<Tag>(tag.id, new PartitionKey(tag.noteId));
            }

            return Ok("Tags successfully deleted.");

        }

        /// <summary>
        /// Deletes a specific single tag. Useful for when a user wants to delete their own
        /// tag on a note.
        /// </summary>
        /// <param name="noteId">The Note ID associated with the tag to be deleted</param>
        /// <param name="name">The name of the tag to delete</param>
        /// <returns></returns>

        [HttpDelete]
        [Route("noteId/{noteId}/name/{name}")]
        public async Task<IActionResult> DeleteTag(string noteId, string name)
        {
            //gets the list of tags in the noteId
            List<Tag> tags = GetTags(noteId).Result;

            if (!tags.Any()) //Checks to see if tags list is either null or empty
                return BadRequest("noteId does not exist");

            //gets the specific tag with the given parameter
            Tag foundTag = tags.Find(tag => tag.name == name);

            if (foundTag != null)
            {
                await _dbService.tagContainer.DeleteItemAsync<Tag>(foundTag.id, new PartitionKey(foundTag.noteId));
                return Ok("Tag successfully deleted.");
            }
            //If the given tag name is not within the given noteId
            return BadRequest("There is no " + name + " tag associated with noteId");

        }
    }
}