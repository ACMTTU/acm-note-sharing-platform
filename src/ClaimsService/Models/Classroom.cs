namespace ClaimsService.Models
{
    public class Classroom
    {
        // Claim id
        public string id { get; set; }
        // The classroom id associated with this specific classroom
        public string classroomId { get; set; }

        // The id of the associated user
        public string userId { get; set; }

        // Permission to Add User
        public bool addUser { get; set; }

        // Permission to leave the classroom
        public bool leave { get; set; }

        // Permission to create a note
        public bool createNote { get; set; }

        // Permission to block a specific user
        public bool block { get; set; }

        // Permission to delete a specific user
        public bool delete { get; set; }

        // Permission to add another Admin
        public bool addAdmin { get; set; }

        // Check whether the user has been blocked or is still in Active status
        public bool userStatus { get; set; }


    }
}