@echo off
echo Building Bank Management System UI...

cd /d "c:\Users\vmusam\source\repos\BankManagementSystem"

echo Building Bank.Model...
cd Bank.Model
msbuild Bank.Model.csproj /p:Configuration=Debug
if %errorlevel% neq 0 goto error

echo Building Bank.Exception...
cd ..\Bank.Exception
msbuild Bank.Exception.csproj /p:Configuration=Debug
if %errorlevel% neq 0 goto error

echo Building Bank.DAO...
cd ..\Bank.DAO
msbuild Bank.DAO.csproj /p:Configuration=Debug
if %errorlevel% neq 0 goto error

echo Building Bank.Service...
cd ..\Bank.Service
msbuild Bank.Service.csproj /p:Configuration=Debug
if %errorlevel% neq 0 goto error

echo Building BankUI...
cd ..\BankUI
msbuild BankUI.csproj /p:Configuration=Debug
if %errorlevel% neq 0 goto error

echo Build completed successfully!
echo Starting Bank Management System UI...
start bin\Debug\BankUI.exe

goto end

:error
echo Build failed!
pause

:end