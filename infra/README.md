This script should install all of the dependencies for developing the application and platform

# Prerequisites

These must be installed manually

* git
* Chocolatey (Windows) (https://chocolatey.org/docs/installation)
* Brew (MacOS) (https://brew.sh)

# Instructions

You must run commands in a Bash-enabled, elevated terminal
>
> ./install_prerequisites.sh
>
> ./devsetup.sh
>

# Things everyone needs:

* VS Code + Extensions
* Azure CLI (az)
* NodeJS for web development

# Things the Client Teams need:

* Android Studio
* XCode and all of it's stuff

# Things Principles need:
Since they are in charge of infrastructure, they require extra tooling
* Helm
* Docker
* Kubernetes CLI

# How to deploy the application
1. Authenticate with Azure AD
2. Create a service account for AKS and Tiller from Helm to use
3. Create a Task on Azure Pipelines and authenticate using the Azure Subscription
4. Choose the `acm-notes` resource group
5. Exit the task build and edit the `AZURE_SUBSCRIPTION` variable in Pipelines to match the authenticated subscription
5. Run Pipeline to build and deploy