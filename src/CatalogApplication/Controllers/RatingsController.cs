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
<<<<<<< HEAD
        public async Task<ActionResult> GetRating(string noteId)
        {
            if (noteId == null)
                return NotFound();
            else
            {
                Rating rating = await _dbService.ratingContainer.ReadItemAsync<Rating>(noteId, new PartitionKey(noteId));
                return Ok(rating);
            }
=======
        public Task<Rating> GetRating(string noteId)
        {
            throw new NotImplementedException();
>>>>>>> TeamFox
        }

        /// <summary>
        /// Creates a new rating
        /// </summary>
        /// <param name="rating">The rating object with its associate class attributes</param>
        /// <returns> Action result for creating a new rating
        /// </returns>
        [HttpPost]
        public Task<bool> NewRating(Rating rating)
        {
<<<<<<< HEAD
            throw{
                if (ModelState.IsValid)
                {
                    GetRating(rating.noteId)
                }
            }
=======
            throw new NotImplementedException();
>>>>>>> TeamFox
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("{noteId}/rating/{stars}")]
        public Task<bool> UpdateRating(string noteId, string stars, Rating update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpDelete("{noteId}")]
        public Task<Rating> DeleteRating(string noteId)
        {
            throw new NotImplementedException();
        }

    }
}