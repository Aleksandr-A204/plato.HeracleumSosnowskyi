@echo off & chcp 866 > nul
setlocal ENABLEDELAYEDEXPANSION

curl --help

REM set path=%path%;C:\Program Files\saga-9.2.0_x64

REM cd %1
REM echo %cd%

REM IF EXIST %1 (
	REM for /F "usebackq" %%a in (`dir /D /B`) do (
		REM echo %%a
		REM REM saga_cmd io_gdal 0 -FILES="%%a" -GRIDS="%%a.sgrd"
	REM )
REM )