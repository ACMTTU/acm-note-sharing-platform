DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

echo 'Resource Group Name: '
read resourceGroupName

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

# Deploy Redis, AKS/Kubernetes, CosmosDB, Storage, EventHub, and ACR
az group deployment create -g $resourceGroupName --template-file $DIR/azuredeploy.json

# Turn on Dev Spaces
az aks use-dev-spaces -g $resourceGroupName -n acm-notes-aks -s dev -y

# Install Helm inside AKS
kubectl create serviceaccount tiller --namespace kube-system
kubectl create clusterrolebinding tiller --clusterrole cluster-admin --serviceaccount=kube-system:tiller
helm init --service-account tiller --history-max 200

# Create a production namespace to hold the production services
kubectl create ns production

# Allow AKS to talk to ACR by creating a role assignment
CLIENT_ID=$(az aks show --resource-group $resourceGroupName --name acm-notes-aks --query "servicePrincipalProfile.clientId" --output tsv)
ACR_ID=$(az acr show --name acmnotesregistry --resource-group $resourceGroupName --query "id" --output tsv)
az role assignment create --assignee $CLIENT_ID --role acrpull --scope $ACR_ID
