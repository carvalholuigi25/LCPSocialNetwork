@echo off
setlocal enableextensions

cd "..\frontend"
npm run start_all

pause
exit /b %errorlevel%
endlocal