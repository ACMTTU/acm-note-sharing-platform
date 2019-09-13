az extension add --name azure-devops

echo 'Resource Group Name: '
read resourceGroupName

echo 'First Name: '
read firstName

echo 'Last Name: '
read lastName

formattedFirstName=${firstName:0:2}
formattedFirstName=$(echo $formattedFirstName | tr '[:upper:]' '[:lower:]')


formattedLastName=${lastName:0:4}
formattedLastName=$(echo $formattedLastName | tr '[:upper:]' '[:lower:]')

userPrincipleName=${formattedFirstName}${formattedLastName}@acmttu.org
displayName="$firstName $lastName"
displayNameLower=$(echo $displayName | tr '[:upper:]' '[:lower:]')
password=$(openssl rand -base64 8)

clear

echo 'Creating account...'

az ad user create --user-principal-name $userPrincipleName --display-name "${displayName}" --password $password --force-change-password-next-login  > /dev/null 2>&1

echo 'Adding account to development resource group...'

az role assignment create --role "Contributor" --assignee $userPrincipleName --resource-group $resourceGroupName > /dev/null 2>&1

echo
echo 'Username: '
echo $userPrincipleName
echo
echo 'Password: (will be prompted to change when first logged in)'
echo $password
echo
echo 'Done!'
echo
echo 'Your principle engineer will add you to the Azure Dev Ops organization soon!'
