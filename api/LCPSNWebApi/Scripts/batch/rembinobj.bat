@echo off
setlocal enableextensions
cls

if exist "%userprofile%\Documents\projects\aspnetcore\LCPSNWebApi\bin" (
	rmdir /s /q "%userprofile%\Documents\projects\aspnetcore\LCPSNWebApi\bin"
)

if exist "%userprofile%\Documents\projects\aspnetcore\LCPSNWebApi\obj" (
	rmdir /s /q "%userprofile%\Documents\projects\aspnetcore\LCPSNWebApi\obj"
)

echo Cleaned up all files from obj and bin directories!

pause
exit /b %errorlevel%
endlocal