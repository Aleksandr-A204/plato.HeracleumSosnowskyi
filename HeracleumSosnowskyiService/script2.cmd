@echo off
setlocal EnableDelayedExpansion

set path=%path%;C:\Program Files\saga-9.2.0_x64

for /F "usebackq" %%a in (`dir D:\Landsat8 /D /B`) do (
	for /F "usebackq" %%b in (`dir D:\Landsat8\%%a\*B5.TIF /B /S`) do (
		saga_cmd io_gdal 0 -FILES="%%b" -GRIDS="%%b.sgrd")
	for /F "usebackq" %%c in (`dir D:\Landsat8\%%a\*B3.TIF /B /S`) do (
		saga_cmd io_gdal 0 -FILES="%%c" -GRIDS="%%c.sgrd"))
		
for /F "usebackq" %%a in (`dir D:\Landsat8 /D /B`) do (
	for /F "usebackq" %%b in (`dir D:\Landsat8\%%a\*5.TIF.sgrd /B /S`) do (
	set t=%%b
	saga_cmd grid_calculus 1 -GRIDS="!t!;!t:~0,-10!3.TIF.sgrd" -RESULT="!t:~0,-10!NDVI.sgrd" -FORMULA="( g1 - g2 ) / ( g1 + g2 )"
	saga_cmd io_gdal 2 -GRIDS="!t:~0,-10!NDVI.sgrd" -FILE="!t:~0,-10!NDVI.TIF"))
	