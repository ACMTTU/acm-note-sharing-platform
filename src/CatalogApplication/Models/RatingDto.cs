namespace CatalogApplication.Models
{
    public class RatingDto
    {
        // The user that owns the rating
        public string userId { get; set; }
        // The note ID associated with this rating
        public string noteId { get; set; }
        // The running average rating for the note
        public double rating { get; set; }
    }
}