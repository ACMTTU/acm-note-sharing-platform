namespace CatalogApplication.Models
{
    public class Tag
    {
        // Unique ID of the tag, an incremental counter
        public string id { get; set; }
        // Tag name, will be normalized to all lowercase and hyphen delimeted
        public string name { get; set; }
        // Color associated with the tag, defaults to the hash of name as RGB
        public int color { get; set; }
        // The note ID associated with this tag
        public string noteId { get; set; }
        // To check whether the tag is associated with a certain user
        public string userId { get; set; }

        // Whether the tag should be hidden from the user
        public bool hidden { get; set; }
        // Whether the tag was automatically added by the catalog system
        public bool isSystem { get; set; }
        // The category of the tag, i.e. Engineering, Computer Science, Business, etc.
        public string category { get; set; }
    }
}