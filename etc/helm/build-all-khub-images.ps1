#!/usr/bin/env pwsh
param (
    [string]$Version = "auto",
    [string]$Registry = "longnguyen1331/khub",
    [string]$Platform = "linux/amd64",
    # Comma-separated repo-relative paths, e.g. "services/masterdata,apps/web". CI fills this from commit message.
    [string]$PathsSpec = "",
    # Build every image (CI uses this for [All] commits).
    [switch]$All
)

# Repo folder path (as in commit message) -> internal service key for khhub/<service>-latest
$script:RepoPathToService = @{
    "services/administration"  = "administration"
    "services/identity"        = "identity"
    "services/audit-logging"   = "auditlogging"
    "services/gdpr"            = "gdpr"
    "services/ai-management"   = "aimanagement"
    "services/language"        = "language"
    "services/masterdata"      = "masterdata"
    "gateways/web"             = "webgateway"
    "gateways/mobile"          = "mobilegateway"
    "apps/auth-server"         = "authserver"
    "apps/web"                 = "web"
}

function Normalize-RepoPath([string]$raw) {
    $p = $raw.Trim().Trim('/', '\').Replace('\', '/')
    return $p.ToLowerInvariant()
}

# Local image tag for build-image.ps1 / values.localdev (not pushed to registry).
if ($Version -eq "auto") {
    $Version = Get-Date -Format "yyyyMMdd.HHmmss"
}

$currentFolder = $PSScriptRoot

$allImages = @(
    @{ ProjectPath = "../../services/administration/KHHub.AdministrationService/KHHub.AdministrationService.csproj"; Service = "administration" },
    @{ ProjectPath = "../../services/identity/KHHub.IdentityService/KHHub.IdentityService.csproj"; Service = "identity" },
    @{ ProjectPath = "../../services/audit-logging/KHHub.AuditLoggingService/KHHub.AuditLoggingService.csproj"; Service = "auditlogging" },
    @{ ProjectPath = "../../services/gdpr/KHHub.GdprService/KHHub.GdprService.csproj"; Service = "gdpr" },
    @{ ProjectPath = "../../services/ai-management/KHHub.AIManagementService/KHHub.AIManagementService.csproj"; Service = "aimanagement" },
    @{ ProjectPath = "../../services/language/KHHub.LanguageService/KHHub.LanguageService.csproj"; Service = "language" },
    @{ ProjectPath = "../../gateways/web/KHHub.WebGateway/KHHub.WebGateway.csproj"; Service = "webgateway" },
    @{ ProjectPath = "../../apps/auth-server/KHHub.AuthServer/KHHub.AuthServer.csproj"; Service = "authserver" },
    @{ ProjectPath = "../../apps/web/KHHub.Web/KHHub.Web.csproj"; Service = "web" },
    @{ ProjectPath = "../../gateways/mobile/KHHub.MobileGateway/KHHub.MobileGateway.csproj"; Service = "mobilegateway" },
    @{ ProjectPath = "../../services/masterdata/KHHub.MasterDataService/KHHub.MasterDataService.csproj"; Service = "masterdata" }
)

if ($All) {
    $images = $allImages
}
elseif (-not [string]::IsNullOrWhiteSpace($PathsSpec)) {
    $tokens = $PathsSpec.Split(',', [System.StringSplitOptions]::RemoveEmptyEntries) | ForEach-Object { $_.Trim() } | Where-Object { $_ }
    $wanted = [ordered]@{}
    foreach ($t in $tokens) {
        $norm = Normalize-RepoPath $t
        if (-not $RepoPathToService.ContainsKey($norm)) {
            Write-Error "Unknown path in -PathsSpec: '$t' (normalized: '$norm'). Use repo-relative segments like services/masterdata or apps/web."
            exit 2
        }
        $svc = $RepoPathToService[$norm]
        if (-not $wanted.Contains($svc)) {
            $wanted[$svc] = $true
        }
    }
    $svcSet = $wanted.Keys
    $images = @($allImages | Where-Object { $svcSet -contains $_.Service })
    if ($images.Count -eq 0) {
        Write-Error "No images to build after filtering."
        exit 2
    }
    Write-Host "Filtered build: $($images.Service -join ', ')" -ForegroundColor Yellow
}
else {
    # Local / no filter: build everything (backward compatible).
    $images = $allImages
}

foreach ($item in $images) {
    $localImage = "khhub/$($item.Service):$Version"
    $remoteLatest = "${Registry}:$($item.Service)-latest"

    Write-Host "Building $localImage -> push $remoteLatest" -ForegroundColor Cyan

    & (Join-Path $currentFolder "build-image.ps1") -ProjectPath $item.ProjectPath -ImageName "khhub/$($item.Service)" -Version $Version -Platform $Platform
    if ($LASTEXITCODE -ne 0) {
        Write-Error "build-image failed for $($item.Service)"
        exit $LASTEXITCODE
    }

    docker tag $localImage $remoteLatest
    if ($LASTEXITCODE -ne 0) {
        Write-Error "docker tag failed: $localImage -> $remoteLatest"
        exit $LASTEXITCODE
    }

    docker push $remoteLatest
    if ($LASTEXITCODE -ne 0) {
        Write-Error "docker push failed: $remoteLatest"
        exit $LASTEXITCODE
    }
}

Write-Host "Pushed $($images.Count) image(s) to $Registry (rolling *-latest tags)." -ForegroundColor Green
