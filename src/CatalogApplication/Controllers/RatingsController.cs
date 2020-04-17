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
using System.Linq;

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
        public async Task<List<Rating>> GetRating(string noteId)
        {
            String sqlQueryText = "SELECT * FROM c WHERE c.noteId = @noteId";
            QueryDefinition query = new QueryDefinition(sqlQueryText).WithParameter("@noteId", noteId);
            FeedIterator<Rating> iterator = _dbService.ratingContainer.GetItemQueryIterator<Rating>(query);

            List<Rating> ratings = new List<Rating>();

            while (iterator.HasMoreResults)
            {
                var resultSet = await iterator.ReadNextAsync();
                foreach (Rating rating in resultSet)
                {
                    ratings.Add(rating);
                }
            }
            return ratings;
        }

        /// <summary>
        /// Creates a new rating for the given note
        /// </summary>
        /// <param name="newRating">The rating object with its associate class attributes</param>
        /// <returns> Action Result for the async operation
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateRating(Rating newRating)
        {
            if (newRating.noteId == null || newRating.rating <= 0)
                return BadRequest();
            else
            {
                //newRating.numRatings = 1;
                Rating rating = await _dbService.ratingContainer.CreateItemAsync(newRating);
                return Ok(rating);

            }

        }
        /// <summary>
        /// Updates the rating on the basis of the noteID
        /// </summary>
        /// <param name="noteId">ID of the note whose ratings need to be updated.</param>
        /// <param name="userRating">A double value that indicates the new rating for the note. </param>
        /// <returns></returns>
        [HttpPut("{noteId}/rating/{userRating}")]
        public async Task<ActionResult> UpdateRating(string noteId, double userRating)
        {
            if (noteId == null || userRating <= 0)
                return BadRequest();
            else
            {
                Rating rating = await _dbService.ratingContainer.ReadItemAsync<Rating>(noteId, new PartitionKey(noteId));
                //rating.numRatings = rating.numRatings + 1;
                // rating.rating = (rating.rating + userRating) / (rating.numRatings);
                await _dbService.ratingContainer.ReplaceItemAsync<Rating>(rating, noteId);
                return Ok(rating);
            }
        }

        /// <summary>
        /// Delete a specific rating, useful for when a note gets deleted.
        /// </summary>
        /// <param name="noteId">White note's rating to delete</param>
        /// <returns>OK on success, otherwise bad request</returns>
        [HttpDelete("{noteId}")]
        public async Task<List<Rating>> DeleteRating(string noteId)
        {
            if (noteId == null)
            {
                return null;
            }

            List<Rating> ratings = GetRating(noteId).Result;
            List<Rating> deletedRatings = new List<Rating>();

            foreach (Rating rating in ratings)
            {
                await _dbService.ratingContainer.DeleteItemAsync<Rating>(rating.id, new PartitionKey(rating.noteId));
                deletedRatings.Add(rating);
            }
            return deletedRatings;
        }
    }
}




