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
        /// Gets the rating of a specific note.
        /// </summary>
        /// <param name="noteId">The ID of the note the user wants the ratings for</param>
        /// <returns>The rating of the note</returns>
        [HttpGet("{noteId}")]
        public async Task<ActionResult> GetRating(string noteId)
        {
            if (noteId == null)
                return NotFound();
            else
            {
                Rating rating = await _dbService.ratingContainer.ReadItemAsync<Rating>(noteId, new PartitionKey(noteId));
                return Ok(rating);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> NewRating(Rating rating)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("{noteId}/rating/{stars}")]
        public async Task<bool> UpdateRating(string noteId, string stars, Rating update)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpDelete("{noteId}")]
        public async Task<Rating> DeleteRating(string noteId)
        {

        }

    }
}