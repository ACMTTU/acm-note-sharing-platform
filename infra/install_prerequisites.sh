#! /bin/bash

# Installs Visual Studio Code

# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    brew cask install visual-studio-code
    
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    sudo apt update
    sudo apt install software-properties-common apt-transport-https wget
    wget -q https://packages.microsoft.com/keys/microsoft.asc -O- | sudo apt-key add -
    sudo add-apt-repository "deb [arch=amd64] https://packages.microsoft.com/repos/vscode stable main"
    sudo apt update
    sudo apt install code
    
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    choco install vscode
fi

# Installs Dotnet Core SDK

# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    # Installs .NET runtime and SDK
    open https://download.visualstudio.microsoft.com/download/pr/5c281f95-91c4-499d-baa2-31fec919047a/38c6964d72438ac30032bce516b655d9/dotnet-sdk-3.0.100-osx-x64.pkg
    open https://download.visualstudio.microsoft.com/download/pr/1b09851c-1c1a-4aeb-a94a-7065db8741c0/b22a0b5501191fe1a263913d8ed11b2e/dotnet-runtime-3.0.0-osx-x64.pkg
    
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    # Please install on your flavor
    open https://dotnet.microsoft.com/download/linux-package-manager/rhel7/runtime-3.0.0
    
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    # Installs ASPNET Core, .NET runtime and SDK
    open https://download.visualstudio.microsoft.com/download/pr/53f250a1-318f-4350-8bda-3c6e49f40e76/e8cbbd98b08edd6222125268166cfc43/dotnet-sdk-3.0.100-win-x64.exe
    open https://download.visualstudio.microsoft.com/download/pr/173b8a01-e65b-4880-af6e-12e45a865c69/f2529ad22ce8eeb0f28fd48dead5459a/aspnetcore-runtime-3.0.0-win-x64.exe
    open https://download.visualstudio.microsoft.com/download/pr/1b09851c-1c1a-4aeb-a94a-7065db8741c0/b22a0b5501191fe1a263913d8ed11b2e/dotnet-runtime-3.0.0-osx-x64.pkg
fi

# Installs Azure CLI, Node Version Manager

# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    brew update && brew install azure-cli
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    sudo apt install curl -y #ubuntu doesn't ship with curl
    curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    choco install azure-cli
    choco install nvm
fi

# Install Node 10.16
nvm install 10.16

# Installing Kubernetes Stuff
az aks install-cli