@echo off
Powershell.exe -executionpolicy remotesigned -File  build.ps1 -Configuration "Release"
pause