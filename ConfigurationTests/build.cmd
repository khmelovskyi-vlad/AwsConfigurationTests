dotnet restore
dotnet tool install -g Amazon.Lambda.Tools --framework net6.0
dotnet lambda package --configuration Release --function-architecture arm64 --framework net6.0 --output-package ../SolutionItems/configurationTests.zip