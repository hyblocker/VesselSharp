@echo off

:: this script copies the stripped down Vessel Binary to the exports directory once Vessel compiles, for importing into projects using a DLL reference

SET TARGET=win10-x64

echo Compiling Vessel for %TARGET%...
dotnet publish Vessel/Vessel.csproj -r %TARGET% -c Release

echo Ensuring exports exists...

:: ensure exports exists
mkdir exports

echo Exporting Vessel...

:: copy vessel AFTER .NET DOES SOME BLACK MAGIC AND SOMEHOW FUCKING MAKES LIKE IDFK HOW MANY DLLS A SINGLE FUCKING DLL
:: MICROSOFT WHAT THE ACTUAL FUCK IS THIS BLACK MAGIC AND WHY IS EVERYTHING 4 FUCKING MEGABYTES
copy Vessel\bin\x64\Release\netstandard2.0\%TARGET%\Vessel.dll exports\Vessel.dll

echo Done!

pause