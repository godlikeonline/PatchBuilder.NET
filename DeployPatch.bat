@ECHO OFF

IF "%1"=="" GOTO HELP
IF "%1"=="/?" GOTO HELP

SET SOURCE_PATH=.\patchfiles
SET TARGET_ROOT=%1

SETLOCAL EnableDelayedExpansion

FOR /F %%A IN (FilesToDeploy.txt) DO (
	FOR /F %%B IN (%%A.TargetDirectories.txt) DO (
		REM Set other config properties
		ECHO Copy files to !TARGET_ROOT!\%%B
		XCOPY /e/v/y/k/I !SOURCE_PATH!\%%A !TARGET_ROOT!\%%B
	)
)

GOTO FINISH

:HELP
ECHO Usage: DeployPatch Target
ECHO Target - Location of website root Eg: C:\PRODUCTION

:FINISH
ECHO End batch