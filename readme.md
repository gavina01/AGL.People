# AGL - Coding Test #

Overall coding test to display the AGL api endpoint as a summary of gender with the names of pets (cats) in alphabetical order.

### Getting Started ###

Clone the repo, open the solution file. By clicking "run" (Docker) you will be able to browse the results on http://localhost

### Prerequisites ###

* Visual Studio 2017
* Docker for Windows (if running on a Windows machine) - Use Linux container setting

No further changes are required to get in running locally. docker-compose project should be set as startup-project.

### Requirements ###

The instructions for the test can be found at http://agl-developer-test.azurewebsites.net/. Based on the instructions the following has been completed

* Call EndPoint http://agl-developer-test.azurewebsites.net/people.json. (Can include something like Polly to include circuit breaker design/improve transient failures)
* Group Data by the Persons Gender who own cats and list the cat names alphabetically.

### How its Built ###

The test has been built by using the SPA React template that is provided with the .Net Core project templates. This includes

* .Net Core 2
* TypeScript with React

The test has been setup with the following settings in appsettings.Development.json file. The appsettings file can be updated to reflect settings per environment (e.g. stage,stub,test). For example if you would like to load different interfaces for another environment these dependencies can be updated in the appsettings file for that environment.

AGL Endpoint Settings

```csharp
"Settings": {
    "AglSettings": {
      "PersonAPIEndPoint": "http://agl-developer-test.azurewebsites.net/people.json"
    },
    "Version": "1.0.0"
  },
```

DI Services

```csharp
"services": [
    {
      "serviceType": "IPeopleService",
      "implementationType": "PeopleService",
      "lifetime": "Scoped"
    },
    {
      "serviceType": "IPeopleRepository",
      "implementationType": "PeopleRepository",
      "lifetime": "Scoped"
    }
```

The settings and the services are loaded on startup.

### Tests ###

Unit tests have been added for the Services and Web project. It covers a few happy paths that can be tested. Further tests will need to be added to complete code coverage.

### Things to improve ###

The following can be added to further that the code is complete

* Logging, by implementing ILogger