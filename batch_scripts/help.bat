@echo off
setlocal enableextensions

cd "..\frontend"
Powershell -Nop -C "(Get-Content .\package.json|ConvertFrom-Json).scripts"

pause
exit /b %errorlevel%
endlocal