# Model for Claims

Classroom 
    Add User
    Leave
    Create Note
    Block
    Delete


Notes
    Watch
    Edit
    Delete



NoSql Schema: 

{
    "Classroom" : [
        {
            "Owners" : [
                {
                    "id" : "Classroom id",
                    "userId" : "User id",
                    "claim" : [
                        {
                            "addUser" : "True",
                            "leave" : "True",
                            "createNote" : "True",
                            "block" : "True",
                            "delete" : "True"
                        }
                    ]   
                }
            ],

            "Members" : [
                {
                    "id" : "Classroom id",
                    "userId" : "User id", 
                    "claim" : [
                        {
                            "addUser" : "True",
                            "leave" : "True",
                            "createNote" : "True",
                            "block" : "False",
                            "delete" : "False"
                        }
                    ]
                }
            ]
            
        }
    ],
    
    "Notes" : [
        {
            "Owners" : [
                {
                    "id" : "Note id",
                    "userId" : "User id", 
                    "claim" : [
                        {
                            "watch" : "True",
                            "edit" : "True",
                            "delete" : "True"
                        }
                    ]
                }
            ],

            "Editors" : [
                {
                    "id" : "Note id",
                    "userId" : "User id", 
                    "claim" : [
                        {
                            "watch" : "True",
                            "edit" : "True",
                            "delete" : "False"
                        }
                    ]
                }
            ],

            "Members" : [
                {
                    "id" : "Note id",
                    "userId" : "User id", 
                    "claim" : [
                        {
                            "watch" : "True",
                            "edit" : "False",
                            "delete" : "False"
                        }
                    ]
                }
            ]
        }
    ]

}