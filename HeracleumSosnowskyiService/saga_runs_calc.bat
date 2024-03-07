@ECHO off & CHCP 866 > NUL
SETLOCAL ENABLEDELAYEDEXPANSION

REM Добавим SAGA_GIS в системную переменную среды.
REM SET path=%path%;C:\Program Files\SAGA-GIS

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

SET NDVI_TIF=%~n1_NDVI_GTR33.TIF
IF NOT EXIST "%NDVI_TIF%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%NDVI_TIF%" ^
 -FORMULA="gt((g5-g4)/(g5+g4), 0.33)"

SET ABI_TIF=%~n1_ABI_IVL.TIF
IF NOT EXIST "%ABI_TIF%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%ABI_TIF%" ^
 -FORMULA="and(and(gt(abs(g3-g2), 1790), lt(abs(g3-g2), 2210)), and(gt(g5, 23000), lt(g5, 30000)))"
 
SET HSI_TIF_ITERVAL=%~n1_HSI_IVL.TIF
IF NOT EXIST "%HSI_TIF_ITERVAL%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%HSI_TIF_ITERVAL%" ^
 -FORMULA="and(gt(g4/abs(g2-g1), 11), lt(g5/abs(g3-g2), 17))"

SET SPLI_SGRD=%~n1_SPLI.sgrd
SET SPLI_TIF=%~n1_SPLI.TIF
IF NOT EXIST "%SPLI_SGRD%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%SPLI_SGRD%" ^
 -FORMULA="and(and(eq(and(gt(g2, 8400), lt(g2, 8605)), 1), eq(and(gt(g3, 10400), lt(g3, 10730)), 1)), and(eq(and(gt(g4, 8750), lt(g4, 9220)), 1), eq(and(gt(g5, 23000), lt(g5, 29500)), 1)))"
IF NOT EXIST "%SPLI_TIF%" saga_cmd io_gdal 2 ^
 -GRIDS="%SPLI_SGRD%" ^
 -FILE="%SPLI_TIF%"
 
IF NOT EXIST "%~n1_POLYGONS.shp" saga_cmd shapes_grid 6 ^
 -GRID="%SPLI_SGRD%" ^
 -POLYGONS="%~n1_POLYGONS.shp" ^
 -CLASS_ALL="0" ^
 -CLASS_ID="1" ^
 -SPLIT="1"

IF NOT EXIST "%~n1_PROPS.shp" saga_cmd shapes_polygons 2 ^
-POLYGONS "%~n1_POLYGONS.shp" ^
-OUTPUT "%~n1_PROPS.shp" ^
-FIELDS  "0,2" ^
-BLENGTH 0 ^
-BAREA 1

REM The ID field is enumerable.
IF NOT EXIST "%~n1_ENUM.shp" saga_cmd table_tools 2 ^
-INPUT "%~n1_PROPS.shp" ^
-ENUM ID ^
-RESULT_SHAPES "%~n1_ENUM.shp"

REM IF NOT EXIST "%~n1_RPL.shp" saga_cmd table_tools 10 ^
REM -TABLE "%~n1_ENUM.shp" ^
REM -FIELD "NAME" ^
REM -OUT_SHAPES "%~n1_RPL.shp" ^
REM -REPLACE "TBL_OF_ATTRS_FOR_RPL.dbf"

IF NOT EXIST "%~n1_PARAMS.shp" saga_cmd shapes_grid 1 ^
-SHAPES "%~n1_ENUM.shp" ^
-GRIDS "%files%" ^
-RESULT "%~n1_PARAMS.shp"