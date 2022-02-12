# Projects_And_Tasks
Projects_And_Tasks is a Web API for entering project data into a database written in .Net Core 5.0
## Demo
http://188.186.68.82:5000/swagger/index.html
## Functionality
* Ability to create / view / edit / delete information about projects
* Ability to create / view / edit / delete task information
* Ability to add and remove tasks from a project (one project can contain several tasks)
* Ability to view all tasks in the project

## Setting up project
Release for Win-x64 uploaded to GitHub
### From source
0. Install corresponding NuGet package for Entity Framework in the DataAccess project if planning to use other than MS SQL Server database
1. Restore packages by using `dotnet restore`
2. Change database connection string in the `\DataAccess\dataaccess.json`
3. Apply database migration using `dotnet ef database update --project DataAccess`
4. Publish the application for your platform
5. Start published application using `dotnet run` command or on Windows `Projects_And_Tasks.exe`. Optionally specify the listening address and port using `--urls "http://*:5000"` argument
### From release
1. Change database connection string in the `dataaccess.json`
2. Create database by starting application with parameter `--migrate`, for example: `Projects_And_Tasks.exe --migrate`
3. Start application using `dotnet run` command or on Windows `Projects_And_Tasks.exe`. Optionally specify the listening address and port using `--urls "http://*:5000"` argument
## NuGet packages used
DataAccess:
* Microsoft.EntityFrameworkCore 5.0.14
* Microsoft.EntityFrameworkCore.Design 5.0.14
* Microsoft.EntityFrameworkCore.SqlServer 5.0.14
* Microsoft.EntityFrameworkCore.Tools 5.0.14
* Microsoft.Extensions.Configuration 6.0.0
* Microsoft.Extensions.Configuration.Json 6.0.0

Logic:

* AutoMapper 11.0.1

Projects_And_Tasks:

* Swashbuckle.AspNetCore 5.6.3
## Code architecture
The code has three-level architecture: data access, logic, and representation levels.
* The data access level is responsible for the interaction with a database using Entity Framework and LINQ. At this moment MS SQL server is used but you can easily switch to another SQL server by installing a specific NuGet package and changing the default connection string.
* Logic level is responsible for the business logic of the API. At this level, you can define any custom logic that fits your needs. This level is also responsible for setting requirements for the data format used during model validation. Some tests for the basic functionality of this level are present in the `Logic_Tests` project
* Representation level defines API structure and which data and how data sent to a user
## How to add new fileds to the entites
There are two main files that define object structure:
* `\Projects_And_Tasks\DataAccess\Entities\[Base.cs, Project.cs, ProjectTask.cs]` - These three files defines the object structure that is used in the database
* `\Projects_And_Tasks\Logic\Model\[ProjectModel.cs, ProjectTaskModel.cs]` - These files define the structure of the object that is visible to the user and also responsible for defining data validation rules

So if your goal is to add only a private field that is would be processed on the logic level and would be stored in the database and not visible to the user then you need to make changes in the first group of files.

If you planning to add new public fields that are visible to the user you should change both groups of files. The database objects are converted into logic objects using Automapper so in case it fails you would need to define custom mapping

API methods for object creation are hiding the Id field from the acceptable parameters it is achieved by modifying the visibility of the Id field at `\Projects_And_Tasks\Projects_And_Tasks\Model\[ProjectModel_Create.cs, ProjectTaskModel_Create.cs]`
