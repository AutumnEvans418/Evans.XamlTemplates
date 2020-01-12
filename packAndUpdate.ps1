
dotnet build .\XamlTemplates.MSBuild\XamlTemplates.MSBuild.csproj

.\XamlTemplates.MSBuild\nuget.exe pack .\XamlTemplates.MSBuild\XamlTemplates.MSBuild.nuspec

dotnet add .\Evans.XamlTemplates\Evans.XamlTemplates\Evans.XamlTemplates.csproj package XamlTemplates.MSBuild

