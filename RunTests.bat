@echo off
echo Running Bank Management System Tests...
echo =====================================

cd /d "c:\Users\vmusam\source\repos\BankManagementSystem"

echo Building the solution...
dotnet build BankManagementSystem.sln --configuration Debug

if %ERRORLEVEL% NEQ 0 (
    echo Build failed!
    pause
    exit /b 1
)

echo.
echo Running xUnit Tests...
echo =====================

cd Bank.Test
dotnet test --logger "console;verbosity=detailed"

echo.
echo Test execution completed!
echo ========================
pause