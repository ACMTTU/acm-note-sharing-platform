az extension add --name azure-devops

resourceGroupName='notes-app'
groupName='ACM Notes Sharing Application Developers Spring 2020'

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

objectId=$(az ad user create --user-principal-name $userPrincipleName --display-name "${displayName}" --password $password --force-change-password-next-login --query objectId -o tsv)
az ad group member add --group "${groupName}" --member-id ${objectId}

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
