source ./constants.sh

# Make sure to install jq for your OS before running this script

# Installing the Key Vault FlexVolumes onto the cluster
# You must be using a cluster of 3 or more nodes for this to work
# Otherwise, your cluster will run out of memory and die
kubectl apply -f https://raw.githubusercontent.com/Azure/kubernetes-keyvault-flexvol/master/deployment/kv-flexvol-installer.yaml

echo 'Gettings credentials for AKS'

# # Set up kubectl for AKS
az aks get-credentials --resource-group ${resourceGroupName} --name notes-app-aks

echo 'Creating secret for cluster'
# Create a Secret in Kubernetes Cluster
kubectl create secret generic notesappkvcreds \
--from-literal clientid=$appId \
--from-literal clientsecret=$clientSecret \
--namespace secrets \
--type=azure/kv

echo 'Giving SP a role assignment for everything'
# Allow the Service Principal we made to talk to Key Vault
az role assignment create \
--role Owner \
--assignee $appId \
--resource-group ${resourceGroupName}

az keyvault set-policy -n ${vaultName} --key-permissions get list decrypt unwrapKey verify create update --spn $appId > /dev/null 2>&1
az keyvault set-policy -n ${vaultName} --secret-permissions get list set delete --spn $appId > /dev/null 2>&1
az keyvault set-policy -n ${vaultName} --certificate-permissions get --spn $appId > /dev/null 2>&1