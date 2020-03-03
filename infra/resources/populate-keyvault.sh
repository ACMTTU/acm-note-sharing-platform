source ./constants.sh

# Database connection strings
echo "Populating Key Vault with Database Secrets..."

productionDocDBConnectionString=$(az cosmosdb list-connection-strings -n notes-app-docdb -g $resourceGroupName --subscription $subscriptionId --query "connectionStrings[?description=='Primary SQL Connection String'].connectionString | [0]" | tr -d \")
stagingDocDBConnectionString=$(az cosmosdb list-connection-strings -n staging-notes-app-docdb -g $resourceGroupName --subscription $subscriptionId --query "connectionStrings[?description=='Primary SQL Connection String'].connectionString | [0]" | tr -d \")
developmentDocDBConnectionString=$(az cosmosdb list-connection-strings -n dev-notes-app-docdb -g $resourceGroupName --subscription $subscriptionId --query "connectionStrings[?description=='Primary SQL Connection String'].connectionString | [0]" | tr -d \")

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

echo "Done with Storage secrets!"