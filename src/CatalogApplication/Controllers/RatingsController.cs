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
    [Route("api/catalog/rating")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ConnectionService _dbService;

        public RatingsController(IHttpClientFactory factory, ConnectionService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Get a single rating based off noteId.
        /// </summary>
        /// <param name="noteId">The note you want the rating of</param>
        /// <returns>A single Rating object</returns>
        [HttpGet("{noteId}")]
        public Task<Rating> GetRating(string noteId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new rating, useful for when a new note is created.
        /// </summary>
        /// <returns>OK on success, bad request otherwise</returns>
        [HttpPost]
        public Task<IActionResult> NewRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a rating, should be called when a user rates a note.
        /// </summary>
        /// <param name="noteId">Which note's rating to update</param>
        /// <param name="stars">The rating that the user gave, should be 0-5 inclusive</param>
        /// <returns></returns>
        [HttpPut("{noteId}/rating/{stars}")]
        public Task<IActionResult> UpdateRating(string noteId, string stars, Rating update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a specific rating, useful for when a note gets deleted.
        /// </summary>
        /// <param name="noteId">White note's rating to delete</param>
        /// <returns>OK on success, otherwise bad request</returns>
        [HttpDelete("{noteId}")]
        public Task<IActionResult> DeleteRating(string noteId)
        {
            throw new NotImplementedException();
        }

    }
}