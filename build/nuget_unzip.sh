#!/bin/bash

# https://stackoverflow.com/a/821419/1663648
set -e

echo "START nuget_unzip.sh"
echo "BUILD_NUMBER: $BUILD_NUMBER"
start=`date +%s`

if [ "$GITHUB_ACTIONS" = "true" ]
then
	unzip tix-factory-nuget.zip
	cp -r nuget ./../LocalNuGetRepo
	rm -rf nuget
	dir
else
	echo "nuget_unzip.sh publish can only run from build agent"
fi

end=`date +%s`
runtime=$((end-start))
echo "END nuget_unzip.sh (runtime: $runtime seconds)"
