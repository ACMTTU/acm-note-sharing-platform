using System;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Services {
    public class NotesMetaDataService {
        private ConnectionService connectionService;

        /// <summary>
        /// Consumes the Connection Service so that I can actually use the database and
        /// the storage blob client
        /// </summary>
        /// <param name="connectionService">Service that connects to the database</param>
        public NotesMetaDataService (ConnectionService connectionService) {
            this.connectionService = connectionService;
        }

        /// <summary>
        /// I can do anything here!!!!
        /// </summary>
        public void DoStuff () {
            Console.WriteLine (this.connectionService.notesContainer.Id);
            Console.WriteLine (this.connectionService.notesBlobContainer.Name);
        }
    }
}