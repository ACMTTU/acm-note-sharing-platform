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
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly ConnectionService _dbService;
        public CatalogController(IHttpClientFactory factory, ConnectionService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="noteId">The note ID</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("tags/note/{noteId}")]
        public async Task<List<Tag>> GetValues(string noteId)
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
    }
}