# Fundalyzer

# Instructions
Run the docker image or run the `Api` project directly.

# Justification
For this project I used .NET 7 and C# 11 (because: new). I acknowledge this .NET version is not LTS.
I also used JSON for the API as it's 

## Containers
I used a Dockerfile in order to run the API easily using the new build-in container support for .NET:
https://devblogs.microsoft.com/dotnet/announcing-builtin-container-support-for-the-dotnet-sdk/.

## Api
The API is a REST / JSON API, I:
- Did not use any authentication, because it was not needed for the assignment. It can always be added later on.
- Did not use minimal APIs as I think controllers are way better to segregate the logic of your endpoints.
- Am using JSON as the protocol of the API, because JSON is ubiquitous and therefore the tooling is good.
- Used HTTPS because I think that HTTP really shouldn't be used anymore for APIs.
- I used Swashbuckle in order to expose objects as JSON endpoints. This way you also have a simple UI for the API and you can easily create a client.

### Funda Api
I checked if Funda has an OpenAPI / Swagger definition, but unfortunately it doesn't
It would have been perfect because you could easily generate a client with NSwag / Swashbuckle.
I preferred to use JSON instead of XML, because XML is older and I prefer the readability of JSON and I prefer its tooling.

## Background job
I wanted to use Quartz as a job schedule. Quartz.NET is an advanced scheduler with a built-in mechanism to persist scheduled jobs, and much more.
But it is a bit overkill for this application and therefore I used a simpler approach: 
When the application starts, a background job starts which uses the new .NET `PeriodicTimer`.
The usage of a background job has a disadvantage:
when you need to scale up the performance by running more instances of the API, multiple background task will run simultaneously.

## Domain modeling
I modelled my domain with a DDD onion design using my own DDD modeling package: https://github.com/Code-Chops/DomainModeling. 

### Value objects
Throughout my code I use value objects which have structural equality.
These are generated using a source generator, so a lot of boiler-plate does not have to be written,
e.g.: structure equality, casts, constructors, factories, and validation.

### Entities / Identities
The 2 entities in the domain model `Agency` and `Estate` both have a strongly typed primary ID that is auto-generated using source generators.

## Contracts
I used my package https://github.com/Code-Chops/Contracts (which is in beta) for easily creating contracts, adapters and using JSON serializers.

## Validation
I am not satisfied yet with the design of the error codes (which you see at the value objects). And especially with the usage of `nameof()`.
These error codes ensure that error responses to external sources are unified and can eventually be parsed and localized to the end user.
In my spare time I am creating a way to unify these error codes in a single MagicEnum, see my MagicEnum package: https://github.com/Code-Chops/MagicEnums.
These error messages (and their parameters) can be localized in a Blazor client using my package https://github.com/Code-Chops/LightResources.

## Global usings
Global usings can be very handy when it comes to using versioning in contracts for external sources and you want to easily change using a specific version.
See Funda.Infrastructure.Api.Properties.GlobalUsings.

## Rate limiting
The background job polls the Funda API once a second (is configurable).
Also included in the FundaHttpClient is a `RateLimitingStrategy` which determines how the application should handle when an HTTP status code of `TooManyRequests` is received from Funda.

# Magic enums
...

# Unfinished things
I didn't finish everything but this is to show how I would set up a basic API and background service.

## Tests
Many more unit and integration tests can be added. For example, the *.http files in Api could be placed in an integration test.

## Validation messages
Somehow the validation exceptions didn't get caught and are not presented nicely to the user. I didn't have to look into why that happens.
