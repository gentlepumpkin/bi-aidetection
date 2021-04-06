@echo off
echo Deleting old installer files
CD "%~dp0\Installer"
DEL /f AITOOLSetup*.exe
::FOR /F "usebackq tokens=* skip=0 delims=" %%A IN (`DIR AIToolSetup*.exe /B /O:-D /A:-D`) DO (
::    ECHO Processing: %%~nA
::    DEL "%%A"
::)
