# راهنمای راه‌اندازی سامانه سیماب

## مراحل راه‌اندازی

### 1. نصب پیش‌نیازها

- .NET 8.0 SDK
- SQL Server LocalDB (معمولاً با Visual Studio نصب می‌شود)

### 2. Restore وابستگی‌ها

```powershell
dotnet restore
```

### 3. Build پروژه

```powershell
dotnet build -c Release
```

یا استفاده از اسکریپت:

```powershell
.\scripts\build.ps1
```

### 4. ایجاد دیتابیس

دیتابیس **SimabDb** به صورت خودکار در اولین اجرای برنامه ایجاد می‌شود.

برای ایجاد دستی با Migration:

```powershell
# نصب ابزار EF Core (فقط یک بار)
dotnet tool install --global dotnet-ef

# ایجاد Migration
.\scripts\database.ps1 add InitialCreate

# اعمال Migration
.\scripts\database.ps1 update
```

### 5. اجرای برنامه

```powershell
dotnet run --project src\Simab.Api\Simab.Api.csproj
```

یا در Visual Studio: F5

### 6. دسترسی به API

- **Swagger UI**: https://localhost:5001/swagger
- **API Base URL**: https://localhost:5001/api/v1

## تست API

### ایجاد ارزیابی جدید

```http
POST https://localhost:5001/api/v1/Evaluations
Content-Type: application/json

{
  "propertyId": "00000000-0000-0000-0000-000000000001",
  "evaluatorId": "00000000-0000-0000-0000-000000000002",
  "type": 0,
  "estimatedAmount": 5000000000,
  "currency": "IRR"
}
```

### دریافت ارزیابی

```http
GET https://localhost:5001/api/v1/Evaluations/{id}
```

## ساختار دیتابیس

پس از اجرای برنامه، دیتابیس **SimabDb** با جداول زیر ایجاد می‌شود:

- **Evaluations**: ارزیابی‌های ملک
- **Properties**: اطلاعات املاک  
- **Evaluators**: اطلاعات ارزیابان
- **Visits**: بازدیدهای انجام شده
- **VisitMedia**: فایل‌های رسانه‌ای بازدیدها
- **Documents**: اسناد و مدارک ارزیابی
- **Workflows**: گردش کار و ارجاع امور

## نکات مهم

1. **نام دیتابیس**: SimabDb (در appsettings.json تنظیم شده)
2. **Connection String**: از LocalDB استفاده می‌کند
3. **Development Mode**: دیتابیس به صورت خودکار ایجاد می‌شود
4. **Production**: باید از Migrations استفاده شود

## عیب‌یابی

### خطای Connection String

اگر خطای اتصال به دیتابیس دریافت کردید:

1. مطمئن شوید LocalDB نصب است
2. Connection String را در `appsettings.json` بررسی کنید
3. نام دیتابیس باید `SimabDb` باشد

### خطای Build

```powershell
# پاک کردن build قبلی
dotnet clean
dotnet restore
dotnet build
```
