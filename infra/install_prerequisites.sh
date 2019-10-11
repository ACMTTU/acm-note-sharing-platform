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
    open https://download.visualstudio.microsoft.com/download/pr/3998e58a-46dd-4f9c-a0e2-d17309de20fb/d694ddf3d8f99e8dee928e0b46f15084/dotnet-sdk-2.1.802-osx-x64.pkg
    
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    # Please install on your flavor
    open https://dotnet.microsoft.com/download/linux-package-manager/rhel7/sdk-2.1.802
    
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    start https://download.visualstudio.microsoft.com/download/pr/0297dbc2-424f-426a-a415-b39927dffe9a/2417ef7aae3c24da94ad7e54137b38b6/dotnet-sdk-2.1.802-win-x64.exe
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