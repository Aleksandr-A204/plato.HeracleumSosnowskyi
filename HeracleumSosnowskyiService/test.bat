@echo off & chcp 866 > nul
setlocal ENABLEDELAYEDEXPANSION

set path=%path%;C:\Program Files\SAGA-GIS

cd %1

REM IF EXIST %1 
	REM for /F "usebackq" %%a in (`dir /D /B`) do (
		REM set test=%%a
		REM saga_cmd io_gdal 0 -FILES="%%a" -GRIDS="!test:~0,-4!.sgrd"
	REM )
REM )

set files=
FOR /R %%I in (*.sgrd) do (
	if defined files (
		set files=!files!;%%~nxI
	) else (
		set files=%%~nxI
	)
)

echo %~1/HSI.sgrd

saga_cmd grid_calculus 1 -GRIDS="%files%" -RESULT="%~1/HSI.sgrd" -FORMULA="g3/abs(g2-g1)"

REM IF EXIST %1 (
	REM for /F "usebackq" %%a in (`dir /D /B`) do (
		REM set test=%%a
		REM echo !test!;!test:~0,-6!
		REM REM saga_cmd grid_calculus 1 -GRIDS="!t!;!t:~0,-10!3.TIF.sgrd" -RESULT="!t:~0,-11!NDVI.sgrd" -FORMULA="(g1 - g2)"
	REM )
REM )
