﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Platform.ClaimsService.Controllers
{
    [Route("api/claims")]
    [ApiController]
    public class ClaimsController : PlatformBaseController
    {
        public ClaimsController(IHttpClientFactory factory) : base(factory) { }

        /// <summary>
        /// This is how you document code
        /// 
        /// Visit the microservice's endpoint and append /swagger
        /// to see your docs in action
        /// </summary>
        /// <param name="id">Some sort of Id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetValues(string id)
        {
            using (CosmosClient dbClient = await this.GetDatatbaseClient())
            {
                await dbClient.CreateDatabaseIfNotExistsAsync("ClaimsService");
            }

            return $"Id value from URL: {id}";
        }
    }
}