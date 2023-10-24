# Prerequisites

1. Install dotnetcore 7 sdk and runtimes (netcore and aspnetcore)
1. Install dotnet EntityFramework tool (`dotnet tool install --global dotnet-ef`)
1. create empty SQL Server database

# Database schema
1. amend connection string in AgeStructureDb/DbModel.cs (outside coding assignment this would )
1. in AgeStructureDB folder run `dotnet ef database update`

# Data load
1. run DbCreateLoad console program.