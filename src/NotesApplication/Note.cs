namespace NotesApplication
{
    ///<summary>
    /// Represents a note in the database.
    ///
    /// This class provides methods that allow easy manipulation of a Note in the database.
    ///</summary>
    public class Note
    {
        public string ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string[] Notes
        {
            get; set;
        }

        public string creationTime
        {
            get; set;
        }

        private Schema(string id, string name, string[] notes)
        {
            ID = id;
            Name = name;
            Notes = notes;
        }

        ///<summary>
        ///Returns a Note object that represents the note in the database with the specified ID. 
        //The returned Note object can be used to manipulate the note in the database via its methods.
        ///</summary>
        ///<param name="id">The id of the note in the database.</param>
        ///<return>A new Note object that represents </return>
        public static Note GetNoteFromDatabase(string id)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///Returns true if a note exists in the database with the given ID.
        ///</summary>
        ///<param name="id">The id of the note in the database.</param>
        public static bool DoesNoteExist(string id)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///Creates a new note in the database with the specified ID, then returns a new Note object representing the newly created database note.
        ///</summary>
        ///<param name="id">The id of the note in the database.</param>
        public static Note CreateNoteInDatabase(string id)
        {
            throw new System.NotImplementedException();
        }

    }
}