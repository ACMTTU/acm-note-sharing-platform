source ./constants.sh

# Make sure to install jq for your OS before running this script

# Installing the Key Vault FlexVolumes onto the cluster
# You must be using a cluster of 3 or more nodes for this to work
# Otherwise, your cluster will run out of memory and die
kubectl apply -f https://raw.githubusercontent.com/Azure/kubernetes-keyvault-flexvol/master/deployment/kv-flexvol-installer.yaml

# # Set up kubectl for AKS
az aks get-credentials --resource-group notes-app --name notes-app-aks

# Create a Secret in Kubernetes Cluster
kubectl create secret generic notesappkvcreds \
--from-literal clientid=$appId \
--from-literal clientsecret=$clientSecret \
--namespace secrets \
--type=azure/kv

# Allow the Service Principal we made to talk to Key Vault
az role assignment create \
--role Reader \
--assignee $appId \
--scope /subscriptions/$subscriptionId/resourcegroups/notes-app/providers/Microsoft.KeyVault/vaults/notes-app-kv

az keyvault set-policy -n notes-app-kv --key-permissions get --spn $appId
az keyvault set-policy -n notes-app-kv --secret-permissions get --spn $appId
az keyvault set-policy -n notes-app-kv --certificate-permissions get --spn $appId