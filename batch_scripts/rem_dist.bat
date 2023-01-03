@echo off
setlocal enableextensions

cd "..\frontend"
npm run rem_dist

pause
exit /b %errorlevel%
endlocal