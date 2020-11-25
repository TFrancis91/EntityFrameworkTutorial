Creating Entity Framework Core 3.1 project using MySql in Fedora
1. Install Visual Studio Code
2. Install .Net Core SDK
3. Create a new console application. From terminal, dotnet new console -o EntityFrameworkTutorial
4. Run application using F5. Set the environment to .Net Core when prompted.
5. Add Entity Core packages,
    dotnet add package Microsoft.EntityFrameworkCore.Design
6. Add MySQL provider package,
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 3.0.0-rc1.final
7. Install dotnet ef tool
    dotnet tool install --global dotnet-ef
