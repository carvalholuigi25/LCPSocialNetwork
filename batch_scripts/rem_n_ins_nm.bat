@echo off
setlocal enableextensions

cd "..\frontend"
npm run rem_n_ins_nm

pause
exit /b %errorlevel%
endlocal