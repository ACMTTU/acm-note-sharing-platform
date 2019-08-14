DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

docker build $DIR/../. -t acmregistry2019.azurecr.io/authapplication:stable
docker push acmregistry2019.azurecr.io/authapplication:stable