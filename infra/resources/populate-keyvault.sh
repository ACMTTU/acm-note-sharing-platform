source ./constants.sh

az login --service-principal --username $appId --password $clientSecret --tenant $tenantId

# Database connection strings
echo "Populating Key Vault with Database Secrets..."

productionDocDBConnectionString=$(az cosmosdb keys list -n notes-app-docdb -g notes-app --query primaryMasterKey)
productionDocDBConnectionString=${productionDocDBConnectionString//\"}
stagingDocDBConnectionString=$(az cosmosdb keys list -n staging-notes-app-docdb -g notes-app --query primaryMasterKey)
stagingDocDBConnectionString=${stagingDocDBConnectionString//\"}
developmentDocDBConnectionString=$(az cosmosdb keys list -n dev-notes-app-docdb -g notes-app --query primaryMasterKey)
developmentDocDBConnectionString=${developmentDocDBConnectionString//\"}

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
