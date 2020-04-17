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
        /// Creates a new rating. Pass everything other than id
        /// </summary>
        /// <param name="ratingDto">The rating object with its associate class attributes</param>
        /// <returns> Action Result for the async operation
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateRating(RatingDto ratingDto)
        {
            Rating rating = new Rating();

            try
            {
                if (ModelState.IsValid)
                {
                    rating.noteId = ratingDto.noteId;
                    rating.userId = ratingDto.userId;
                    rating.rating = ratingDto.rating;

                    //We need it to be known since ReadItemAsync only checks for id
                    rating.id = rating.noteId + "." + rating.userId;

                    //searches the database if the tagResponse is there
                    ItemResponse<Rating> ratingResponse = await _dbService.ratingContainer.ReadItemAsync<Rating>(rating.id, new PartitionKey(rating.noteId));
                    //if it finds it in the database
                    //await _dbService.ratingContainer.ReplaceItemAsync<Rating>(rating, rating.id);
                    Rating rating_update = await _dbService.ratingContainer.ReadItemAsync<Rating>(rating.id, new PartitionKey(rating.noteId));
                    rating_update.rating = rating.rating;
                    await _dbService.ratingContainer.ReplaceItemAsync<Rating>(rating_update, rating.noteId);
                    return Ok("RATING IS REPLACED SUCCESSFULLY");
                }
            }

            //catch the exception of the tagResponse
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                await _dbService.ratingContainer.CreateItemAsync(rating);
                return Ok("RATING IS ADDED SUCCESSFULLY");
            }

            //If it didn't go through all the test cases
            return BadRequest("Body is invalid");

        }

        /// <summary>
        /// Updates the rating on the basis of the noteID
        /// </summary>
        /// <param name="noteId">ID of the note whose ratings need to be updated.</param>
        /// <param name="userID">ID of the user who wants to update the rating.</param>
        /// <param name="userRating">An int value that indicates the new rating for the note. </param>
        /// <returns></returns>
        [HttpPut("{noteId}/rating/{userRating}")]
        public async Task<IActionResult> UpdateRating(string noteId, string userID, int userRating)
        {
            string id;
            id=noteId + "." + userID;
            
            try
            {
                if (id == null || userRating <= 0)
                    return BadRequest();
                else
                {
                    //searches the database if the tagResponse is there
                    ItemResponse<Rating> ratingResponse = await _dbService.ratingContainer.ReadItemAsync<Rating>(id, new PartitionKey(noteId));
                    //if it finds it in the database
                    Rating rating_update = await _dbService.ratingContainer.ReadItemAsync<Rating>(id, new PartitionKey(noteId));
                    rating_update.rating=userRating;
                    //rating_update.rating = (rating_update.rating + userRating) / (rating_update.numRatings);
                    await _dbService.ratingContainer.ReplaceItemAsync<Rating>(rating_update, noteId);
                    return Ok("RATING IS REPLACED SUCCESSFULLY");
                }
            }

            //catch the exception of the tagResponse
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return BadRequest();
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

            if (!ratings.Any())
            {
                return null;
            }

            foreach (Rating rating in ratings)
            {
                await _dbService.ratingContainer.DeleteItemAsync<Rating>(rating.id, new PartitionKey(rating.noteId));
                deletedRatings.Add(rating);
            }
            return deletedRatings;
        }
    }
}