@echo off

:: this script needs https://www.nuget.org/packages/ilmerge

:: set the build folder
SET BUILD_DIR=Vessel\bin\Release\netstandard2.0

:: set your target executable name (typically [projectname].exe)
SET APP_NAME=Vessel.dll

:: Set build, used for directory. Typically Release or Debug
SET ILMERGE_BUILD=Release

:: Set platform, typically x64
SET ILMERGE_PLATFORM=AnyCPU

:: set your NuGet ILMerge Version, this is the number from the package manager install, for example:
:: PM> Install-Package ilmerge -Version 3.0.29
:: to confirm it is installed for a given project, see the packages.config file
SET ILMERGE_VERSION=3.0.41

:: the full ILMerge should be found here:
SET ILMERGE_PATH=%USERPROFILE%\.nuget\packages\ilmerge\%ILMERGE_VERSION%\tools\net452
:: dir "%ILMERGE_PATH%"\ILMerge.exe

echo Merging %APP_NAME% ...

:: add project DLL's starting with replacing the FirstLib with this project's DLL
%ILMERGE_PATH%\ILMerge.exe %BUILD_DIR%\%APP_NAME% ^
  /out:Bin\%ILMERGE_PLATFORM%\%ILMERGE_BUILD%\%APP_NAME%  ^
  /lib:Bin\%ILMERGE_PLATFORM%\%ILMERGE_BUILD%\ ^
  %BUILD_DIR%\Veldrid.dll ^
  %BUILD_DIR%\Veldrid.ImageSharp.dll ^
  %BUILD_DIR%\Veldrid.ImGui.dll ^
  %BUILD_DIR%\Veldrid.MetalBindings.dll ^
  %BUILD_DIR%\Veldrid.OpenGLBindings.dll ^
  %BUILD_DIR%\Veldrid.RenderDoc.dll ^
  %BUILD_DIR%\Veldrid.SDL2.dll ^
  %BUILD_DIR%\Veldrid.StartupUtilities.dll ^
  %BUILD_DIR%\Veldrid.Utilities.dll


:Done
dir %APP_NAME%

pause