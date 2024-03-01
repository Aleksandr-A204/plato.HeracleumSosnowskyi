@ECHO off & CHCP 866 > NUL
SETLOCAL ENABLEDELAYEDEXPANSION

REM Добавим SAGA_GIS в системную переменную среды.
SET path=%path%;C:\Program Files\SAGA-GIS

PUSHD %1
IF ERRORLEVEL 1 GOTO :eof

SET files=
FOR %%a IN (*B?.tif) DO (
	SET FILENAME_SGRD=%%a.sgrd
	IF DEFINED files (
		SET files=!files!;!FILENAME_SGRD!
	) ELSE (
		SET files=!FILENAME_SGRD!
	)
	IF NOT EXIST !FILENAME_SGRD! saga_cmd io_gdal 0 -FILES="%%a" -GRIDS="!FILENAME_SGRD!"
)

IF NOT DEFINED files ECHO Something went wrong... & GOTO :eof

REM SET NDVI_SGRD=%~1\%~n1_NDVI_GTR33.sgrd
SET NDVI_TIF=%~1\%~n1_NDVI_GTR33.TIF
IF NOT EXIST "%NDVI_TIF%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%NDVI_TIF%" ^
 -FORMULA="gt((g4-g3)/(g4+g3), 0.33)"
REM IF NOT EXIST "%NDVI_TIF%" saga_cmd io_gdal 2 ^
 REM -GRIDS="%NDVI_SGRD%" ^
 REM -FILE="%NDVI_TIF%"

REM SET HSI_SGRD=%~1\%~n1_HSI.sgrd
SET ABI_TIF=%~1\%~n1_ABI_IVL.TIF
IF NOT EXIST "%ABI_TIF%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%ABI_TIF%" ^
 -FORMULA="and(and(gt(abs(g2-g1), 1790), lt(abs(g2-g1), 2210)), and(gt(g4, 23000), lt(g4, 30000)))"
REM IF NOT EXIST "%HSI_TIF%" saga_cmd io_gdal 2 ^
 REM -GRIDS="%HSI_SGRD%" ^
 REM -FILE="%HSI_TIF%"
 
SET HSI_TIF_ITERVAL=%~1\%~n1_HSI_IVL.TIF
IF NOT EXIST "%HSI_TIF_ITERVAL%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%HSI_TIF_ITERVAL%" ^
 -FORMULA="and(gt(g4/abs(g2-g1), 11), lt(g4/abs(g2-g1), 17))"

SET SPLI_SGRD=%~1\%~n1_SPLI.sgrd
SET SPLI_TIF=%~1\%~n1_SPLI.TIF
IF NOT EXIST "%SPLI_SGRD%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%SPLI_SGRD%" ^
 -FORMULA="and(and(eq(and(gt(g1, 8400), lt(g1, 8605)), 1), eq(and(gt(g2, 10400), lt(g2, 10730)), 1)), and(eq(and(gt(g3, 8750), lt(g3, 9220)), 1), eq(and(gt(g4, 23000), lt(g4, 29500)), 1)))"
IF NOT EXIST "%SPLI_TIF%" saga_cmd io_gdal 2 ^
 -GRIDS="%SPLI_SGRD%" ^
 -FILE="%SPLI_TIF%"
 
IF NOT EXIST "%~1\%~n1_SPLI.shp" saga_cmd shapes_grid 6 ^
 -GRID="%SPLI_SGRD%" ^
 -POLYGONS="%~1\%~n1_SPLI.shp" ^
 -CLASS_ALL="0" ^
 -CLASS_ID="1" ^
 -SPLIT="1"
 
 REM IF NOT EXIST "%~1\%~n1_TEST.shp" saga_cmd shapes_grid 6 ^
 REM -GRID="%~1\%~n1_SR_B2.TIF.sgrd" ^
 REM -POLYGONS="%~1\%~n1_TEST.shp" ^
 REM -CLASS_ALL="1" ^
 REM -SPLIT="1"