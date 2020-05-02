using System;
using System.Collections.Generic;

namespace ClassApplication.Models
{
    public class Classroom
    {
        public string classID { get; set; }                    //From the user
        public string Name { get; set; }                    //From the user
        public string Description { get; set; }             //From the user
        public HashSet<string> Notes { get; set; }        //id of other collection - Notes
        public HashSet<string> Filters { get; set; }    //changed from Tag //id of other collection - Catalog
        public HashSet<string> Students { get; set; }  //id of other collection - Students


    }
}
