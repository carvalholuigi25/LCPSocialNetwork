@echo off
setlocal enableextensions

cd "..\frontend"
npm run rem_dist_n_do_build

pause
exit /b %errorlevel%
endlocal