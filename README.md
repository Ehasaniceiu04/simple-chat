# Online Chat Application
This is online chat application with Angular, ASP.NET Core, SignalR and SqlServer following the principles of Clean Architecture. It has the following functionalities </br> 
 * As a new user you can resgister with your email address, first name and last name </br>
 * Registered user can login with his email address. </br>
 * LoggedIn user has dashboard and from where he can see list users and he can chat with any one from the list. Chat always happen between two users. </br>
 * User can see chat history.</br>
 * User can delete his chat history in one of two ways. He or she can delete chat data for himselft/herself or everyone.</br>
 * Application has the sign out functionality.</br></br>

## Technologies

* ASP.NET Core 3.1
* Entity Framework Core 3.1
* Angular 9
* Signal R
* Sql Server
* Autofac
* Moq
* XUnit
* xunit.runner.visualstudio
* FluentAssertions
* Bootstrap

## Development Environment Ready

1. Install Latest [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Install [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
3. Install the latest [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
4. Install the latest [Node.js LTS](https://nodejs.org/en/)
5. Run `npm install -g @angular/cli` to install latest version of angular CLI
6. Download source code from  `https://github.com/Ehasaniceiu04/simple-chat.git` 

Note: Please walkthrogh this `simple-chat/simple chat development environment preparation guidelines.docx` to know how to prepare your environment and to run this online chat application in details.

## Run Front-End Application (Angular 9)

1. Navigate to the workspace folder, such as `simple-chat-ui`.
2. Open terminal window
3. Run `npm install` to install all dependencies used in application.
4. Run `npm start` to run chat application in browser.
5. Browse `http://localhost:4500` to view chat app in browser

Note: Please walkthrogh this `simple-chat/simple chat development environment preparation guidelines.docx` to know how to prepare your environment and to run this online chat application in details.

## Database Configuration

The Application uses data-store in SQL Server.

Update the **SimpleChatConnectionString** connection string within **simple-chat-api/src/Ehasan.SimpleChat.API/appsettings.json** , so that application can point to a valid SQL Server instance. 

```json
  "ConnectionStrings": {
    "SimpleChatConnectionString": "Server=ehasan-dbms; Database=SimpleChatDb; Trusted_Connection=True; MultipleActiveResultSets=True;"
  },
```

When you run **update-database** command, the migrations will be applied and the database will be automatically created.

## Application Architecture

### Domain (Ehasan.SimpleChat.Core)

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application (Ehasan.SimpleChat.Core)

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a message service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Tests

This layer contains unit tests.

### RestFull API (Ehasan.SimpleChat.API)

This layer is a restfull api based on .Net Core 3.1. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

### Front-End (Anuglar 9)

Front-end is a single page application based on angular 9. This only communicates with restfull api layer to store or retrieve data.
