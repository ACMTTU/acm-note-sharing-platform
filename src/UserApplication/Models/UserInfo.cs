using Newtonsoft.Json;
namespace UserApplication.Models
{
    //Class that defines notable info about user
    public class UserInfo
    {
        //Specifies the specific userprofile
        public string id { get; set; }
        //Name that people will reference the user by
        public string displayName { get; set; }
        //What year the user is in (Freshman, Sophmore, Junior, Senior, Graduate, PHD)
        public string classification { get; set; }
        //What degree the user is pursuing (Ex: Computer Science)
        public string major { get; set; }
        //Image of the profile pic
        public string profilePic { get; set; }
        //Brief description about User
        public string personalText { get; set; }


    }
}