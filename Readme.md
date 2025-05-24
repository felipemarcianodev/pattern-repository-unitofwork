
## Instalar pacotes NuGet:
dotnet tool install --global dotnet-ef --version 9.0.5 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.5

## Criar migration:
dotnet ef migrations add InitialCreate
dotnet ef database update
