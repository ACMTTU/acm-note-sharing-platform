using Newtonsoft.Json;
namespace UserApplication.Models
{
    //Class that defines notable info about user
    public class UserInfo
    {
        //Specifies the specific userprofile
        public string id { get; set; }
        //Name that people will reference the user by
        public string username { get; set; }
        //Used to verify that the user is a [TTU?] student
        public string email { get; set; }
        //What year the user is in (Freshman, Sophmore, Junior, Senior, Graduate, PHD)
        public string classification { get; set; }
        //What degree the user is pursuing (Ex: Computer Science)
        public string major { get; set; }
        
        //toString that returns the fields in a .json format
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}