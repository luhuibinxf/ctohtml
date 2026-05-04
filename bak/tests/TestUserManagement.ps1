Write-Host "=== 统计分析系统 - 用户管理功能验证 ===" -ForegroundColor Green
Write-Host ""

# 测试1: 登录系统
Write-Host "1. 测试登录系统" -ForegroundColor Cyan
try {
    $loginParams = @{
        Uri = "http://localhost:9094/login"
        Method = "POST"
        Headers = @{"Content-Type"="application/x-www-form-urlencoded"}
        Body = "username=lhbdb&password=241023"
        UseBasicParsing = $true
    }
    $loginResponse = Invoke-WebRequest @loginParams
    Write-Host "   ✅ 登录成功" -ForegroundColor Green
    Write-Host "   登录响应状态码: $($loginResponse.StatusCode)"
} catch {
    Write-Host "   ❌ 登录失败: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host ""

# 测试2: 获取用户列表
Write-Host "2. 测试获取用户列表" -ForegroundColor Cyan
try {
    $usersParams = @{
        Uri = "http://localhost:9094/get-users"
        Method = "GET"
        UseBasicParsing = $true
    }
    $usersResponse = Invoke-WebRequest @usersParams
    $usersData = $usersResponse.Content | ConvertFrom-Json
    Write-Host "   ✅ 获取用户列表成功" -ForegroundColor Green
    Write-Host "   用户总数: $($usersData.data.Count)"
    Write-Host "   用户列表:"
    foreach ($user in $usersData.data) {
        Write-Host "     - ID: $($user.id), 用户名: $($user.username), 角色: $($user.role), 状态: $($user.status)"
    }
} catch {
    Write-Host "   ❌ 获取用户列表失败: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""

# 测试3: 添加用户
Write-Host "3. 测试添加用户" -ForegroundColor Cyan
try {
    $addUserParams = @{
        Uri = "http://localhost:9094/add-user"
        Method = "POST"
        Headers = @{"Content-Type"="application/json"}
        Body = '{"id": 999, "username": "test999", "password": "123456", "role": "普通用户", "status": "启用"}'
        UseBasicParsing = $true
    }
    $addResponse = Invoke-WebRequest @addUserParams
    $addData = $addResponse.Content | ConvertFrom-Json
    if ($addData.success) {
        Write-Host "   ✅ 添加用户成功" -ForegroundColor Green
        Write-Host "   响应: $($addData.message)"
    } else {
        Write-Host "   ❌ 添加用户失败: $($addData.error)" -ForegroundColor Red
    }
} catch {
    Write-Host "   ❌ 添加用户失败: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""

# 测试4: 确认用户添加成功
Write-Host "4. 测试确认用户添加成功" -ForegroundColor Cyan
try {
    $usersParams = @{
        Uri = "http://localhost:9094/get-users"
        Method = "GET"
        UseBasicParsing = $true
    }
    $usersResponse = Invoke-WebRequest @usersParams
    $usersData = $usersResponse.Content | ConvertFrom-Json
    $newUser = $usersData.data | Where-Object { $_.username -eq "test999" }
    if ($newUser) {
        Write-Host "   ✅ 新用户已添加到列表中" -ForegroundColor Green
        Write-Host "   新用户信息: ID: $($newUser.id), 用户名: $($newUser.username), 角色: $($newUser.role), 状态: $($newUser.status)"
    } else {
        Write-Host "   ❌ 新用户未在列表中显示" -ForegroundColor Red
    }
} catch {
    Write-Host "   ❌ 确认失败: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""

# 测试5: 编辑用户
Write-Host "5. 测试编辑用户" -ForegroundColor Cyan
try {
    $updateUserParams = @{
        Uri = "http://localhost:9094/update-user"
        Method = "POST"
        Headers = @{"Content-Type"="application/json"}
        Body = '{"id": 999, "username": "test999", "password": "654321", "role": "管理员", "status": "启用"}'
        UseBasicParsing = $true
    }
    $updateResponse = Invoke-WebRequest @updateUserParams
    $updateData = $updateResponse.Content | ConvertFrom-Json
    if ($updateData.success) {
        Write-Host "   ✅ 更新用户成功" -ForegroundColor Green
        Write-Host "   响应: $($updateData.message)"
    } else {
        Write-Host "   ❌ 更新用户失败: $($updateData.error)" -ForegroundColor Red
    }
} catch {
    Write-Host "   ❌ 更新用户失败: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""

# 测试6: 删除用户
Write-Host "6. 测试删除用户" -ForegroundColor Cyan
try {
    $deleteUserParams = @{
        Uri = "http://localhost:9094/delete-user"
        Method = "POST"
        Headers = @{"Content-Type"="application/json"}
        Body = '{"id": 999}'
        UseBasicParsing = $true
    }
    $deleteResponse = Invoke-WebRequest @deleteUserParams
    $deleteData = $deleteResponse.Content | ConvertFrom-Json
    if ($deleteData.success) {
        Write-Host "   ✅ 删除用户成功" -ForegroundColor Green
        Write-Host "   响应: $($deleteData.message)"
    } else {
        Write-Host "   ❌ 删除用户失败: $($deleteData.error)" -ForegroundColor Red
    }
} catch {
    Write-Host "   ❌ 删除用户失败: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""

# 测试7: 确认用户删除成功
Write-Host "7. 测试确认用户删除成功" -ForegroundColor Cyan
try {
    $usersParams = @{
        Uri = "http://localhost:9094/get-users"
        Method = "GET"
        UseBasicParsing = $true
    }
    $usersResponse = Invoke-WebRequest @usersParams
    $usersData = $usersResponse.Content | ConvertFrom-Json
    $deletedUser = $usersData.data | Where-Object { $_.username -eq "test999" }
    if (-not $deletedUser) {
        Write-Host "   ✅ 用户已成功从列表中删除" -ForegroundColor Green
    } else {
        Write-Host "   ❌ 用户仍在列表中" -ForegroundColor Red
    }
    Write-Host "   最终用户列表:"
    foreach ($user in $usersData.data) {
        Write-Host "     - ID: $($user.id), 用户名: $($user.username), 角色: $($user.role), 状态: $($user.status)"
    }
} catch {
    Write-Host "   ❌ 确认失败: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== 验证完成 ===" -ForegroundColor Green
Write-Host "所有用户管理功能测试已完成！" -ForegroundColor Green
Write-Host ""