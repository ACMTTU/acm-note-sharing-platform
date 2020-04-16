
namespace CatalogApplication.Models

{
    public class Tag
    {
        // Unique ID of the tag, formed by combining name + noteId + userId
        public string id { get; set; }
        // Tag name, will be normalized to all lowercase and hyphen delimeted
        public string name { get; set; }

        // The note ID associated with this tag
        public string noteId { get; set; }
        // To check whether the tag is associated with a certain user
        public string userId { get; set; }
        // Whether the tag should be hidden from the user
        public bool hidden { get; set; }
        // Whether the tag was automatically added by the catalog system
        public bool isSystem { get; set; }
    }
}