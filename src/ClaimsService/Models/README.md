# Model for Claims

| Classroom | Type |
 ----------- | ---- 
  id | string
  Classroom Id | string
  User Id | string
  Add User | string 
  Leave | string 
  Create Note | bool 
  Block | bool 
  Delete | bool 
  Add Admin | bool 
  User Status | bool 


 Notes | Type
 ------ | ----
  id | string
  Note Id | string
  User Id | string
  Watch | bool 
  Edit | bool 
  Delete | bool 



## Example NoSql for Classroom

{
    "Classroom" : [
        {
            "Owners" : [
                {
                    "id" : "Claim id",
                    "classroomId" : "Classroom id",
                    "userId" : "User id",
                    "claim" : [
                        {
                            "addUser" : "Bool",
                            "leave" : "Bool",
                            "createNote" : "Bool",
                            "block" : "Bool",
                            "delete" : "Bool",
                            "addAdmin" : "Bool",
                            "userStatus" : "Bool"
                        }
                    ]   
                }
            ]
        }
    ]
}


**classroomId**
The classroom id associated with this specific classroom

**userId**
The id of the associated user

**addUser**
Permission to Add User

**leave**
Permission to leave the classroom

**createNote**
Permission to create a note

**block**
Permission to block a specific user

**delete**
Permission to delete a specific user

**addAdmin**
Permission to add another Admin

**userStatus**
Check whether the user has been blocked or is still in Active status


## Example NoSql for Notes

{
    "Notes" : [
        {
            "Owners" : [
                {
                    "id" : "Claim id",
                    "noteId" : "Note id",
                    "userId" : "User id", 
                    "claim" : [
                        {
                            "watch" : "Bool",
                            "edit" : "Bool",
                            "delete" : "Bool"
                        }
                    ]
                }
            ]
        }
    ]
}

**noteId**
The note id associated with this specific note

**userId**
The id of the associated user

**watch**
Permission to watch the note

**edit**
Permission to edit the note

**delete**
Permission to delete the note
