# wait-for-sql.ps1

$server = "sqlserver"
$port = 1433
$maxAttempts = 30
$attempt = 0

Write-Host "Esperando a que SQL Server esté disponible en" $server ":" $port "..."

while ($attempt -lt $maxAttempts) {
    try {
        $tcpClient = New-Object System.Net.Sockets.TcpClient
        $tcpClient.Connect($server, $port)
        if ($tcpClient.Connected) {
            Start-Sleep -Seconds 4
            Write-Host "✅ SQL Server está listo - arrancando la aplicación .NET"
            $tcpClient.Close()
            break
        }
    } catch {
        Write-Host "⏳ SQL Server aún no está listo - intentando de nuevo..."
    }
    Start-Sleep -Seconds 1
    $attempt++
}

if ($attempt -eq $maxAttempts) {
    Write-Error "❌ SQL Server no está disponible después de $maxAttempts segundos."
    exit 1
}

# Ejecutar la aplicación .NET
& $args[0] $args[1]
