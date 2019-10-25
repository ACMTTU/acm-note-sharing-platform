using System;
namespace NotesApplication
{
    ///<summary>
    /// Represents a note in the database.
    ///
    /// This class provides methods that allow easy manipulation of a Note in the database.
    /// Each Note object that is an instance of this class represents a physical note stored in the database.
    /// Because of this, Note objects <b>must</b> be obtained from the static functions in this class, (FromDatabase, and Create). This helps assure that each created C# Note object represents a note in the database.
    ///</summary>
    public class Note
    {
        /// <summary>
        /// Unique ID of the note
        /// </summary>
        internal string id { get; } //We should not allow updating of IDs because it could mess with the database

        /// <summary>
        /// Unique ID of the user
        /// </summary>
        internal string user_id { get; } //user does not change

        /// <summary>
        /// The name of the note. (Similar to a title)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The files attached to the note. This property is an array of the IDs of all the files that belong to this note. Each ID is a unique identifier for a document in the Azure Blob store.
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
        /// This is a private constructor to be used by the factory methods in this class, (namely, Create(...), and Exists(...)).
        /// This constructor creates a C# Note object. The arguments to this constructor should be the values of the Note in the database, that the resulting object will represent.
        /// </summary>
        /// <param name="id">The ID of the note in the database. This is the Note's primary key.</param>
        /// <param name="name">The name of the note.</param>
        /// <param name="notes">The files attached to the note.</param>
        /// <param name="createdAt">The time at which the note was created.</param>
        /// <param name="lastModified">The time at which the note was last modified.</param>
        private Note(string id, string name, string[] notes, DateTime createdAt, DateTime lastModified)
        {
            this.id = id;
            Name = name;
            Notes = notes;
            CreatedAt = createdAt;
            LastModified = lastModified;
        }

        /// <summary>
        /// <para>
        /// Returns a Note object that represents the note in the database with the specified ID. 
        /// The returned Note object can be used to manipulate the note in the database via the object's methods.
        /// <b>This method will throw an ArgumentException if there is no note in the database with the specified ID.</b>
        /// </para>
        /// <para/>
        /// <para>
        /// More formally, this function throws an ArgumentException if a call to Exists(string), with the same id argument, will have returned false, if called instead of this function.
        /// </para>
        /// 
        /// <para>
        /// Note: This check to see if a note with the specifie id exists in the database, and the retrieval of the database note's data is a single atomic operation with respect to all other database queries.
        /// </para>
        /// </summary>
        /// <param name="id">The id of the note in the database.</param>
        /// <return>A new Note object.</return>
        public static Note FromDatabase(string id)
        {
            // TODO Try to retrieve a note from the database with the id argument. If the database returns no note, throw an ArgumentException. Otherwise, create a Note object and populate it with the properties from the note in the database.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns true if a note exists in the database with the given ID. This operation is atomic with respect to other database querying operations.
        /// </summary>
        /// <param name="id">The id of the note in the database.</param>
        public static bool Exists(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <para>
        /// Creates a new note in the database with the specified ID, then returns a new Note object representing the newly created database note.
        /// </para>
        /// <para>
        /// If a note with the specified id already exists in this database, this function will throw an ArgumentException.
        /// </para>
        /// </summary>
        /// <param name="id">The id of the note in the database.</param>
        public static Note Create(string id) //We need to figure out exactly *how* we are going to do this. This method may not be needed.
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <para>
        /// Updates this Note object to be consistent with the note, in the database, that it represents. If the note in the database has been deleted since the creation of this object, this method will return false.
        /// If this object could not be updated for any other reason, this method throws an exception accordingly.
        /// Otherwise, this method will return true, signifying that this object is now in sync with the note that it represents in the database.
        /// </para>
        /// <para>
        /// Generally, this object should be discarded if a call to this method returns false.
        /// </para>
        /// </summary>
        /// <returns>true, if this method successfully updated this Note object, to the best of this method's ability, or false, if the physical note in the database has been deleted.</returns>
        public bool Update()
        {
            throw new System.NotImplementedException();
        }
    }
}