@echo off
setlocal enableextensions

cd "..\frontend"
npm run start

pause
exit /b %errorlevel%
endlocal