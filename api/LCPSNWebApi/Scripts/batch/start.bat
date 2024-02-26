@echo off
setlocal enableextensions

cd "%USERPROFILE%\Documents\projects\angular\LCPSocialNetwork\api\LCPSNWebApi"
start /d "." dotnet watch run

pause
exit /b %errorlevel%
endlocal