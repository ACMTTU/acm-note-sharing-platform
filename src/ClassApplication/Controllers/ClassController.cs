using System;
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
            throw new NotImplementedException();
        }
    }
}