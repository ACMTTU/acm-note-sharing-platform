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

        public void setName(string newName)
        {
            this.Name = newName;
        }

        /// <summary>
        /// Adds note to this classroom's collection of notes
        /// </summary>
        /// <param name="noteID">The id of the note object to be added </param>
        public void AddNote(string noteID)
        {

            Notes.Add(noteID);

        }

        public void setDescription(string newDescription)
        {
            this.Description = newDescription;
        }

        /// <summary>
        /// Removes user from the classroom
        /// </summary>
        /// <param name="removeId">User ID of the user being removed</param>
        public void RemoveStudent(string removeId)
        {

            Students.Remove(removeId);

        }

        public bool ContainsStudent(string userId) { return Students.Contains(userId); }
        public bool StudentIsOwner(string userId) { return ownerID.Equals(userId); }

    }
}
