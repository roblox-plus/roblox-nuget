#!/bin/bash

# https://stackoverflow.com/a/821419/1663648
set -e

echo "START build.sh"
echo "BUILD_NUMBER: $BUILD_NUMBER"
start=`date +%s`

if [ "$GITHUB_ACTIONS" = "true" ]
then
	configuration="Release"
else
	configuration="Debug"
	
	sh generate_build_proj.sh
fi

dotnet msbuild "./build.proj" -t:Build -maxcpucount:64 -p:Configuration=$configuration

end=`date +%s`
runtime=$((end-start))
echo "END build.sh (runtime: $runtime seconds)"
