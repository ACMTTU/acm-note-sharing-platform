DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

docker build $DIR/../. -t notesappregistry.azurecr.io/classapplication:stable
docker push notesappregistry.azurecr.io/classapplication:stable