@echo off
setlocal enableextensions

if not exist "%~dp0\lcpsnapi" (
   dotnet new webapi -lang "C#" --name "lcpsnapi" --output "lcpsnapi"
) else (
   echo The project "%~dp0\lcpsnapi" already exists!
)

pause
exit /b %errorlevel%
endlocal