

schema:

    NoSQL:

    Database: "NotesAplication"
        - Collection: "Notes"
            - Database{
                documentKey: <string>
                locationOfBlob: HashTable <key: int, value: location>
                createdAt: <string>
                noteName: <string>
                editedAt: <string>
            }


    Azure Blob:
        - image