@ECHO off & CHCP 866 > NUL
SETLOCAL ENABLEDELAYEDEXPANSION

REM Добавим SAGA_GIS в системную переменную среды.
SET path=%path%;C:\Program Files\SAGA-GIS-9.2.1

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

SET CSV_SGRD=%~n1_CSV.sgrd
IF NOT EXIST "%CSV_SGRD%" saga_cmd grid_calculus 1 ^
 -GRIDS="%files%" ^
 -RESULT="%CSV_SGRD%" ^
 -FORMULA="and(and(eq(and(gt(g2, 8400), lt(g2, 8605)), 1), eq(and(gt(g3, 10400), lt(g3, 10730)), 1)), and(eq(and(gt(g4, 8750), lt(g4, 9220)), 1), eq(and(gt(g5, 23000), lt(g5, 29500)), 1)))"

REM Convert of raster to vector.
IF NOT EXIST "%~n1_POLYGONS.shp" saga_cmd shapes_grid 6 ^
 -GRID="%CSV_SGRD%" ^
 -POLYGONS="%~n1_POLYGONS.shp" ^
 -CLASS_ALL="0" ^
 -CLASS_ID="1" ^
 -SPLIT="1"

REM Add of area to shp-file.
IF NOT EXIST "%~n1_PROPS.shp" saga_cmd shapes_polygons 2 ^
-POLYGONS "%~n1_POLYGONS.shp" ^
-OUTPUT "%~n1_PROPS.shp" ^
-FIELDS  "0,2" ^
-BLENGTH 0 ^
-BAREA 1

REM The ID field is enumerable.
saga_cmd table_tools 2 ^
-INPUT "%~n1_PROPS.shp" ^
-ENUM ID

IF NOT EXIST "%~n1_PARAMS.shp" saga_cmd shapes_grid 1 ^
-SHAPES "%~n1_PROPS.shp" ^
-GRIDS "%files%" ^
-RESULT "%~n1_PARAMS.shp"

GOTO :eof

IF NOT EXIST "%~n1_RPL.shp" saga_cmd table_tools 10 ^
-TABLE "%~n1_PARAMS.shp" ^
-FIELD NAME ^
-OUT_SHAPES "%~n1_RPL.shp" ^
-REPLACE "%~2\Attributes.Replace.csv"

IF NOT EXIST "%~n1_OUT.shp" saga_cmd table_tools 25 ^
-TABLE "%~n1_RPL.shp" ^
-RESULT "%~n1_OUT.shp" ^
-FIELD NAME ^
-FORMAT "string(NAME) + integer(ID)"
