@echo off
setlocal enableextensions

cd "..\frontend"
npm run start_prod

pause
exit /b %errorlevel%
endlocal