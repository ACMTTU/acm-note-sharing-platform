using System;

namespace ExampleAPI.Services {
    public class FooService {
        public FooService(ConnectionService connectionService) {
            Console.WriteLine(connectionService.serviceStorageContainer.Name);
            Console.WriteLine(connectionService.serviceDatabaseContainer.Id);
        }
    }
}