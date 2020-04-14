namespace CatalogApplication.Models
{
    public class Rating
    {

        public string id { get; set; } // noteId.userId

        // The note ID associated with this rating
        public string noteId { get; set; }
        // The user ID associated with this rating
        public string userId { get; set; }

        // The int value of the rating
        public int rating { get; set; }
    }
}