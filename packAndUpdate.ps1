
dotnet clean .\XamlTemplates.MSBuild\XamlTemplates.MSBuild.csproj -c release

dotnet build .\XamlTemplates.MSBuild\XamlTemplates.MSBuild.csproj -c release

.\XamlTemplates.MSBuild\nuget.exe pack .\XamlTemplates.MSBuild\XamlTemplates.MSBuild.nuspec

Move-Item -Path ".\*.nupkg" -Destination ".\nugetResults" -Force -Verbose

dotnet add .\Evans.XamlTemplates\Evans.XamlTemplates.csproj package XamlTemplates.MSBuild -s "nugetResults"

