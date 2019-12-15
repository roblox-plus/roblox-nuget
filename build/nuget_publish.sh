#!/bin/bash

# https://stackoverflow.com/a/821419/1663648
set -e

echo "START nuget_publish.sh"
echo "BUILD_NUMBER: $BUILD_NUMBER"
start=`date +%s`

if [ "$GITHUB_ACTIONS" = "true" ]
then
	for nugetPackage in ./../PublishNuGetRepo/*.nupkg
	do
		curl -vX PUT -u "roblox-plus:$1" -F package=@$nugetPackage https://nuget.pkg.github.com/roblox-plus/
	done
else
	echo "nuget_publish.sh publish can only run from build agent"
fi

end=`date +%s`
runtime=$((end-start))
echo "END nuget_publish.sh (runtime: $runtime seconds)"
