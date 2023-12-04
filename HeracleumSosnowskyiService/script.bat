@echo off
setlocal EnableDelayedExpansion
cd /D "%~dp0" & cd ..
IF NOT EXIST "%cd%\Storage" (mkdir "%cd%\Storage")
cd Storage
REM set path=%path%;C:\Program Files\saga-9.2.0_x64

IF EXIST "%cd%\Landsat8" (
	for /F "usebackq" %%a in (`dir Landsat8 /D /B`) do (
		echo %%a
		for /F "usebackq" %%b in (`dir Landsat8\%%a /D /B`) do (
			echo %%b
		)
	)
) ELSE (
	ECHO Not exists directory Landsat8.
)

	REM for /F "usebackq" %%a in (`dir Landsat8 /D /B`) do (
		REM for /F "usebackq" %%b in (`dir Landsat8\%%a /D /B`) do (
		REM echo %%b)))

pause
cls

REM for /F "usebackq" %%a in (`dir D:\Landsat8 /D /B`) do (
	REM for /F "usebackq" %%b in (`dir D:\Landsat8\%%a\*B5.TIF /B /S`) do (
		REM saga_cmd io_gdal 0 -FILES="%%b" -GRIDS="%%b.sgrd")
	REM for /F "usebackq" %%c in (`dir D:\Landsat8\%%a\*B3.TIF /B /S`) do (
		REM saga_cmd io_gdal 0 -FILES="%%c" -GRIDS="%%c.sgrd"))



		
REM for /F "usebackq" %%a in (`dir D:\Landsat8 /D /B`) do (
	REM for /F "usebackq" %%b in (`dir D:\Landsat8\%%a\*5.TIF.sgrd /B /S`) do (
	REM set t=%%b
	REM saga_cmd grid_calculus 1 -GRIDS="!t!;!t:~0,-10!3.TIF.sgrd" -RESULT="!t:~0,-10!NDVI.sgrd" -FORMULA="g1 - g2 / g1 + g2"
	REM saga_cmd io_gdal 2 -GRIDS="!t:~0,-10!NDVI.sgrd" -FILE="!t:~0,-10!NDVI.TIF"))
	 