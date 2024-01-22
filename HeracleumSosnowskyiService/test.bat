@echo off & chcp 866 > nul
setlocal ENABLEDELAYEDEXPANSION

set path=%path%;C:\Program Files\saga-9.2.0_x64

cd %1
echo %cd%

IF EXIST %1 (
	for /F "usebackq" %%a in (`dir /D /B`) do (
		echo %%a
		REM saga_cmd io_gdal 0 -FILES="%%a" -GRIDS="%%a.sgrd"
	)
)