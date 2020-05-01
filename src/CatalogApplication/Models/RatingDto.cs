namespace CatalogApplication.Models
{
    public class RatingDto
    {
        // The user that owns the rating
        public string userId { get; set; }
        // The note ID associated with this rating
        public string noteId { get; set; }
        // The int value of the rating
        public int rating { get; set; }
    }
}