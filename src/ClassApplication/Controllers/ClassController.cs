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
        /*
                public async Task<ActionResult<string>> GetClassroom(string id)
                {
                    //to check if i am pushing it right
                    //throw new NotImplementedException();
         }
         */
        public async Task<ActionResult<string>> GetClassroomByID(string id)
        {


            string Text = " SELECT * FROM c WHERE c.noteId =@id";
            QueryDefinition query = new QueryDefinition(Text).WithParameter("@Id", id);

            FeedIterator<Classroom> iterator = classesContainer.ReadItemAsync<Classroom>(query);

            List<Classroom> classroomId = new List<Classroom>(classId);

            //this is an example to add class
            classroomId.Add("CS 3364"); //example
            classroomId.Add(new (ClassController) { GetClassroom = "CS 3375" });
            var classId = id;

            while (iterator.HasMoreResults)
            {
                FeedResponse<Classroom> result = await iterator.ReadNextAsync();
                foreach (Classroom classId in result)
                {
                    classroomId.Add(classId);
                }
            }
            //Returns data from a classroom from a given classroom ID
            return classroomId;


            if (classroomId == null)
            {
                Console.WriteLine("No any Classes. Click '+' and type new class Id to create one.")
                // availableClassroom = classroomList;
            }
            else
            {
                Console.WriteLine("The total number of class room is of different class is: " + classroomId.count +
                "Please select your intended class.");
                //display the class room with respective classId
                var classId = classroomId.classId;
            }
        }


        public async Task<ActionResult<string>> GetClassroomByName(string className)
        {


            string Text = " SELECT * FROM c WHERE c.name= @name =@className";
            QueryDefinition query = new QueryDefinition(Text).WithParameter("@className", className);

            FeedIterator<Classroom> iterator = classesContainer.ReadItemAsync<Classroom>(query);

            List<string> classroomName = new List<string>();

            //this is an example to add class
            classroomName.Add("Computer Architecture"); //example
            classroomName.Add(new (ClassController) { GetClassroom = "Software Engineering" });
            var classId = id;

            while (iterator.HasMoreResults)
            {
                FeedResponse<Classroom> result = await iterator.ReadNextAsync();
                foreach (Classroom className in result)
                {
                    classroomName.Add(className);
                }
            }
            //Returns data from a classroom from a given classroom ID
            return classroomName;

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

        /*        public async Task<ActionResult<string>> DeleteClass(string classId)
                {
                    throw new NotImplementedException();
                }
        */

        public async Task<ActionResult<string> DeleteClass(string classId)
            {

            //here, it gets the list of class id's in the classId
                List<Classroom> id = GetClassroomByID(classId).Result;

                if(!id.Any())
                return EmptyList("classId does not exist"); //here i am confused where to return if there is no class id

        //find a specific class with the parameter classID
        Classroom getlass = id.Find(classroom => classroom.classId == classId);

                if(getClass != null)
                {
                    await classesContainer.DeleteItemAsync<Classroom>(getClass.id, new PartitionKey(getClass.classId));
                    return Deleted("Class successfully deleted");

    }
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