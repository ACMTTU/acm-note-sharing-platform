**YOU ARE NOT THE CHECK THESE FILES IN WITH CHANGES INTO GIT. THESE ARE CONSIDERED SENSITIVE FILES**

# How to deploy the Platform onto Azure

Prerequisites:
* Must have successfully ran the `../install_prerequisites.sh` script
* Must have access to keys and secrets necessary for Key Vault Deployment (Ask Principal Engineer for access)

Settings:
* `resourceGroupName`: resource group name
* `subscriptionId`: Subscription ID for ACM Notes Project
* `vaultName`: Name of the Azure Key Vault created during deployment. Search the `azuredeploy.json` file and find `KeyVault`
* `appId`: App ID you get from creating a Service Principal for RBAC, assigned to the KeyVault Service
* `clientSecret`: Password you get from creating a Service Principal for RBAC, assigned to the KeyVault Service

How to run scripts:
1. `./deploy-resources.sh`
2. You will then be prompted for the `service_account_client_id`, which is the value of the `appId`
3. You will then be prompted for the `service_account_client_secret`, which is the value of the `clientSecret`
4. Wait for next prompt
5. You will then be prompted for a Subscription ID and the client id and secret again.


