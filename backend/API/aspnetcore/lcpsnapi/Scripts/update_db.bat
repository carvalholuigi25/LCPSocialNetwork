@echo off
setlocal enableextensions

cd "C:\Users\Luis\Documents\projects\websites\LCPSocialNetwork\backend\API\aspnetcore\lcpsnapi"

dotnet ef migrations remove --force
dotnet ef migrations add InitialCreation
dotnet ef database update

pause
exit /b %errorlevel%
endlocal