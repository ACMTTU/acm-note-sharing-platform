namespace CatalogApplication.Models
{
    public class CatalogData
    {
        public string id { get; set; }
        public string name { get; set; }
        public int color { get; set; }
        public int noteId { get; set; }
        public bool hidden { get; set; }
        public bool isSystem { get; set; }
        public string category { get; set; }
    }
}