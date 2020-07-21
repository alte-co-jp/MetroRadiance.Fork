@ECHO OFF
SETLOCAL enabledelayedexpansion
SET BAT_FOLDER=%~d0%~p0
SET DATE_TIME=%DATE:/=%_%TIME::=%
SET DATE_TIME=%DATE_TIME: =0%
SET SRC_FOLDER=%BAT_FOLDER%..\source
SET WORK_FOLDERNAME=Package_%DATE_TIME%
SET WORK_FOLDER=%BAT_FOLDER%%WORK_FOLDERNAME%
SET CERT_SUBJECT_NAME=nishy software

COLOR F

ECHO signtool: %SIGNTOOL%
IF NOT EXIST "%SIGNTOOL%" (
    ECHO Pease SET SIGNTOOL=sign batch file path
    COLOR 6F
    PAUSE
    COLOR F
    GOTO END
)

mkdir "%WORK_FOLDER%"
PUSHD "%SRC_FOLDER%"

REM  Rebuild modules
ECHO =======================
ECHO Rebuild modules
ECHO -----------------------
dotnet.exe build -c Release --no-incremental --no-dependencies MetroRadiance.Core\MetroRadiance.Core.csproj
IF ERRORLEVEL 1 GOTO ERROR
dotnet.exe build -c Release --no-incremental --no-dependencies MetroRadiance.Chrome\MetroRadiance.Chrome.csproj
IF ERRORLEVEL 1 GOTO ERROR
dotnet.exe build -c Release --no-incremental --no-dependencies MetroRadiance\MetroRadiance.csproj
IF ERRORLEVEL 1 GOTO ERROR
dotnet.exe build -c Release --no-incremental --no-dependencies ..\samples\MetroRadiance.Showcase\MetroRadiance.Showcase.csproj
IF ERRORLEVEL 1 GOTO ERROR


REM  Backup unsigned modules
ECHO =======================
ECHO Backup unsigned modules
ECHO -----------------------

MKDIR "%WORK_FOLDER%\net45"
MKDIR "%WORK_FOLDER%\net452"
MKDIR "%WORK_FOLDER%\netcoreapp3.1"

XCOPY /D "MetroRadiance.Core\bin\Release\net45\MetroRadiance.Core.*" "%WORK_FOLDER%\net45\" > nul
IF ERRORLEVEL 1 GOTO ERROR
XCOPY /D "MetroRadiance.Core\bin\Release\netcoreapp3.1\MetroRadiance.Core.*" "%WORK_FOLDER%\netcoreapp3.1\" > nul
IF ERRORLEVEL 1 GOTO ERROR

XCOPY /D "MetroRadiance.Chrome\bin\Release\net45\MetroRadiance.Chrome.*" "%WORK_FOLDER%\net45\" > nul
IF ERRORLEVEL 1 GOTO ERROR
XCOPY /D "MetroRadiance.Chrome\bin\Release\netcoreapp3.1\MetroRadiance.Chrome.*" "%WORK_FOLDER%\netcoreapp3.1\" > nul
IF ERRORLEVEL 1 GOTO ERROR

XCOPY /D "MetroRadiance\bin\Release\net45\MetroRadiance.*" "%WORK_FOLDER%\net45\" > nul
IF ERRORLEVEL 1 GOTO ERROR
XCOPY /D "MetroRadiance\bin\Release\netcoreapp3.1\MetroRadiance.*" "%WORK_FOLDER%\netcoreapp3.1\" > nul
IF ERRORLEVEL 1 GOTO ERROR

XCOPY /D "..\samples\MetroRadiance.Showcase\bin\Release\net452\MetroRadiance.Showcase.*" "%WORK_FOLDER%\net452\" > nul
IF ERRORLEVEL 1 GOTO ERROR
XCOPY /D "..\samples\MetroRadiance.Showcase\bin\Release\netcoreapp3.1\MetroRadiance.Showcase.*" "%WORK_FOLDER%\netcoreapp3.1\" > nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "%WORK_FOLDER%\net45\MetroRadiance.Core.dll" "%WORK_FOLDER%\MetroRadiance.Core.net45_unsinged.dll" > nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "%WORK_FOLDER%\netcoreapp3.1\MetroRadiance.Core.dll" "%WORK_FOLDER%\MetroRadiance.Core.net31_unsigned.dll" > nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "%WORK_FOLDER%\net45\MetroRadiance.Chrome.dll" "%WORK_FOLDER%\MetroRadiance.Chrome.net45_unsinged.dll" > nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "%WORK_FOLDER%\netcoreapp3.1\MetroRadiance.Chrome.dll" "%WORK_FOLDER%\MetroRadiance.Chrome.net31_unsinged.dll" > nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "%WORK_FOLDER%\net45\MetroRadiance.dll" "%WORK_FOLDER%\MetroRadiance.net45_unsinged.dll" > nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "%WORK_FOLDER%\netcoreapp3.1\MetroRadiance.dll" "%WORK_FOLDER%\MetroRadiance.net31_unsinged.dll" > nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "%WORK_FOLDER%\net452\MetroRadiance.Showcase.exe" "%WORK_FOLDER%\MetroRadiance.Showcase.net452_unsinged.exe" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "%WORK_FOLDER%\netcoreapp3.1\MetroRadiance.Showcase.exe" "%WORK_FOLDER%\MetroRadiance.Showcase.net31_unsinged.exe" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "%WORK_FOLDER%\netcoreapp3.1\MetroRadiance.Showcase.dll" "%WORK_FOLDER%\MetroRadiance.Showcase.net31_unsinged.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR


