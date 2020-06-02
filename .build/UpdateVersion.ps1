<#
.SYNOPSIS
    Script to updating project version.
.DESCRIPTION
    Script will update version for all csharp projects.
.PARAMETER mode
    Specify a value for the version
.EXAMPLE
    UpdateVersion.ps1 "1.2.3.4"
#>

[cmdletbinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$version
)

$projectFiles = Get-ChildItem -Path $PSScriptRoot/../**/Info.plist -Recurse -Force

foreach ($file in $projectFiles) {
    Write-Host "Found Info.plist file:" $file.FullName

    $xml = [xml](Get-Content $file)
    [bool]$updated = $false

    # $xml.plist.GetElementsByTagName("CFBundleShortVersionString") | ForEach-Object{
    #     Write-Host "Updating CFBundleShortVersionString to:" $version
    #     $_."#text" = $version

    #     $updated = $true
    # }

    # $xml.plist.GetElementsByTagName("CFBundleVersion") | ForEach-Object{
    #     Write-Host "Updating CFBundleVersion to:" $version
    #     $_."#text" = $version

    #     $updated = $true
    # }

    Select-Xml -xml $xml -XPath "//dict/key[. = 'CFBundleShortVersionString']/following-sibling::string[1]" |
    ForEach-Object{
        $_.Node.InnerXml = $version
        $updated = $true
    }

    Select-Xml -xml $xml -XPath "//dict/key[. = 'CFBundleVersion']/following-sibling::string[1]" |
    ForEach-Object{
        $_.Node.InnerXml = $version
        $updated = $true
    }

    if ($updated) {
        Write-Host "Info.plist file saved"
        $xml.Save($file.FullName)
    } else {
        Write-Host "Version property not found in the Info.plist file"
    }
}