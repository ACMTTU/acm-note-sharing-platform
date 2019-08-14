docker login acmregistry2019.azurecr.io -u $DOCKER_USERNAME -p $DOCKER_PASSWORD

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

docker build $DIR/../. -t acmregistry2019.azurecr.io/classapplication:stable
docker push acmregistry2019.azurecr.io/classapplication:stable