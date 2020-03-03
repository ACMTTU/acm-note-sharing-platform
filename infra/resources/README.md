**YOU ARE NOT THE CHECK THESE FILES IN WITH CHANGES INTO GIT. THESE ARE CONSIDERED SENSITIVE FILES**

# How to deploy the Platform onto Azure

Prerequisites:
* Must have successfully ran the `../install_prerequisites.sh` script
* Must have access to keys and secrets necessary for Key Vault Deployment (Ask Principal Engineer for access)

Settings:
* `resourceGroupName`: resource group name
* `subscriptionId`: Subscription ID for ACM Notes Project
* `vaultName`: Name of the Azure Key Vault created during deployment. Search the `azuredeploy.json` file and find `KeyVault`
* `appId`: App ID you get from creating a Service Principal for RBAC
* `clientSecret`: Password you get from creating a Service Principal for RBAC
* `tenantID`: Tenant ID you get from creating a Service Principal for RBAC

How to run scripts:
1. Declare proper variabels in `constants.sh`
2. Search for `service_account_client_id` and `service_account_client_secret` and fill in default values
3. `./deploy-resources.sh`



