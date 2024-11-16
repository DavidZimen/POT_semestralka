@echo off
setlocal


REM Check if an argument is provided
if "%1"=="" (
    echo ERROR: Missing argument. Usage: docker_control.bat -up or -down >&2
    exit /b 1
)

set IMAGE1=pot_semestralka-pot_frontend
set IMAGE2=pot_semestralka-pot_api

REM Handle -up argument
if "%1"=="-up" (
    echo Starting Docker Compose...
    docker-compose --env-file .env up --build -d
    exit /b 0
)

REM Handle -down argument
if "%1"=="-down" (
    echo Stopping Docker Compose...
    docker-compose down

    REM Remove specified images
    echo Removing images...
    docker rmi -f %IMAGE1%
    docker rmi -f %IMAGE2%
    if errorlevel 1 (
        echo ERROR: Failed to remove images. >&2
        exit /b 1
    )

    exit /b 0
)

REM If invalid argument is provided
echo ERROR: Invalid argument. Use -up or -down. >&2
exit /b 1