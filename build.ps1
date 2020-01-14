dotnet restore -v:d

& .\packAndUpdate.ps1

dotnet build
dotnet test

