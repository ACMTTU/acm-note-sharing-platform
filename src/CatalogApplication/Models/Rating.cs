namespace CatalogApplication.Models
{
    public class Rating
    {
        // noteId.userId, unique identifier for a rating
        public string id { get; set; } 
        // The note ID associated with this rating
        public string noteId { get; set; }
        // The user ID associated with this rating
        public string userId { get; set; }
        // The int value of the rating
        public int rating { get; set; }