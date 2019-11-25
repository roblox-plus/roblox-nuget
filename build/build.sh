#!/bin/bash

# https://stackoverflow.com/a/821419/1663648
set -e

echo "START build.sh"
echo "BUILD_NUMBER: $BUILD_NUMBER"

# https://stackoverflow.com/a/25119904/1663648
if [ "$GITHUB_ACTIONS" = "true" ]; then configuration="Release"; else configuration="Debug"; fi

slns=(
)

echo "Building ${#slns[@]} solutions (configuration: $configuration)..."

# https://stackoverflow.com/a/18898718/1663648
for sln in "${slns[@]}"
do
    echo "Building $sln..."
	dotnet build ./../$sln -p:Version=2.0.$BUILD_NUMBER --configuration $configuration
done

echo "END build.sh"
