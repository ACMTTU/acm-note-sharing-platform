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
        /// Creates a new rating for the given note
        /// </summary>
        /// <param name="newRating">The rating object with its associate class attributes</param>
        /// <returns> Action Result for the async operation
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> CreateRating(Rating newRating)
        {
            if (newRating.noteId == null && newRating.rating <= 0)
                return BadRequest();
            else
            {
                newRating.numRatings = 1;
                Rating rating = await _dbService.ratingContainer.CreateItemAsync(newRating);
                return Ok(rating);

            }

        }
        /// <summary>
        /// Updates the rating on the basis of the noteID
        /// </summary>
        /// <param name="noteId">ID of the note whose ratings need to be updated.</param>
        /// <param name="stars">A double value that indicates the new rating for the note. </param>
        /// <returns></returns>
        [HttpPut("{noteId}/rating/{stars}")]
        public Task<bool> UpdateRating(string noteId, double stars)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the most recent rating created by the user
        /// </summary>
        /// <param name="noteId">The ID of note that needs its rating to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{noteId}")]
        public Task<IActionResult> DeleteRating(string noteId)
        {
            throw new NotImplementedException();
        }

    }
}