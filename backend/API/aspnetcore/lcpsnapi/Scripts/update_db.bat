@echo off
setlocal enableextensions

cd "C:\Users\Luis\Documents\projects\LCPSocialNetwork\backend\API\aspnetcore\lcpsnapi"
REM dotnet ef database remove
dotnet ef migrations add InitialCreation
dotnet ef database update

pause
exit /b %errorlevel%
endlocal