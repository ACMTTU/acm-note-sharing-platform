using System;
using System.Collections.Generic;

namespace ClassApplication.Models
{
    public class Classroom
    {
        public int classID { get; set; }                    //From the user
        public string Name { get; set; }                    //From the user
        public string Description { get; set; }             //From the user
        public IEnumerable<Note> Notes { get; set; }        //Pointer to other collection - Notes
        public IEnumerable<string> Filters { get; set; }    //changed from Tag //Pointer to other collection - Catalog
        public IEnumerable<Student> Students { get; set; }  //Pointer to other collection - Students

    }
}