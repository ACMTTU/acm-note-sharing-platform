# Use Case Model For Classroom
![alt text](https://i.imgur.com/jXG3BdY.png "Use Case Diagram for Classroom")
![alt text](https://files.slack.com/files-pri/T0D7KK29F-FP6GUBUE9/image.png "Class diagram for Classroom")


# Classrooms Attributes
```json
Classrooms(
    classID:        int            # Our ID for the classroom
    name:           str            # Name of classroom
    description:    str            # What the class is about
    notes:          list<note>     # Point to other collection's data
    filters:        list<catalog>  # Point to other collection's data
    students:       list<student>  # Point to other collection's data
)
```

## classID
The ID for the classroom. Will be able to use this to keep track of classrooms e.g. a user can use the ID to navigate to the classroom.

## name
Name of classroom (CS1302).

## description
A description of the classroom i.e. explaining what topics the classroom will be covering and what to expect from the page.

## notes
The classroom will contain a list of notes posted by users.

## filters
Filters will be used to navigate through the notes contained in the classroom.

## students
The students subscribed to the classroom.
