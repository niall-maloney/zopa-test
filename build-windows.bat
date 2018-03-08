call dotnet restore
call dotnet build .\ZopaTest.App\ZopaTest.App.csproj -f netcoreapp2.0 -c Release -r win-x64 -o ..\build-output\win-x64\ZopaTest