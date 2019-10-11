using System;
using System.Collections.Generic;
using System.Linq;

class Classroom
{ 
    public IEnumerable<Student> Students { get; set; }
    public String Name { get; set; }
    public IEnumerable<String> Tags { get; set; }
    public IEnumerable<Note> Notes { get; set; }

    // public Classroom() {
    //     Students = new Student[];     
    //     Name = "";
    //     Tags = new String[];     
    //     Notes = new Note[];     
    // }

    // public Classroom(String name) {
    //     Students = new Student[];     
    //     Name = name;
    //     Tags = new String[];     
    //     Notes = new Note[];     
    // }

    public IEnumerable<Note> sortNotesByRating() {
        return Notes.OrderBy(x => x.Rating);
    }
}



class Student {
    public String Name { get; set; }
    public String Year { get; set; }
    public IEnumerable<Note> Notes { get; set; } 
    public IEnumerable<Classroom> Classrooms { get; set; }

    // public Student() {
    // }
}

class Note {
    public Student Author { get; set; }
    public int Rating { get; set; }
    public String Description { get; set; }
    public DateTime CreationDate { get; set; }
}