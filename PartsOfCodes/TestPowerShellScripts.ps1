$folder = "D:\Temp\1111";
Write-Output "$folder\aaa.txt";

$content = ConvertTo-Html -Body "$folder\aaa.html";
Write-Output "$content";
<# | Out-Null#>
New-Item -Path "$folder\aaa.txt" -ItemType File -Value "$folder\aaa.txt" -Force | Out-Null;
New-Item -Force -Path "$folder\aaa.html" -ItemType File -Value "$content" | Out-Null;

<#
Remove-Item -Path "$folder\*.*";
#>

<#
if(Test-Path "$folder\aaa.txt" -PathType Leaf){
    Write-Output "Prepare to delete file - $folder\aaa.html"
    Start-Sleep -s 10
    Remove-Item -Path "$folder\aaa.txt"
}
else
{
    Write-Output "No file to delete"
}
#>
