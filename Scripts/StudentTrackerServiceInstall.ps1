# StudentTrackerServiceManager.ps1

$serviceName = "StudentTrackerService"
$displayName = "Student Tracker API Service"
$description = "Runs the Student Tracker backend API"
$exePath = "E:\GP\releases\StudentTracker.APIs.exe"

# Check if running as administrator
if (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Error "Please run this script as an Administrator!"
    Exit
}

# Function to create or update the service
function Set-StudentTrackerService {
    $service = Get-Service -Name $serviceName -ErrorAction SilentlyContinue

    if ($service) {
        Write-Host "Service already exists. Updating..."
        sc.exe config $serviceName binPath= "$exePath --contentRoot CE:\GP\releases"
        Write-Host "Service updated successfully."
    } else {
        Write-Host "Creating new service..."
        New-Service -Name $serviceName -BinaryPathName "$exePath --contentRoot E:\GP\releases" -DisplayName $displayName -Description $description -StartupType Automatic
        Write-Host "Service created successfully."
    }
}

# Function to start the service
function Start-StudentTrackerService {
    Start-Service -Name $serviceName
    Write-Host "Service started."
}

# Function to stop the service
function Stop-StudentTrackerService {
    Stop-Service -Name $serviceName
    Write-Host "Service stopped."
}

# Function to display the service status
function Get-StudentTrackerServiceStatus {
    $service = Get-Service -Name $serviceName
    Write-Host "Service Status: $($service.Status)"
}

# Main script logic
$action = $args[0]

switch ($action) {
    "install" {
        Set-StudentTrackerService
        Start-StudentTrackerService
    }
    "start" {
        Start-StudentTrackerService
    }
    "stop" {
        Stop-StudentTrackerService
    }
    "status" {
        Get-StudentTrackerServiceStatus
    }
    default {
        Write-Host "Usage: .\StudentTrackerServiceManager.ps1 [install|start|stop|status]"
        Write-Host "  install: Creates or updates the service and starts it"
        Write-Host "  start: Starts the service"
        Write-Host "  stop: Stops the service"
        Write-Host "  status: Displays the current status of the service"
    }
}

# Always display the status after any action
Get-StudentTrackerServiceStatus