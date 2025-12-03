# 用户认证授权模块 API 测试指南

## 概述
本文档提供用户认证授权模块的完整API测试指南，包括所有端点的请求示例和响应格式。

## 基础信息
- **Base URL**: `http://localhost:5000` 或 `https://localhost:5001`
- **认证方式**: JWT Bearer Token
- **Token有效期**: 24小时
- **RefreshToken有效期**: 7天
- **密码加密**: MD5

---

## API 端点列表

### 1. 管理员登录
**POST** `/api/auth/login`

#### 请求示例
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

#### 响应示例（成功）
```json
{
  "success": true,
  "message": "登录成功",
  "data": {
    "userId": 1,
    "username": "admin",
    "fullName": "系统管理员",
    "email": "admin@hailong.com",
    "role": "admin",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "base64encodedrefreshtoken...",
    "expiresAt": "2025-12-04T02:00:00Z"
  }
}
```

#### 响应示例（失败）
```json
{
  "success": false,
  "message": "用户名或密码错误",
  "data": null
}
```

---

### 2. 刷新Token
**POST** `/api/auth/refresh`

#### 请求示例
```http
POST /api/auth/refresh
Content-Type: application/json

{
  "refreshToken": "your_refresh_token_here"
}
```

#### 响应示例（成功）
```json
{
  "success": true,
  "message": "Token刷新成功",
  "data": {
    "userId": 1,
    "username": "admin",
    "fullName": "系统管理员",
    "email": "admin@hailong.com",
    "role": "admin",
    "token": "new_jwt_token...",
    "refreshToken": "new_refresh_token...",
    "expiresAt": "2025-12-04T02:00:00Z"
  }
}
```

#### 响应示例（失败）
```json
{
  "success": false,
  "message": "刷新令牌无效或已过期",
  "data": null
}
```

---

### 3. 获取当前用户信息
**GET** `/api/auth/me`

#### 请求示例
```http
GET /api/auth/me
Authorization: Bearer your_jwt_token_here
```

#### 响应示例（成功）
```json
{
  "success": true,
  "message": "获取用户信息成功",
  "data": {
    "userId": 1,
    "username": "admin",
    "fullName": "系统管理员",
    "email": "admin@hailong.com",
    "phone": "13800138000",
    "role": "admin",
    "lastLogin": "2025-12-03T10:30:00"
  }
}
```

#### 响应示例（失败）
```json
{
  "success": false,
  "message": "无效的用户令牌",
  "data": null
}
```

---

### 4. 修改密码
**POST** `/api/auth/change-password`

#### 请求示例
```http
POST /api/auth/change-password
Authorization: Bearer your_jwt_token_here
Content-Type: application/json

{
  "oldPassword": "admin123",
  "newPassword": "newpassword123"
}
```

#### 响应示例（成功）
```json
{
  "success": true,
  "message": "密码修改成功，请重新登录",
  "data": true
}
```

#### 响应示例（失败）
```json
{
  "success": false,
  "message": "旧密码错误或用户不存在",
  "data": false
}
```

---

### 5. 登出
**POST** `/api/auth/logout`

#### 请求示例
```http
POST /api/auth/logout
Authorization: Bearer your_jwt_token_here
```

#### 响应示例（成功）
```json
{
  "success": true,
  "message": "登出成功",
  "data": true
}
```

---

### 6. 验证Token
**POST** `/api/auth/validate-token`

#### 请求示例
```http
POST /api/auth/validate-token
Content-Type: application/json

{
  "token": "your_jwt_token_here"
}
```

#### 响应示例
```json
{
  "success": true,
  "message": "Token有效",
  "data": true
}
```

---

## 使用Postman测试

### 1. 设置环境变量
创建以下环境变量：
- `base_url`: `http://localhost:5000`
- `token`: (登录后自动设置)
- `refresh_token`: (登录后自动设置)

### 2. 登录并保存Token
在登录请求的Tests标签中添加：
```javascript
if (pm.response.code === 200) {
    var jsonData = pm.response.json();
    pm.environment.set("token", jsonData.data.token);
    pm.environment.set("refresh_token", jsonData.data.refreshToken);
}
```

### 3. 在需要认证的请求中使用Token
在Authorization标签中选择：
- Type: `Bearer Token`
- Token: `{{token}}`

---

## 使用cURL测试

### 登录
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'
```

### 获取用户信息
```bash
curl -X GET http://localhost:5000/api/auth/me \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### 刷新Token
```bash
curl -X POST http://localhost:5000/api/auth/refresh \
  -H "Content-Type: application/json" \
  -d '{"refreshToken":"YOUR_REFRESH_TOKEN_HERE"}'
```

### 修改密码
```bash
curl -X POST http://localhost:5000/api/auth/change-password \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -H "Content-Type: application/json" \
  -d '{"oldPassword":"admin123","newPassword":"newpassword123"}'
```

---

## 错误代码说明

| HTTP状态码 | 说明 |
|-----------|------|
| 200 | 请求成功 |
| 400 | 请求参数错误 |
| 401 | 未授权或Token无效 |
| 404 | 资源不存在 |
| 500 | 服务器内部错误 |

---

## 安全注意事项

1. **Token存储**: 前端应将Token存储在HttpOnly Cookie或安全的本地存储中
2. **HTTPS**: 生产环境必须使用HTTPS
3. **密码强度**: 建议密码至少6位，包含字母和数字
4. **Token刷新**: Token过期前应使用RefreshToken刷新
5. **修改密码**: 修改密码后会自动撤销RefreshToken，需要重新登录

---

## 测试账户

默认管理员账户：
- 用户名: `admin`
- 密码: `admin123`
- 角色: `admin`

**注意**: 首次使用请运行数据库迁移脚本 `SQL/auth_module_migration.sql`