How To Run:
- API host application is a self hosted WepAPI application that only requires .NET 4.5.2 run-time. The port number has been hard-coded and service Urls are displayed in the console.

Assumptions:
- As for upcoming bets it wasn't clear that a given unsettled bet can be considered as Risky, Unusual, and ToWinGreaterThan1000 at the same time or not, it defined the risk status as a collection of enums.(Flag enum) 
- The data processing is pull based. Data file is read at backed at once and will be exposed via API.  
- There is no API authentication, authorization , exception handling and error logging 
- Api application looks for a hard-coded relative path in current execution folder to read and process cvs files.

Todo list:
- User interface ASP.NET or WPF MVVM User interface, ( 1 more hour was needed to implement a working UI with a proper project structure and test cases)
- Config file and configuration settings for API
- Exception handling and logging in general
- Error handling and proper cvs parsing 
- API Authorization And Authentication and automatic doc generation
- Async implementation 
- Request response pattern with included well-defined error codes 
- Service classes should be extracted into a new project
- Dependency injection implementation
- Data Caching 
- Event streaming and processing
- Event Pushing,  Publish-Subscribe 
- Implementing rich domain model
