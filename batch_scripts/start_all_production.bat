@echo off
setlocal enableextensions

cd "..\frontend"
npm run start_all_p

pause
exit /b %errorlevel%
endlocal