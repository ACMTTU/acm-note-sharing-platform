using System;
using System.Collections.Generic;

namespace ClassApplication.Models
{
    public class Classroom
    {
        public string classID { get; set; }                    //From the user
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
        public bool AddNote(string noteID)
        {

            Notes.Add(noteID);

            // return true if no error encountered
            return true;

        }

        public void setDescription(string newDescription)
        {
            this.Description = newDescription;
        }
    }
}
