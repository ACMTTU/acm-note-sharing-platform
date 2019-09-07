az login

# Set up their dev space
echo 'Please enter your name (alphanumeric, no spaces, all lowercase): '
read name

az aks use-dev-spaces -g acm-notes -n acm-notes-aks -y -s dev/$name
