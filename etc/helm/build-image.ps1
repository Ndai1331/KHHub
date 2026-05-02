param (
    $ProjectPath,
    $ImageName,
    $Version="auto",
    $ProjectType="dotnet",
    # e.g. linux/amd64 for images that run on x64 Linux (build from Apple Silicon, etc.)
    [string]$Platform = ""
)

if ($Version -eq 'auto') {
    $Version = Get-Date -Format "yyyyMMdd.HHmmss"
}

$currentFolder = $PSScriptRoot
$relativeProjectFolder = [System.IO.Path]::GetExtension($ProjectPath) -eq "" ? $ProjectPath : [System.IO.Path]::GetDirectoryName($ProjectPath)
$projectFolder = Join-Path $currentFolder $relativeProjectFolder

Set-Location $projectFolder

try
{
    $projectFileName = [System.IO.Path]::GetFileName($ProjectPath)

    if($ProjectType -eq "dotnet")
    {
        Write-Host "Publishing Dotnet Project: ${projectFileName}" -ForegroundColor Green -BackgroundColor Black

        $rid = $null
        switch ($Platform.ToLowerInvariant()) {
            "linux/amd64" { $rid = "linux-x64" }
            "linux/arm64" { $rid = "linux-arm64" }
        }

        if ($rid) {
            # Same output folder as Dockerfile COPY (bin/Release/net10.0/publish/)
            Write-Host "dotnet publish RID: $rid (Docker --platform $Platform)" -ForegroundColor DarkCyan
            dotnet publish -c Release -r $rid --self-contained false -o bin/Release/net10.0/publish
        }
        else {
            dotnet publish -c Release
        }

        if (-Not $?) {
            Write-Error "Publishing Dotnet Project failed: $projectFileName"
            exit $LASTEXITCODE
        }
    }

    Write-Host "Building Docker Image: ${ImageName}" -ForegroundColor Green -BackgroundColor Black
    $Env:DOCKER_SCAN_SUGGEST="false"
    if (-not [string]::IsNullOrWhiteSpace($Platform)) {
        docker build --platform $Platform . -f Dockerfile -t ${ImageName}:${Version}
    }
    else {
        docker build . -f Dockerfile -t ${ImageName}:${Version}
    }

    # BEGIN: UPDATE values.localdev.yaml

    $splitImageName = $ImageName -split '/'
    $subChartName = $splitImageName[1]

    # Full path of the values.localdev.yaml file
    $localDevFilePath = Join-Path $PSScriptRoot "khhub/values.localdev.yaml"

    # If the values.localdev.yaml file does not exist, create it first
    if (!(Test-Path $localDevFilePath)) {
        New-Item -ItemType File -Path $localDevFilePath | Out-Null
    }

    # Content to write to the file.
    $chartSectionContent = @"
${subChartName}:
  image:
    tag: "${Version}"
"@

    # Read the existing content from the file
    $existingContent = Get-Content -Path $localDevFilePath -Raw

    # Check if the existing content contains the service name, and replace it if found.
    if ($existingContent -match "${subChartName}:") {
        $existingContent = $existingContent -replace "(?s)${subChartName}:.*?tag:.`".*?`"", $chartSectionContent.Trim()
    } else {
        if(![string]::IsNullOrEmpty($existingContent)) {
            $existingContent += "`n"
        }
        $existingContent += $chartSectionContent
    }

    # Write the updated content to the file.
    [System.IO.File]::WriteAllText($localDevFilePath, $existingContent)

    # END: UPDATE values.localdev.yaml
}
finally
{
    Set-Location $currentFolder    
}
exit $LASTEXITCODE