dotnet restore
dotnet tool install -g Amazon.Lambda.Tools --framework net6.0
dotnet lambda package --project-location ../../ConfigurationTests --configuration Release --function-architecture arm64 --framework net6.0 --output-package ../configurationTests.zip
dotnet lambda package --project-location ../../UserAuthorizer --configuration Release --function-architecture arm64 --framework net6.0 --output-package ../userAuthorizer.zip
