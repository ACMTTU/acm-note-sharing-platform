# How to use Infrastructure Scripts

Make sure to update if using scripts for a new semester. (Current: Spring 2020)

## add_contributor.sh
Adds a new user with a unique email and password. This password will be reset as soon as they log in for the first time.

Prerequisites:
* Knowledge of what the resource group name is (typically, `notes-app`)
* Knowledge of which group to add the new user to (Current: `ACM Notes Sharing Application Developers Spring 2020`)
* Must have admin privileges to the Azure Subscription for ACM

Settings:
* You can find variables `resourceGroupName` and `groupName` which you can update

How to use:
1. `./add_contributor.sh` on Unix-like systems or `add_contributor` on Windows
2. Send the username and password generated to email provided on the Collaborators Request Form

## install_prerequisites.sh
Installs all of the software needed to run the application/platform on a local machine.

Prerequisites:
* None

Settings:
* None

How to use:
1. `./install_prerequisites.sh` on Unix-like systems or `install_prerequisites` on Windows
2. You typically want to run the `install_extensions` script

## install_extensions.sh
Installs the Visual Studio Code extensions needed to develop the project and properly debug it

Prerequisites:
* Must have successfully ran the `install_prerequisites.sh` script

Settings:
* None

How to use:
1. `./install_extensions.sh` on Unix-like systems or `install_extensions` on Windows
2. May need to restart VS Code

## devsetup.sh
Sets you up with a Azure Kubernetes Dev Space which allows you to write code on your local machine and see how it interacts with the rest of the cloud environment. This way, no matter what machine you are on, you should be able to work on the project.

Prerequisites:
* Must have a work account (your @acmttu.org email)
* Must have successfully ran the `install_prerequisites.sh` script

Settings:
* You can change the `resourceGroupName` variable in the script to properly target the resource group the AKS service is running on

How to use:
1. `./devsetup.sh` on Unix-like systems or `devsetup` on Windows
2. May need to restart VS Code