# Overview
This repo contains the source code for the AccountSystem assignment. It is all written in .net8 and C#. 

# Code structure
The solution contains 3 projects. 
- AccountService: the actual service to start who can handle various Account events. 
- AccountServiceTest: the (unit) tests for the AccountService project.
- DemoRunner: a simple console application to demonstrate the AccountService.

## AccountService
The account service is designed to run in an infinite loop and listen for incoming events.
When events are added it will process them sequentially.

### Project structure and design decisions 
- EventMetadata: contains the metadata for the events. The events are identical in terms of payload, however they are separated to differentiate between them. They derive from an event base class for further extensibility.
- EventHandlers: the various event handlers for the events. Separates the logic for each event. Including a factory to be injected into the AccountService, in order to easily achieve the appropriate handler. The handlers are designed with the template method design pattern, as certain steps in the event handling is default across all.
- Models: simple data transfer objects. 
- Repository: A simple thread-safe in memory repository to store and mutate the accounts. 
- RetryPolicy: Contains logic to compute the next retry of a failed event.
- AccountService.cs: The main service to start. It will listen for incoming events and process them sequentially. The various dependencies are injected into the constructor. The service can be started/stopped. And events are manually added to the service.

## AccountServiceTest
The test are written with XUnit and Moq. The tests are written for the AccountService and the EventHandlers. The test are primarily written in terms of happy path, and code coverage.
The structure of the tests are similar to the structure of the AccountService project.

## DemoRunner
Simple console application to demonstrate the AccountService. It will start two concurrent AccountServices and feed them their events. A random delay has been introduced for simulation/observability purpose. 

# How to run
Run the DemoRunner project and observe the logs. 