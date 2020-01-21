# Windows
if [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ];
then
    az.cmd login
    
    # Set up their dev space
    echo 'Please enter your name (alphanumeric, no spaces, all lowercase): '
    read name
    
    az.cmd aks use-dev-spaces -g notes-app -n notes-app-aks -y -s dev/$name
else
    az login
    
    # Set up their dev space
    echo 'Please enter your name (alphanumeric, no spaces, all lowercase): '
    read name
    
    az aks use-dev-spaces -g notes-app -n notes-app-aks -y -s dev/$name
fi