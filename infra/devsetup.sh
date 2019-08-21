az login

# Set up their dev space
echo 'Please enter your name (alphanumeric, no spaces): '
read name

az aks use-dev-spaces -g acm-notes -n acm-aks -y -s dev/$name