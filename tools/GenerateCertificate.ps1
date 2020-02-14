[CmdletBinding()]
param (
    [Parameter(Mandatory = $true, Position = 1)]
    [string] $Name,
    [Parameter(Mandatory = $false, Position = 2)]
    [int] $Days = 365,
    [Parameter(Mandatory = $false, Position = 4)]
    [int] $KeySize = 2048
)

$cert = $Name + ".cer"
$key = $name + ".key"
$pfx = $name + ".pfx"
openssl req -newkey rsa:$KeySize -nodes -keyout $key -x509 -days $Days -out $cert
openssl pkcs12 -export -in $cert -inkey $key -out $pfx