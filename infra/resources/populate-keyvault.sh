vaultName="acm-notes-kv"
resourceGroup="acm-notes"
subscriptionId="9c16434b-4aa3-4e38-9bda-d68d192f9b2c"

# Database connection strings
echo "Populating Key Vault with Database Secrets..."

productionDocDBConnectionString=$(az cosmosdb list-connection-strings -n acm-notes-docdb -g $resourceGroup --subscription $subscriptionId --query "connectionStrings[?description=='Primary SQL Connection String'].connectionString | [0]" | tr -d \")
stagingDocDBConnectionString=$(az cosmosdb list-connection-strings -n staging-acm-notes-docdb -g $resourceGroup --subscription $subscriptionId --query "connectionStrings[?description=='Primary SQL Connection String'].connectionString | [0]" | tr -d \")
developmentDocDBConnectionString=$(az cosmosdb list-connection-strings -n dev-acm-notes-docdb -g $resourceGroup --subscription $subscriptionId --query "connectionStrings[?description=='Primary SQL Connection String'].connectionString | [0]" | tr -d \")

az keyvault secret set --vault-name $vaultName --name "database-prod" --value $productionDocDBConnectionString
az keyvault secret set --vault-name $vaultName --name "database-staging" --value $stagingDocDBConnectionString
az keyvault secret set --vault-name $vaultName --name "database-dev" --value $developmentDocDBConnectionString

echo "Done with Database secrets!"

echo "Populating Key Vault with Storage secrets..."
# Storage connection strings

productionBlobStorageConnectionString=$(az storage account show-connection-string -g acm-notes -n acmnotesblob --query "connectionString" | tr -d \")
stagingBlobStorageConnectionString=$(az storage account show-connection-string -g acm-notes -n stagingacmnotesblob --query "connectionString" | tr -d \")
developmentBlobStorageConnectionString=$(az storage account show-connection-string -g acm-notes -n devacmnotesblob --query "connectionString" | tr -d \")

az keyvault secret set --vault-name $vaultName --name "blobstorage-prod" --value $productionBlobStorageConnectionString
az keyvault secret set --vault-name $vaultName --name "blobstorage-staging" --value $stagingBlobStorageConnectionString
az keyvault secret set --vault-name $vaultName --name "blobstorage-dev" --value $developmentBlobStorageConnectionString

echo "Done with Storage secrets!"