REM  Signe modules
ECHO =======================
ECHO Signe modules
ECHO -----------------------
CALL "%SIGNTOOL%" "MetroRadiance.Core\bin\Release\net45\MetroRadiance.Core.dll" "MetroRadiance.Core\bin\Release\netcoreapp3.1\MetroRadiance.Core.dll" "MetroRadiance.Chrome\bin\Release\net45\MetroRadiance.Chrome.dll" "MetroRadiance.Chrome\bin\Release\netcoreapp3.1\MetroRadiance.Chrome.dll" "MetroRadiance\bin\Release\net45\MetroRadiance.dll" "MetroRadiance\bin\Release\netcoreapp3.1\MetroRadiance.dll" "..\samples\MetroRadiance.Showcase\bin\Release\net452\MetroRadiance.Showcase.exe" "..\samples\MetroRadiance.Showcase\bin\Release\netcoreapp3.1\MetroRadiance.Showcase.exe" "..\samples\MetroRadiance.Showcase\bin\Release\netcoreapp3.1\MetroRadiance.Showcase.dll"


REM  Backup signed modules
ECHO =====================
ECHO Backup signed modules
ECHO -----------------------

COPY "MetroRadiance.Core\bin\Release\net45\MetroRadiance.Core.dll" "%WORK_FOLDER%\MetroRadiance.Core.net45_singed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "MetroRadiance.Core\bin\Release\netcoreapp3.1\MetroRadiance.Core.dll" "%WORK_FOLDER%\MetroRadiance.Core.net31_signed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "MetroRadiance.Chrome\bin\Release\net45\MetroRadiance.Chrome.dll" "%WORK_FOLDER%\MetroRadiance.Chrome.net45_singed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "MetroRadiance.Chrome\bin\Release\netcoreapp3.1\MetroRadiance.Chrome.dll" "%WORK_FOLDER%\MetroRadiance.Chrome.net31_singed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "MetroRadiance\bin\Release\net45\MetroRadiance.dll" "%WORK_FOLDER%\MetroRadiance.net45_singed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "MetroRadiance\bin\Release\netcoreapp3.1\MetroRadiance.dll" "%WORK_FOLDER%\MetroRadiance.net31_singed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR

COPY "..\samples\MetroRadiance.Showcase\bin\Release\net452\MetroRadiance.Showcase.exe" "%WORK_FOLDER%\MetroRadiance.Showcase.net452_singed.exe" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "..\samples\MetroRadiance.Showcase\bin\Release\netcoreapp3.1\MetroRadiance.Showcase.exe" "%WORK_FOLDER%\MetroRadiance.Showcase.net31_singed.exe" >nul
IF ERRORLEVEL 1 GOTO ERROR
COPY "..\samples\MetroRadiance.Showcase\bin\Release\netcoreapp3.1\MetroRadiance.Showcase.dll" "%WORK_FOLDER%\MetroRadiance.Showcase.net31_singed.dll" >nul
IF ERRORLEVEL 1 GOTO ERROR


REM  Packaging
ECHO =======================
ECHO Packaging
ECHO -----------------------

dotnet.exe pack -c Release --no-build -o "%WORK_FOLDER%" MetroRadiance.Core\MetroRadiance.Core.csproj
IF ERRORLEVEL 1 GOTO ERROR
dotnet.exe pack -c Release --no-build -o "%WORK_FOLDER%" MetroRadiance.Chrome\MetroRadiance.Chrome.csproj
IF ERRORLEVEL 1 GOTO ERROR
REM dotnet.exe pack -c Release --no-build -o "%WORK_FOLDER%" MetroRadiance.Chrome.Externals\MetroRadiance.Chrome.Externals.csproj
REM IF ERRORLEVEL 1 GOTO ERROR
dotnet.exe pack -c Release --no-build -o "%WORK_FOLDER%" MetroRadiance\MetroRadiance.csproj
IF ERRORLEVEL 1 GOTO ERROR
dotnet.exe pack -c Release --no-build -o "%WORK_FOLDER%" ..\samples\MetroRadiance.Showcase\MetroRadiance.Showcase.csproj
IF ERRORLEVEL 1 GOTO ERROR


REM  Backup packages
ECHO =======================
ECHO Backup packages
ECHO -----------------------

MKDIR "%WORK_FOLDER%\signed"
FOR %%i in (%WORK_FOLDER%\*.nupkg) do (
    COPY "%%i" "%WORK_FOLDER%\signed" > nul
    REN "%%i" "%%~ni_unsigned%%~xi"
)

REM  Signe packages
ECHO =======================
ECHO Signe packages
ECHO -----------------------

FOR %%i in ("%WORK_FOLDER%\signed\*.nupkg") do (
    echo sign: %%~ni%%~xi
    "%BAT_FOLDER%nuget.exe" sign "%%i" -Verbosity quiet -CertificateSubjectName "%CERT_SUBJECT_NAME%"  -Timestamper "http://sha256timestamp.ws.symantec.com/sha256/timestamp" 
    IF ERRORLEVEL 1 GOTO ERROR
)

GOTO END

:ERROR
ECHO ERROR occurred
COLOR C1
PAUSE
COLOR F

:END
ECHO =======================
ECHO Output folder: "%WORK_FOLDERNAME%"

POPD
ENDLOCAL
EXIT /B