# Hello and welcome to my sample repo for a Cinema bookings web application using MVC.

## Getting started

I am using Visual Studio 2022 with .NET 6 to create this web app.You need to create a new ASP.NET MVC project using C#. <br />
Then you have to install (EF=Entity Framework) EF Core , EF Tools and EF SQL Server through the NuGet Package Manager of Visual Studio. <br />
Also you have to setup your application.json file in the project.I included it in .gitignore in this project because it contains sensitive info. <br />

Create one first on the root of your solution.Then you must set it up like this:\

    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
            "{Your_Connection_Name}": "Server={Your_Server};Database={Your_DB_Name};Trusted_Connection=True;TrustServerCertificate=True"
        }
    }

Exclude the hyphens on the above example.The hyphens highlight your custom values for the connection string. <br />

## EF Core Info

I will be using Entity Framework Core using a code-first approach for the database creation , handling etc. <br />
Whenever you want to create a new model , update a table , create a table etc. , you have to add your changes in the ApplicationDbContext file. <br />
Afterwards , you need to migrate the changes to the database.EF Core achieves that through migrations.Check online for more info. <br />
In our case , after every change we perform to the data models , we have to do the following:

1.Open Tools->Package Manager Console
2.Perform the following command : 'add-migration {your_migration_name}'
3.update-database

The migration name must be somewhat related to the changes you perform.For instance , if you change a model named User , you can have a name such as <br />
'UpdateUserTable'.

## Basic View Setup

I will use the slate theme from https://bootswatch.com/ and the navbar included if you go on Preview->Second navbar and code the code in place of the basic navbar. <br />

## Pop-up messages functionality

I will be using the Sweet Alert 2 from https://sweetalert2.github.io/ for pop-up messages in my application. <br />
Just go over to the above website , find the script source and include it in the header of the \_Layout.cshtml file.
In order to include a Sweet Alert pop-up in a view , you need a code bracket like the following:

    @{
    ...some code here
    <script>
    Swal.fire({
    title: {Title},
    text: {Your Message},
    icon: {Appropriate icon name},
    confirmButtonText: {Button text}
    })
    </script>
    ...rest code here
    }

The values in brackets in the above snippet should be your custom values.Search at the website provided for every provided icon name.
