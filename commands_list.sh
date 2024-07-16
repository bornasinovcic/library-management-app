docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

dotnet ef migrations add Initial --startup-project ..\\LibraryManagementApp.Web --context LibraryDbContext
dotnet ef database update --startup-project ..\\LibraryManagementApp.Web --context LibraryDbContext

NuGet\Install-Package Microsoft.EntityFrameworkCore -Version 8.0.4
NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.4
NuGet\Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.4
NuGet\Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.4

dbcc checkident ('Clients', reseed, 0);

npm install cypress --save-dev
npx cypress open
npx cypress run
npx cypress run --headed
npx cypress run --headed --browser edge