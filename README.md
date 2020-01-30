# Chambers-DocumentService
Document management solution (test)

## :bookmark_tabs: About
The aim of this project is to demonstrate what I believe to be a good approach to building a modern API solution using DDD and Clean Architecture. Sticking closely to SOLID principals and ensuring proper use of IoC to ensure all aspects of the system can be tested.
* Implemented DDD pattern
* Unit tests written for controllers
* Dependancy Injection implemented
* API following REST designs
* Interface written for repository and implementation (InMemoryRepository) using in generic List

## :floppy_disk: How do I use it?

You need some of the fallowing tools:

* Visual Studio 2019
* .Net Core 3.0


Swagger documentation has been enabled and can be accessed locally from https://localhost:44366/swagger/index.html .

## :clipboard: Things to improve

Here's a list of things I feel could have been done to improve on this project if more time was available.

* Functional tests on the API to test the behaviour of the system against the documented user stories (using Specflow or XBehave).
* Proper implementation of the mediatr pattern with the use of a dispatcher.
* Implement Integration tests to ensure Infrastructure is working.
* Publish domain events
* throw custom exceptions at the domain level and bubble up as appropriate http response at the API
* Application level validation (validate commands and query requests)
* FluentValidation at the API layer to validate all request 
* Slim controllers (no real logic in the controller outside DTO mapping for handler execution)
* Return a paged list in the Application services for GetDocuments
* Enable paging on the API through querystring (PageIndex, PageNumber etc)
* Return ProblemDetail objects for exceptions (map from domain exceptions)
* Implement use of Azure Blob storage to store the files
* Use Cosmos DB to persist the domain models.
* Complete the user stories

