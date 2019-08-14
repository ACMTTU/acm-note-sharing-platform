DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

docker build $DIR/../. -t acmregistry2019.azurecr.io/catalogapplication:stable
docker push acmregistry2019.azurecr.io/catalogapplication:stable