# Platform Base Service

I have been getting some questions about how to actually connect to the database.

PLEASE DO NOT DO IT MANUALLY.

There is a lot of infrastructure work that was done to make sure that we connect to the correct database
depending on what environment we are running your service (development/staging/production)

Instead, please inherit the `PlatformBaseService` and register it with ASPNET Core's Dependency Injection.
Take a look at the sample application in the `Examples` directory