az login

# Set up their dev space
echo 'Please enter your name: '
read name

az aks use-dev-spaces -g acm -n acm-aks -y -s $name