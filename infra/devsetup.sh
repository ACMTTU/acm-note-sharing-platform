az login

echo 'Resource Group Name: '
read resourceGroupName

# Set up their dev space
echo 'Please enter your name (alphanumeric, no spaces): '
read name

az aks use-dev-spaces -g $resourceGroupName -n acm-aks -y -s dev/$name