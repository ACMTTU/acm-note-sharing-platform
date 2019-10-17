using System;
namespace NotesApplication
{
    ///<summary>
    /// Represents a note in the database.
    ///
    /// This class provides methods that allow easy manipulation of a Note in the database.
    ///</summary>
    public class Note
    {
        /// <summary>
        /// Unique ID of the note
        /// </summary>
        internal string id { get; } //We should not allow updating of IDs because it could mess with the database

        /// <summary>
        /// The name of the note. (Similar to a title)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The files attached to the note
        /// </summary>
        public string[] Notes { get; set; } //Wasn't this suppose to be a HashMap?

        /// <summary>
        /// The UTC datetime that the note was created
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// The last time the note was modified specified by a UTC datetime
        /// </summary>
        /// <value></value>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Creates a new Note
        /// </summary>
        /// <param name="name">The name of the new note</param>
        /// <param name="notes">The files attached to the new note</param>
        public Note(string name, string[] notes)
        {
            id = "-- NEEDS TO BE GENERATED --"; // ID should be generated not given
            Name = name;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        ///<summary>
        ///Returns a Note object that represents the note in the database with the specified ID. 
        ///The returned Note object can be used to manipulate the note in the database via its methods.
        ///</summary>
        ///<param name="id">The id of the note in the database.</param>
        ///<return>A new Note object that represents</return>
        public static Note FromDatabase(string id)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///Returns true if a note exists in the database with the given ID.
        ///</summary>
        ///<param name="id">The id of the note in the database.</param>
        public static bool Exists(string id)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///Creates a new note in the database with the specified ID, then returns a new Note object representing the newly created database note.
        ///</summary>
        ///<param name="id">The id of the note in the database.</param>
        public static Note ToDatabase(string id) //We need to figure out exactly *how* we are going to do this. This method may not be needed.
        {
            throw new System.NotImplementedException();
        }
    }
}