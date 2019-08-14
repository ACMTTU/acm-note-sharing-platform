az login
az group create --name acm --location westus2

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

# Deploy Redis, AKS/Kubernetes, CosmosDB, Storage, EventHub, and ACR
az group deployment create -g acm --template-file $DIR/azuredeploy.json

# Turn on Dev Spaces
az aks use-dev-spaces -g acm -n acm-aks -s dev -y

# Allow AKS to talk to ACR by creating a role assignment
CLIENT_ID=$(az aks show --resource-group acm --name acm-aks --query "servicePrincipalProfile.clientId" --output tsv)
ACR_ID=$(az acr show --name acmregistry2019 --resource-group acm --query "id" --output tsv)
az role assignment create --assignee $CLIENT_ID --role acrpull --scope $ACR_ID