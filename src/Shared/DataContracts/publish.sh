echo "Version: "
read version

echo "API Key: "
read apiKey

dotnet build
dotnet pack DataContracts.csproj
dotnet nuget push ./bin/Debug/ACMTTU.NoteSharing.Shared.DataContracts.$version.nupkg -k $apiKey -s https://api.nuget.org/v3/index.json