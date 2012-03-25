pushd
$here = (Split-Path -parent $MyInvocation.MyCommand.Definition)
cd $here

.nuget\NuGet.exe i RavenDb -o packages

# NCrunch does not like when something locks files
# in package folder. So just execute Raven.Server.exe wont work
# Need to copy in temp folder and then run
#$server = (gci -recurse Raven.Server.exe)
#$server | iex

$ServerFolder = (Split-Path -Parent (gci packages -recurse Raven.Server.exe))

Write-Host $ServerFolder

if (!(Test-Path temp)) {
	MkDir temp | Out-Null
}

cpi $ServerFolder temp\ -Recurse

".\temp\server\Raven.Server.exe" | iex

popd