The following document demonstrates how to use Project Neptune.

## 1. Some background.
Neptune relies on Dependency Injection to achieve [inversion of control](https://en.wikipedia.org/wiki/Inversion_of_control]). The reason IoC is desirable is because external systems can be [mocked for testing](https://duanenewman.net/blog/post/better-unit-testing-with-ioc-di-and-mocking/), and the work for constructing and configuring external systems can be done [externally](https://martinfowler.com/articles/injection.html) (Also known as Single Responsibility Principle).

Neptune relies on [C# generics](https://www.tutorialsteacher.com/csharp/csharp-generics). Generic types are one of the most important programming principles in modern Software Engineering.

## 2. Using Neptune
### 1. Create the service interface
See Garage.cs line 11.
```csharp
interface Inventory : IService { }
```
This service interface declares no methods, or functions. The point of this interface is to tie together the Message Service and the Message itself together in the type system.
### 2. Define the Message Service
See Garage.cs line 12.
```csharp
class InventoryService : MessageService<Inventory>
{
    public InventoryService(IHttpClientFactory clientFactory) : base(clientFactory) { }
}
```
This MessageService subtype can optionally override the work done in the MessageService, however it does not need to. The MessageService defines methods to serialize and send any C# object, deseriailze the response and return it to the caller. I invite you to take a look at the MessageService definition, and ask me any questions you may have on how it works.
### 3. Define the message
See Garage.cs line 18
```csharp
class NewItemRequest : PostRequest<Inventory>
```
The `NewItemRequest` defines the Uri to send the request to, and a useful constructor for building the request. Crucially the `NewItemRequest` contains the information that you want to be transmitted to the running `InventoryService`. In this case the `itemName` field needs to be sent to the `InventoryService`.
### 4. Define the response.
See Garage.cs line 28
```csharp
class NewItemResponse : Response<Inventory, NewItemRequest>
{
    public int count;
    public string itemName;
    public int itemId;
}
```
The `NewItemResponse` must contain all of the fields that the InventoryService returns when posting to this URI. The owner of the `InventoryService` API should work to ensure all messages and responses for the `InventoryService` are accurate.

Note the `NewItemResponse` does not have a constructor. This is because the object will be constructed by the deserialization library, which does not rely on the constructor. The deserialization library will construct the object from the byte stream returned by the HttpServer.
### 5. Putting it all together: Using the service to send a message
See Garage.cs line 38-40
```csharp
class Example
{
    public async Task<int> count(InventoryService service)
    {
        var message = new NewItemRequest("screwdriver");
        var response = await service.PostRequest<NewItemRequest, NewItemResponse>(message);
        return response.count;
    }
}
```
The `InventoryService` dependency will be injected into any module that needs access to the `InventoryService`.

A new message is constructed, and the `PostRequest` on the `InventoryService` base class is called. The type parameters let the serialization library know what type to deserialize into, also allows compiler to infer when a developer has made a mistake and has attempted to send the wrong message type to a service.

The response object is of type `NewItemResponse`, and therefore the fields of `NewItemResponse` are all available to the `PostRequest` caller.

If anything has gone wrong with sending the message, or deserializing the response a runtime error will occur.

## Contact
Contact me at zachlefevre@gmail.com. github.com/zachlefevre, or find me on slack if you have any questions about the use or design of Neptune.