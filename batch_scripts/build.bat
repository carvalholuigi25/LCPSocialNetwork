@echo off
setlocal enableextensions

cd "..\frontend"
npm run build

pause
exit /b %errorlevel%
endlocal