source ./constants.sh

az login --service-principal --username $appId --password $clientSecret --tenant $tenantId

# Database connection strings
echo "Populating Key Vault with Database Secrets..."

baseDbName=notes-app-docdb
baseStorageName=notesappblob

productionDocDBConnectionString=$(az cosmosdb keys list -n ${baseDbName} -g notes-app --query primaryMasterKey)
productionDocDBConnectionString=AccountEndpoint=https://${baseDbName}.documents.azure.com:443/\;AccountKey=${productionDocDBConnectionString//\"}\;
stagingDocDBConnectionString=$(az cosmosdb keys list -n staging-${baseDbName} -g notes-app --query primaryMasterKey)
stagingDocDBConnectionString=AccountEndpoint=https://staging-${baseDbName}.documents.azure.com:443/\;AccountKey=${stagingDocDBConnectionString//\"}\;
developmentDocDBConnectionString=$(az cosmosdb keys list -n dev-${baseDbName} -g notes-app --query primaryMasterKey)
developmentDocDBConnectionString=AccountEndpoint=https://dev-${baseDbName}.documents.azure.com:443/\;AccountKey=${developmentDocDBConnectionString//\"}\;

az keyvault secret set --vault-name $vaultName --name "database-prod" --value $productionDocDBConnectionString
az keyvault secret set --vault-name $vaultName --name "database-staging" --value $stagingDocDBConnectionString
az keyvault secret set --vault-name $vaultName --name "database-dev" --value $developmentDocDBConnectionString

echo "Done with Database secrets!"

echo "Populating Key Vault with Storage secrets..."
# Storage connection strings

productionBlobStorageConnectionString=$(az storage account show-connection-string -g notes-app -n notesappblob --query "connectionString" | tr -d \")
stagingBlobStorageConnectionString=$(az storage account show-connection-string -g notes-app -n stagingnotesappblob --query "connectionString" | tr -d \")
developmentBlobStorageConnectionString=$(az storage account show-connection-string -g notes-app -n devnotesappblob --query "connectionString" | tr -d \")

az keyvault secret set --vault-name $vaultName --name "blobstorage-prod" --value $productionBlobStorageConnectionString
az keyvault secret set --vault-name $vaultName --name "blobstorage-staging" --value $stagingBlobStorageConnectionString
az keyvault secret set --vault-name $vaultName --name "blobstorage-dev" --value $developmentBlobStorageConnectionString

echo "Done with Storage secrets! Logging out of SP..."
az logout
az login
