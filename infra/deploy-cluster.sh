az login
az group create --name acm --location westus2

# Deploy Redis, AKS/Kubernetes, CosmosDB, Storage, EventHub, and ACR
az group deployment create -g acm --template-file ./resources/azuredeploy.json

# Turn on Dev Spaces
az aks use-dev-spaces -g acm -n acm-aks
