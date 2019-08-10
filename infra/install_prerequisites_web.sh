# Install NodeJS
# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    nvm install 10.16
    brew install kubernetes-helm
    # Linux
    elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.1/install.sh | bash
    nvm install 10.16
    # Windows
    elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    open https://nodejs.org/dist/v10.16.0/node-v10.16.0-x64.msi
    read -n 1 -s -r -p "Press any key when done installing node"
    nvm install 10.16
fi

npm -v
node -v
nvm --version

echo "Please Install Docker for your own machine"