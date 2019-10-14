namespace ClaimsService.Models
{
    public class Notes
    {
        // Claim id
        public string id { get; set; }
        // The note id associated with this specific note
        public string noteId { get; set; }

        // The id of the associated user
        public string userId { get; set; }

        // Permission to watch the note
        public bool watch { get; set; }

        // Permission to edit the note
        public bool edit { get; set; }

        // Permission to delete the note
        public bool delete { get; set; }

    }
}