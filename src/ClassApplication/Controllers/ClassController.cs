﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using Microsoft.Azure.Cosmos;
using ACMTTU.NoteSharing.Platform.ClassApplication.Services;
using ClassApplication.Models;
using System.Collections.Generic;

namespace ACMTTU.NoteSharing.Platform.ClassApplication.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : PlatformBaseController
    {
        private Container classesContainer;
        private PartitionKey partitionKey = new PartitionKey("classroom");

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
        /// <param name="classId">classroom id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("GetClassByID/{id}")]
        public async Task<Classroom> GetClassroom(string classId)
        {

            // get classroom
            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            // are we supposed to return a Task<ActionResult<string>>? In NotesController, it returns Task<Note>
            return classroom;

        }

        /// <summary>
        /// 
        /// Finds a classroom by the classrooms name
        ///
        /// </summary>
        /// <param name="className">classroom name</param>
        /// <returns>A list containing a classIDs determined by the parameter</returns>
        [HttpGet("GetClassByName/{className}")]
        public async Task<List<string>> QueryClassByName(string className)
        {
            string sqlQueryStatement = "SELECT * FROM c WHERE c.Name = @className";
            QueryDefinition query = new QueryDefinition(sqlQueryStatement).WithParameter("@className", className);
            FeedIterator<Classroom> queryIterator = classesContainer.GetItemQueryIterator<Classroom>(query);

            List<string> result = new List<string>();

            while (queryIterator.HasMoreResults)
            {
                FeedResponse<Classroom> resultSet = await queryIterator.ReadNextAsync();

                foreach (Classroom classroom in resultSet)
                {
                    result.Add(classroom.classID);
                }

            }

            return result;

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

            // get classroom
            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            // add note to classroom's set of notes
            //      the boolean stores whether the function was successful, in case we implement error-checking
            bool result = classroom.AddNote(notesId);

            // return okay; we can use the boolean to return a different result if there's an error encountered
            return Ok();

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
        ///<param name="userId">Id of the user making update request</param>
        [HttpPut("{classId}")]
        public async Task<ActionResult<string>> UpdateClass(string classId, string className, string classDescription, string userId)
        {
            Classroom updatingClass;
            updatingClass = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            if (updatingClass.ownerID == userId)
            {
                updatingClass.setName(className);
                updatingClass.setDescription(classDescription);
                await classesContainer.ReplaceItemAsync<Classroom>(updatingClass, classId);

                return Ok();
            }

            else
                return BadRequest();
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

            // NEED TO FIGURE OUT HOW TO QUERY NOSQL DATABASES
            throw new NotImplementedException();

            // // get classroom
            // Classroom classroom;
            // classroom = await _classesContainer.ReadItemAsync<Classroom>(classId, _partKey);

            // // create query with which to search the database
            // //      by putting the note name inside % %, we're looking for any strings that contain the given name
            // string loc = "Notes"; // ????
            // string query = "SELECT * FROM " + loc + " WHERE name LIKE %" + noteName + "%";

            // // create query request options, which will contain partition key
            // QueryRequestOptions qro = new QueryRequestOptions();
            // qro.PartitionKey = _partKey;

            // // create feed iterator into which to store the result of the query
            // FeedIterator<string> queryIterator = _classesContainer.GetItemQueryIterator<string>(query, null, qro);

            // // how do we return the resultant?????

            // // by default, assume that note is not found 
            // return NotFound();

        }
    }
}