param([string] $Directory)

if($Directory -eq ""){$Directory="."}
#Write-Output "$Directory\SourceFiles\*.*"
#Write-Output "$Directory\"

Copy-Item -Path "$Directory\SourceFiles\*.*" -Destination "$Directory\" -Exclude *.ps1,*.bat,*.backup,*.b