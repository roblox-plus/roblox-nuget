#!/bin/bash

# https://stackoverflow.com/a/821419/1663648
set -e

echo "START generate_build_proj.sh"
echo "BUILD_NUMBER: $BUILD_NUMBER"

dotnet run --project "E:\Git\tix-factory\msbuild-project-generator\TixFactory.MsBuildProjectGenerator\TixFactory.MsBuildProjectGenerator\TixFactory.MsBuildProjectGenerator.csproj" ../ ./build.proj

echo "END generate_build_proj.sh"
