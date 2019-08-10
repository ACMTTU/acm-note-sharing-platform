installExtensions() {
    if hash code 2>/dev/null; then
        code --install-extension azuredevspaces.azds --force
        code --install-extension ms-vscode.csharp --force
        code --install-extension jchannon.csharpextensions --force
        code --install-extension ms-azuretools.vscode-docker --force
        code --install-extension kishoreithadi.dotnet-core-essentials --force
        code --install-extension eamodio.gitlens --force
        code --install-extension ms-kubernetes-tools.vscode-kubernetes-tools --force
        code --install-extension jmrog.vscode-nuget-package-manager --force
        code --install-extension wayou.vscode-todo-highlight --force
        code --install-extension fernandoescolar.vscode-solution-explorer --force
    else
        echo "Make sure you have Visual Studio Code installed before continuing. Openning browser..."
        read -n 1 -s -r -p "Press any key to try again"
        installExtensions
    fi
}

installExtensions