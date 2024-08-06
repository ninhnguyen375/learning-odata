# THELINER API - for EHS GEMBA

## Steps to run the project:

1. Move to **src** folder.
2. Run command-line `dotnet restore` to install all packages were used in project.
3. Run command-line `dotnet ef database update` in first time when run the project to create new database.
4. Run command-line `dotnet run` to start server.
5. Access [**Swagger**](http://localhost:5000/swagger/index.html) to run API.

## Requirements:

1. Install [**.NET SDK 8.0**](https://dotnet.microsoft.com/download).
2. Install **SQL Server**.
3. Install **dotnet ef** in by command-line `dotnet tool install --global dotnet-ef`.
   If **dotnet ef** was installed then using the following command line `dotnet tool update --global dotnet-ef` to update dotnet-ef tool.
