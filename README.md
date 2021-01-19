# Online Chat Application
This is online chat application with Angular, ASP.NET Core, SignalR and SqlServer following the principles of Onion Architecture. It has the following functionalities </br> 
 * As a new user you can resgister with your email address, first name and last name </br>
 * Registered user can login with his email address. </br>
 * LoggedIn user has dashboard and from where he can see list users and he can chat with any one from the list. Chat always happen between two users. </br>
 * User can see chat history.</br>
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
* Bootstrap

## Getting Started

1. Install the latest [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Install the latest [git](https://git-scm.com/downloads)
4. Run `git clone https://github.com/Ehasaniceiu04/simple-chat.git` to clone this repository
5. Navigate to `simple-chat-ui` and run `npm install ` to install npm packages for the front end (Angular)
6. run `npm start` to launch the front end (Angular)

## Database Configuration

The Application uses data-store in SQL Server.

Update the **SimpleChatConnectionString** connection string within **simple-chat-api/src/Ehasan.SimpleChat.API/appsettings.json** , so that application can point to a valid SQL Server instance. 

```json
  "ConnectionStrings": {
    "SimpleChatConnectionString": "Server=ehasan-dbms; Database=SimpleChatDb; Trusted_Connection=True; MultipleActiveResultSets=True;"
  },
```

When you run **update-database** command, the migrations will be applied and the database will be automatically created.
