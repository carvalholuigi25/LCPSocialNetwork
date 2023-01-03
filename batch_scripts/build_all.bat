@echo off
setlocal enableextensions

cd "..\frontend"
npm run build_all

pause
exit /b %errorlevel%
endlocal