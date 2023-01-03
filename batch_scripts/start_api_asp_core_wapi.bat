@echo off
setlocal enableextensions

cd "..\frontend"
npm run server_api

pause
exit /b %errorlevel%
endlocal