@ECHO OFF


IF "%1" == "" echo "Missing the enviroment name. Supported enviroment name: dev, staging, prod"

IF "%1" == "dev" dotnet fm migrate -p mysql -c "server=localhost;userid=root;pwd=a@12345;port=3306;database=emsdb;sslmode=none;" -a ".\src\LamondLu.EmailClient.ConsoleApp\bin\Release\netcoreapp2.2\publish\LamondLu.EmailClient.Infrastructure.DataPersistent.dll" -n "LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations"


IF "%1" == "staging" dotnet fm migrate -p mysql -c "" -a ".\src\LamondLu.EmailClient.ConsoleApp\bin\Release\netcoreapp2.2\publish\LamondLu.EmailClient.Infrastructure.DataPersistent.dll" -n "LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations"


IF "%1" == "prod" dotnet fm migrate -p mysql -c ""   -a ".\src\LamondLu.EmailClient.ConsoleApp\bin\Release\netcoreapp2.2\publish\LamondLu.EmailClient.Infrastructure.DataPersistent.dll" -n "LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations"
 