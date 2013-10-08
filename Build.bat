@echo off

%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe ".\Accela.Apps.Dev.sln" /t:Build /p:Configuration=Release /distributedFileLogger

if %errorlevel% NEQ 0 goto end

:end
if [%1] NEQ [nopause] pause

