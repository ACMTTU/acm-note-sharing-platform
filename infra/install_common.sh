#!/usr/bin/env bash

echo "Sign up for an Azure Subscription"

open https://azure.microsoft.com/en-us/free/

echo "Please install VS Code if you have not already"

# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    open https://code.visualstudio.com/docs/?dv=osx
    
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    open https://code.visualstudio.com/docs/?dv=linux64_deb
    
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    open https://code.visualstudio.com/docs/?dv=win64user
fi

read -n 1 -s -r -p "Press any key to continue..."

# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    /usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
    brew update && brew install azure-cli
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    nvm install 10.16
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    nvm install 10.16
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    open https://aka.ms/installazurecliwindows
    open https://nodejs.org/dist/v10.16.0/node-v10.16.0-x64.msi
fi