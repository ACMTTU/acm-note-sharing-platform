namespace ClassApplication.Models
{
    public class Classroom
    {
        public int classID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<String> Filters { get; set; } //changed from Tags
        public IEnumerable<Student> Students { get; set; }

    }
}