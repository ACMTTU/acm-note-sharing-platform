# Make sure to install jq for your OS before running this script

# Installing the Key Vault FlexVolumes onto the cluster
# You must be using a cluster of 3 or more nodes for this to work
# Otherwise, your cluster will run out of memory and die
kubectl apply -f https://raw.githubusercontent.com/Azure/kubernetes-keyvault-flexvol/master/deployment/kv-flexvol-installer.yaml

echo "Subscription Id: "
read subscriptionId

echo "App ID: "
read appId

echo "Password: "
read clientSecret

# # Set up kubectl for AKS
az aks get-credentials --resource-group acm-notes --name acm-notes-aks

# Create a Secret in Kubernetes Cluster
kubectl create secret generic acmkvcreds \
--from-literal clientid=$appId \
--from-literal clientsecret=$clientSecret \
--namespace secrets \
--type=azure/kv

# Allow the Service Principal we made to talk to Key Vault
az role assignment create \
--role Reader \
--assignee $appId \
--scope /subscriptions/$subscriptionId/resourcegroups/acm-notes/providers/Microsoft.KeyVault/vaults/acm-notes-kv

az keyvault set-policy -n acm-notes-kv --key-permissions get --spn $appId
az keyvault set-policy -n acm-notes-kv --secret-permissions get --spn $appId
az keyvault set-policy -n acm-notes-kv --certificate-permissions get --spn $appId