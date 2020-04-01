using System;
using System.Collections.Generic;

namespace ClassApplication.Models
{
    public class Classroom
    {
        public int classID { get; set; }                    //From the user
        public string Name { get; set; }                    //From the user
        public string Description { get; set; }             //From the user
        public IEnumerable<string> Notes { get; set; }        //id of other collection - Notes
        public IEnumerable<string> Filters { get; set; }    //changed from Tag //id of other collection - Catalog
        public IEnumerable<string> Students { get; set; }  //id of other collection - Students

        public Classroom(string id, string name, string description /* have to add in collection*/)
        {
            /*
                initalize instance...
            */
        }

    }
}
