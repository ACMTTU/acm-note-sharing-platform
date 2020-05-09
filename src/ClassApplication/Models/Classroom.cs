using System;
using System.Collections.Generic;

namespace ClassApplication.Models
{
    public class Classroom
    {
        public string id { get; }
        public string ownerID { get; set; }
        public string Name { get; set; }                    //From the user
        public string Description { get; set; }             //From the user
        public HashSet<string> Notes { get; set; }        //id of other collection - Notes
        public HashSet<string> Filters { get; set; }    //changed from Tag //id of other collection - Catalog
        public HashSet<string> Students { get; set; }  //id of other collection - Students

        /// <summary>
        ///     Classroom Constructor; Required data to create a classroom
        /// </summary>
        /// <param name="id">unique id for the classroom</param>
        /// <param name="ownerID">user id of the person who had full admin privileges</param>
        /// <param name="name">the name of the classroom</param>
        /// <param name="description">a description of the classroom</param>
        public Classroom(string id, string ownerID, string name, string description)
        {
            this.id = id;
            this.ownerID = ownerID;
            this.Name = name;
            this.Description = description;

            this.Notes = new HashSet<string>();
            this.Filters = new HashSet<string>();
            this.Students = new HashSet<string>();
        }

        /// <summary>
        ///     Changes the name of the classroom
        /// </summary>
        /// <param name="newName">the new name of the classroom</param>
        public void setName(string newName)
        {
            this.Name = newName;
        }

        /// <summary>
        ///     Adds note to this classroom's collection of notes
        /// </summary>
        /// <param name="noteID">The id of the note object to be added </param>
        public void AddNote(string noteID)
        {
            Notes.Add(noteID);
        }

        /// <summary>
        ///     Changes the description of the classroom
        /// </summary>
        /// <param name="newDescription"></param>
        public void setDescription(string newDescription)
        {
            this.Description = newDescription;
        }

        /// <summary>
        ///     Add user to classroom
        /// </summary>
        /// <param name="addId"></param>
        public void AddStudent(String addId)
        {
            Students.Add(addId);
        }

        /// <summary>
        /// Removes user from the classroom
        /// </summary>
        /// <param name="removeId">User ID of the user being removed</param>
        public void RemoveStudent(string removeId)
        {
            Students.Remove(removeId);
        }

        /// <summary>
        ///     Checks if a user is contained in a class
        /// </summary>
        /// <param name="userId">the id of the user being checked</param>
        /// <returns>if the user is in the classroom</returns>
        public bool ContainsStudent(string userId) { return Students.Contains(userId); }

        /// <summary>
        ///     Checks if a user is the owner of a class
        /// </summary>
        /// <param name="userId">the id of the user being checked</param>
        /// <returns>if the user is in the classroom</returns>
        public bool StudentIsOwner(string userId) { return ownerID.Equals(userId); }

    }
}
