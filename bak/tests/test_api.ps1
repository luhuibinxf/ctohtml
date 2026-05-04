$url = "http://localhost:9094/get-all-options"
try {
    $response = Invoke-WebRequest -Uri $url -Method Get -UseBasicParsing -TimeoutSec 10
    Write-Host "成功 - 状态码: $($response.StatusCode)"
    Write-Host "响应长度: $($response.Content.Length)"
} catch {
    Write-Host "失败 - 异常类型: $($_.Exception.GetType().Name)"
    Write-Host "失败 - 错误消息: $($_.Exception.Message)"
}