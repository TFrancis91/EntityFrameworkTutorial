Creating Entity Framework Core 3.1 Code First project using MySql in Fedora
1. Install Visual Studio Core
2. Install .Net Core SDK
3. Create a new console application. From termonal, dotnet new console
4. Run application using F5. Set the environment to .Net Core when prompted.
5. Add Entity Core packages,
    dotnet add package Microsoft.EntityFrameworkCore.Design
6. Add MySQL provider package,
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 3.0.0-rc1.final
7. Install dotnet ef tool
    dotnet tool install --global dotnet-ef
