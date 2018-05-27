dotnet restore
dotnet build
dotnet ef database update --context accountdbcontext
dotnet ef database update --context applicationdbcontext
dotnet ef database update --context hangfiredbcontext
dotnet run