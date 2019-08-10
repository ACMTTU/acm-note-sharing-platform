# Mac OS
if [ "$(uname)" == "Darwin" ]; then
    xcode-select --install
else
    echo "Cannot develop iOS when not using a Mac..."
fi

