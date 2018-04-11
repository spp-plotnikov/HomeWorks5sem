@echo off
Powershell.exe -executionpolicy remotesigned -File  unblockFile.ps1
Powershell.exe -executionpolicy remotesigned -File  build.ps1 -Configuration "Release"
cd .\FormalLang5sem\bin\Release
.\FormalLang5sem.exe tests
pause