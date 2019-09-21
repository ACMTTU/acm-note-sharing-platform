echo "Version: "
read version

echo "API Key: "
read apiKey

dotnet build
dotnet pack SDK.csproj
dotnet nuget push ./bin/Debug/ACMTTU.NoteSharing.Shared.SDK.$version.nupkg -k $apiKey -s https://api.nuget.org/v3/index.json