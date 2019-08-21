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

# Installs Azure CLI, Node Version Manager

# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    brew update && brew install azure-cli
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
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