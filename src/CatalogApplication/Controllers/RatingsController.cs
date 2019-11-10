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
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("{noteId}")]
        public Task<Rating> GetRating(string noteId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> NewRating(Rating rating)
        {
            throw new NotImplementedException();
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