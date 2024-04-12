@echo off
setlocal enableextensions

SET DEFOPTIONDBM=1
SET DBMode="SQLServer"
SET ASPNETCORE_ENVIRONMENT="Development"
SET MYLCPPATH="%USERPROFILE%\Documents\projects\LCPSocialNetwork\api\LCPSNWebApi"

cls
dotnet tool install --global dotnet-ef

if exist "%MYLCPPATH%\Scripts\sql" ( 
	rd /s /q "%MYLCPPATH%\Scripts\sql"
)

if not exist "%MYLCPPATH%\Scripts\sql" (
	mkdir "%MYLCPPATH%\Scripts\sql"
)

cd "%MYLCPPATH%"

echo.
echo 1. SQL Server
echo 2. SQLite
echo 3. PostgreSQL
echo 4. MySQL
echo 5. All
echo.

set /P DBModeOpt="Choose the option [%DEFOPTIONDBM%]: "
if "%DBModeOpt%"=="" set "DBModeOpt=%DEFOPTIONDBM%"

if "%DBModeOpt%" == "1" (
	call :genScriptSQLServer
) else if "%DBModeOpt%" == "2" (
	call :genScriptSQLite
) else if "%DBModeOpt%" == "3" (
	call :genScriptPostgreSQL
) else if "%DBModeOpt%" == "4" (
	call :genScriptMySQL
) else (
	call :genScriptAll
)

:genScriptSQLServer
	SET DBMode="SQLServer"
	dotnet ef migrations script --context DBContext --output "Scripts\sql\lcpsnwasqlserver.sql"
goto:eof

:genScriptSQLite
	SET DBMode="SQLite"
	dotnet ef migrations script --context DBContextSQLite --output "Scripts\sql\lcpsnwasqlite.sql"
goto:eof

:genScriptPostgreSQL
	SET DBMode="PostgreSQL"
	dotnet ef migrations script --context DBContextPostgreSQL --output "Scripts\sql\lcpsnwasqlpostgresql.sql"
goto:eof

:genScriptMySQL
	SET DBMode="MySQL"
	dotnet ef migrations script --context DBContextMySQL --output "Scripts\sql\lcpsnwasqlmysql.sql"
goto:eof

:genScriptAll
	call :genScriptSQLServer
	call :genScriptSQLite
	call :genScriptPostgreSQL
	call :genScriptMySQL
goto:eof

exit /b %errorlevel%
endlocal