# Hello and welcome to my sample repo for a Cinema bookings web application using MVC.

## Getting started

I am using Visual Studio 2022 with .NET 6 to create this web app.You need to create a new ASP.NET MVC project using C#.
Then you have to install (EF=Entity Framework) EF Core , EF Tools and EF SQL Server through the NuGet Package Manager of Visual Studio.
Also you have to setup your application.json file in the project.I included it in .gitignore in this project because it contains sensitive info.
Create one first on the root of your solution.Then you must set it up like this:

    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
            "{Your_Connection_Name}": "Server={Your_Server};Database={Your_DB_Name};Trusted_Connection=True;"
        }
    }

Exclude the hyphens on the above example.The hyphens highlight your custom values for the connection string.
