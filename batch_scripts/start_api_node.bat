@echo off
setlocal enableextensions

cd "..\frontend"
npm run server

pause
exit /b %errorlevel%
endlocal