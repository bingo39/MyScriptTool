@ECHO OFF & CD /D %~DP0
>NUL 2>&1 REG.exe query "HKU\S-1-5-19" || (
    ECHO SET UAC = CreateObject^("Shell.Application"^) > "%TEMP%\Getadmin.vbs"
    ECHO UAC.ShellExecute "%~f0", "%1", "", "runas", 1 >> "%TEMP%\Getadmin.vbs"
    "%TEMP%\Getadmin.vbs"
    DEL /f /q "%TEMP%\Getadmin.vbs" 2>NUL
    Exit /b
)
taskkill /f /im LockHunter.exe >NUL 2>NUL
regsvr32 /s /u LHShellExt64.dll
::Start /Wait /B "" "%~dp0LHService.exe" /uninstall /silent
rd/s/q "%AppData%\LockHunter" 2>NUL
reg delete HKCU\Software\LockHunter /F  >NUL 2>NUL
taskkill /f /im explorer.exe >NUL 2>NUL & start explorer
exit