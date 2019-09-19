az login

# Set up their dev space
echo 'Please enter your name (alphanumeric, no spaces, all lowercase): '
read name

az aks use-dev-spaces -g acm-notes -n acm-notes-aks -y -s dev/$name

# Migrate Secrets from the default namespace to the namespace you created
secrets=$(echo "$(kubectl get secrets acmkvcreds --namespace=default -oyaml)" | sed "s/default/$name/g")
echo $secrets | kubectl apply --namespace miggyreyes -f -


# Windows
if [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ];
then
    az.cmd login
    
    # Set up their dev space
    echo 'Please enter your name (alphanumeric, no spaces, all lowercase): '
    read name
    
    az.cmd aks use-dev-spaces -g acm-notes -n acm-notes-aks -y -s dev/$name
    
    # Migrate Secrets from the default namespace to the namespace you created
    secrets=$(echo "$(kubectl get secrets acmkvcreds --namespace=default -oyaml)" | sed "s/default/$name/g")
    echo $secrets | kubectl apply --namespace miggyreyes -f -
else
    az login
    
    # Set up their dev space
    echo 'Please enter your name (alphanumeric, no spaces, all lowercase): '
    read name
    
    az aks use-dev-spaces -g acm-notes -n acm-notes-aks -y -s dev/$name
    
    # Migrate Secrets from the default namespace to the namespace you created
    secrets=$(echo "$(kubectl get secrets acmkvcreds --namespace=default -oyaml)" | sed "s/default/$name/g")
    echo $secrets | kubectl apply --namespace miggyreyes -f -
fi