DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

docker build $DIR/../. -t acmnotesregistry.azurecr.io/claimsservice:stable
docker push acmnotesregistry.azurecr.io/claimsservice:stable