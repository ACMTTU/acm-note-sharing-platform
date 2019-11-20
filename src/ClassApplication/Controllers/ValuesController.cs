﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.ClassApplication.Services;
using ClassApplication.Models;

namespace ACMTTU.NoteSharing.Platform.ClassApplication.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : PlatformBaseController
    {

        private Container classesContainer;
        public ClassController(IHttpClientFactory factory, DatabaseService databaseService) : base(factory)
        {
            this.classesContainer = databaseService.classroomsContainer;
        }

        /// <summary>
        /// 
        /// Creates a classroom in the database from a given Classroom object
        /// 
        /// </summary>
        /// <param name="classroom">Has a ClassId, Name, and Description</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateClassroom(Classroom classroom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// Returns data from a classroom from a given classroom ID
        ///
        /// </summary>
        /// <param name="id">classroom id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("GetClassByID/{id}")]
        public async Task<ActionResult<string>> GetClassroom(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// Finds a classroom by the classrooms name
        ///
        /// </summary>
        /// <param name="className">classroom name</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("GetClassByName/{className}")]
        public async Task<ActionResult<string>> QueryClassByName(string className)
        {
            throw new NotImplementedException();
        }

    }
}