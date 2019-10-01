Version=$(sed -ne '/Version/{s/.*<Version>\(.*\)<\/Version>.*/\1/p;q;}' <<< cat Sdk.csproj)

echo "API Key: "
read apiKey

dotnet build
dotnet pack SDK.csproj
dotnet nuget push ./bin/Debug/ACMTTU.NoteSharing.Shared.SDK.$Version.nupkg -k $apiKey -s https://api.nuget.org/v3/index.json