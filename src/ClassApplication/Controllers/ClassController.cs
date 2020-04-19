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
        /// Creates a classroom in the database
        /// 
        /// </summary>
        /// <param name="ownerId">ID of the user creating the class</param>
        /// <param name="ownerName">Public name of the user creating the class</param>
        [HttpPost]
        public async Task<ActionResult<string>> CreateClassroom(string ownerId, string ownerName)
        {
            Classroom classroom;
            classroom.classID = Guid.NewGuid();
            classroom.Name = ownerName + "'s classroom";
            classroom.Students.add(ownerId);
            classroom.ownerID = ownerId;

            await classesContainer.AddItemAsync<Classroom>(classroom, classroom.ClassID);

            return Ok();
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

        /// <summary>
        /// This call is used to add a Note to the Classroom
        /// using the Note ID generated by the Notes Applicaton
        /// </summary>
        /// <param name="classId">Id of the classroom</param>
        /// <param name="notesId">Id of the note being added</param>
        [HttpPut("{classId}/notes/{notesId}")]
        public async Task<ActionResult<string>> AddNoteToClass(string classId, string notesId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This call is used to remove a Note from a Classroom
        /// using the Note ID generated by the Notes Application
        /// </summary>
        /// <param name="classId">Id of the classroom</param>
        /// <param name="notesId">Id of the note being added</param>
        [HttpDelete("{classId}/notes/{notesId}")]
        public async Task<ActionResult<string>> RemoveNoteFromClass(string classId, string notesId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///This call is used to delete a classroom
        ///by classID
        ///</summary>
        ///<param name= "classId">ID of the classroom </param>
        [HttpDelete("{classId}")]
        public async Task<ActionResult<string>> DeleteClass(string classId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///This call is used to update the name and/or description of a class
        ///by classId
        ///</summary>
        ///<param name="classId">Id of the classroom</param>
        ///<param name="className">Name of classroom to set</param>
        ///<param name="classDescription">Description of classroom to set</param>
        [HttpPut("{classId}")]
        public async Task<ActionResult<string>> UpdateClass(string classId, string className, string classDescription)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///This call is used to query a list of notes in a class
        ///by classId
        ///</summary>
        ///<param name="classId">Id of the classroom</param>
        ///<param name="noteName">Name of the note to search for</param>
        [HttpGet("{classId}")]
        public async Task<ActionResult<string>> QueryNote(string classId, string noteName)
        {
            throw new NotImplementedException();
        }
    }
}