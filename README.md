# Fundalyzer

# Instructions
Run the `Api` project. A swagger UI page should open where you can perform 2 get requests. 
You have to wait for the requests to work because of the BackgroundService that is pulling data from Funda in the meanwhile.

# Justification
For this project I used .NET 7 and C# 11 (because: new). I acknowledge this .NET version is not LTS.

The design of the code is quite elaborate, but it showcases how I would set up an application using a DDD approach.

## Api
This API of this solution is completely not RESTful or according to any standard. It is just a way to retrieve data from the application.

## Funda Api
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


## Rate limiting
The background job polls the Funda API once a second (is configurable).
Also included in the FundaHttpClient is a `RateLimitingStrategy` which determines how the application should handle when an HTTP status code of `TooManyRequests` is received from Funda.

## Magic enums
Magic enums have been used twice throughout my code: https://github.com/Code-Chops/MagicEnums.

## Tests
Many more unit and integration tests can be added. For example, the *.http files in Api could be placed in an integration test.