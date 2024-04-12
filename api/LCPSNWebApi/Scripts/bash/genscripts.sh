#!/usr/bin/bash

DEFOPTIONDBM=1
DBMode="SQLServer"
ASPNETCORE_ENVIRONMENT="Development"
USERPTHCT="$HOME"
MYLCPPATH="$USERPTHCT/Documents/projects/LCPSocialNetwork/api/LCPSNWebApi"

genScriptSQLServer () {
	DBMode="SQLServer"
	dotnet ef migrations script --context DBContext --output "Scripts/sql/lcpsnwasqlserver.sql"
}

genScriptSQLite () {
	DBMode="SQLite"
	dotnet ef migrations script --context DBContextSQLite --output "Scripts/sql/lcpsnwasqlite.sql"
}

genScriptPostgreSQL () {
	DBMode="PostgreSQL"
	dotnet ef migrations script --context DBContextPostgreSQL --output "Scripts/sql/lcpsnwasqlpostgresql.sql"
}

genScriptMySQL () {
	DBMode="MySQL"
	dotnet ef migrations script --context DBContextMySQL --output "Scripts/sql/lcpsnwasqlmysql.sql"
}

genScriptAll () {
	genDBSQLServer
	genDBSQLite
	genDBPostgreSQL
	genDBMySQL
}

clear
dotnet tool install --global dotnet-ef

if [[ -d "$MYLCPPATH/Scripts/sql" ]]; then 
	rm -rf "$MYLCPPATH/Scripts/sql"
fi

if [[ ! -d "$MYLCPPATH/Scripts/sql" ]]; then 
	mkdir -p "$MYLCPPATH/Scripts/sql"
fi

cd "$MYLCPPATH"

echo
echo "1. SQL Server"
echo "2. SQLite"
echo "3. PostgreSQL"
echo "4. MySQL"
echo "5. All"
echo

read -p "Choose the option [$DEFOPTIONDBM]: " DBModeOpt
DBModeOpt="${DBModeOpt:-$DEFOPTIONDBM}"

if [[ $DBModeOpt == "1" ]]; then
	genScriptSQLServer
elif [[ $DBModeOpt == "2" ]]; then
	genScriptSQLite
elif [[ $DBModeOpt == "3" ]]; then
	genScriptPostgreSQL
elif [[ $DBModeOpt == "4" ]]; then
	genScriptMySQL
else
	genScriptAll
fi

exit