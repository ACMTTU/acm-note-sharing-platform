namespace CatalogApplication.Models

{
    public class TagDto
    {
        // Tag name, will be normalized to all lowercase and hyphen delimeted
        public string name { get; set; }

        // The note ID associated with this tag
        public string noteId { get; set; }
        // To check whether the tag is associated with a certain user
        public string userId { get; set; }
        // Whether the tag should be hidden from the user
        public bool hidden { get; set; }


    }
}