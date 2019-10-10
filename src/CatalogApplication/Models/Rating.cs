namespace CatalogApplication.Models
{
    public class Rating
    {
        // The note ID associated with this rating
        public string noteId { get; set; }
        // The running average rating for the note
        public double rating { get; set; }
        // The number of ratings total for the note
        public int numRatings { get; set; }
    }
}