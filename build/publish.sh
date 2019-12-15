#!/bin/bash

# https://stackoverflow.com/a/821419/1663648
set -e

echo "START publish.sh"
echo "BUILD_NUMBER: $BUILD_NUMBER"
start=`date +%s`

if [ "$GITHUB_ACTIONS" = "true" ]
then
	configuration="Release"
else
	configuration="Debug"
fi

rm -rf ./publish
mkdir -p ./publish

# We don't have any applications to publish right now.
# But if we did this is what would be run.
# dotnet msbuild "./build.proj" -t:Publish -maxcpucount:64 -p:RuntimeIdentifier=win-x64 -p:Configuration=$configuration -p:PublishSingleFile=true -p:SelfContained=false -restore

if [ "$GITHUB_ACTIONS" = "true" ]
then
	cp build.proj ./publish/build.proj
	zip -r publish.zip publish
	cp -r ./../LocalNuGetRepo nuget
	zip -r nuget.zip nuget
	dir
fi

end=`date +%s`
runtime=$((end-start))
echo "END publish.sh (runtime: $runtime seconds)"
