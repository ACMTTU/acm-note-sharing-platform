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
    [Route("api/classroom")]
    [ApiController]
    public class ClassController : PlatformBaseController
    {
        private Container classesContainer;
        private PartitionKey partitionKey = new PartitionKey("classroom");

        public ClassController(IHttpClientFactory factory, DBService databaseService) : base(factory)
        {

            // IF YOU'RE READING THIS: the _controller field should be PRIVATE,
            //      but this programmer didn't have the clearance to abstract
            //      the database functions in this controller.
            //      please make this field PRIVATE upon abstracting the database
            //      service from this class.
            this.classesContainer = databaseService._container;
        }

        /// <summary>
        /// 
        /// Creates a classroom in the database
        /// 
        /// </summary>
        /// <param name="ownerId">ID of the user creating the class</param>
        /// <param name="classroomName">Name of the classroom being created</param>
        /// <param name="description">String to set the new classroom description to (Optional)</param>
        [HttpPost("CreateClass")]
        public async Task<ActionResult<string>> CreateClassroom(string ownerId, string classroomName, string description = "")
        {
            string classId = Guid.NewGuid().ToString();
            Classroom classroom = new Classroom(classId, ownerId, classroomName, description);
            classroom.AddStudent(ownerId);

            await classesContainer.CreateItemAsync<Classroom>(classroom, new PartitionKey(classId));

            return Ok();
        }



        /// <summary>
        /// 
        /// Returns data from a classroom from a given classroom ID
        ///
        /// </summary>
        /// <param name="classId">classroom id</param>
        /// <returns>An array containing a value determined by the parameter</returns>
        [HttpGet("GetClassByID/{classId}")]
        public async Task<Classroom> GetClassroom(string classId)
        {
            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, new PartitionKey(classId));

            return classroom;
        }

        /// <summary>
        /// 
        /// Finds a classroom by the classrooms name
        ///
        /// </summary>
        /// <param name="className">classroom name</param>
        /// <returns>A list containing a classIDs determined by the parameter</returns>
        [HttpGet("QueryClassByName/{className}")]
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
                    result.Add(classroom.id);
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
        [HttpPut("AddNoteToClass/{classId}/notes/{notesId}")]
        public async Task<ActionResult<string>> AddNoteToClass(string classId, string notesId)
        {

            // get classroom
            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            // add note to classroom's set of notes
            //      the boolean stores whether the function was successful, in case we implement error-checking
            classroom.AddNote(notesId);

            await classesContainer.ReplaceItemAsync<Classroom>(classroom, classId);

            // return okay; we can use the boolean to return a different result if there's an error encountered
            return Ok();

        }

        /// <summary>
        /// This call is used to remove a Note from a Classroom
        /// using the Note ID generated by the Notes Application
        /// </summary>
        /// <param name="classId">Id of the classroom</param>
        /// <param name="notesId">Id of the note being added</param>
        [HttpDelete("RemoveNoteFromClass/{classId}/notes/{notesId}")]
        public async Task<ActionResult<string>> RemoveNoteFromClass(string classId, string notesId)
        {
            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            classroom.RemoveNote(notesId);

            await classesContainer.ReplaceItemAsync<Classroom>(classroom, classId);

            return Ok();
        }

        /// <summary>
        ///This call is used to delete a classroom
        ///by classID
        ///</summary>
        ///<param name="classId">ID of the classroom </param>
        ///<param name="userId">id of the user making the request</param> 
        [HttpDelete("DeleteClassroom/{classId}")]
        public async Task<ActionResult<string>> DeleteClassroom(string classId, string userId)
        {

            Classroom classToBeDeleted = await GetClassroom(classId);

            //Only the owner has permission to delete the classroom
            if (classToBeDeleted.ownerID == userId)
            {
                await classesContainer.DeleteItemAsync<Classroom>(classId, partitionKey);
                return Ok();
            }

            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        ///This call is used to update the name and/or description of a class
        ///by classId
        ///</summary>
        ///<param name="classId">Id of the classroom</param>
        ///<param name="className">Name of classroom to set</param>
        ///<param name="classDescription">Description of classroom to set</param>
        ///<param name="userId">Id of the user making update request</param>
        [HttpPut("UpdateClassInfo/{classId}/{userId}/{className}/{classDescription}")]
        public async Task<ActionResult<string>> UpdateClassInfo(string classId, string className, string classDescription, string userId)
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

        /// <summary>
        ///This call is used to update the name and/or description of a class
        ///by classId
        ///</summary>
        ///<param name="classId">Id of the classroom</param>
        ///<param name="actorId">Id of user invoking</param>
        ///<param name="removeId">Id of the user getting removed</param> 
        [HttpDelete("RemoveStudent/{classId}/{userId}/{className}/{classDescription}")]
        public async Task<ActionResult<string>> RemoveStudent(string classId, string actorId, string removeId)
        {

            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            // if either the actor or the user to be removed are not part of this classroom,
            //      this is a bad request
            if (!classroom.ContainsStudent(actorId) || !classroom.ContainsStudent(removeId))
                return BadRequest();

            // if the user being removed is the Owner
            if (classroom.StudentIsOwner(removeId))
            {

                // if the Owner is removing themselves, this is undefined behavior
                if (removeId.Equals(actorId))
                    throw new NotImplementedException();

                // otherwise, a non-Owner cannot remove the Owner, so this is a bad request
                else
                    return BadRequest();

            }

            // at this point, this is a good request so perform the task
            classroom.RemoveStudent(removeId);
            return Ok();

        }

        /// <summary>
        ///This call is used to add student in the classroom
        ///by classId, addId
        ///</summary>
        ///<param name="classId">Id of the classroom</param>
        ///<param name="addId">Id of the student that is going to be added</param>
        [HttpGet("{classId}/{addId}")]
        public async Task<ActionResult<string>> AddStudent(string classId, string addId)
        {
            Classroom classroom;
            classroom = await classesContainer.ReadItemAsync<Classroom>(classId, partitionKey);

            //Since there is no validation for adding student,
            // added student with respect to addId
            classroom.AddStudent(addId);

            await classesContainer.ReplaceItemAsync<Classroom>(classroom, classId);

            return Ok();
        }

    }
}