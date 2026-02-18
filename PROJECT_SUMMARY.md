# خلاصه پروژه سامانه سیماب

## ✅ وضعیت پروژه

پروژه با موفقیت Build شده و آماده اجرا است.

## 📁 ساختار پروژه

```
mercury system(simab)/
├── src/
│   ├── Simab.Api/                    # API Layer
│   │   ├── Controllers/              # REST API Controllers
│   │   ├── Middlewares/              # Exception Handling
│   │   ├── Properties/               # launchSettings.json
│   │   ├── appsettings.json          # تنظیمات (DB: SimabDb)
│   │   └── Program.cs                # Startup
│   │
│   ├── Simab.Application/             # Application Layer
│   │   ├── Commands/                 # CQRS Commands
│   │   │   ├── CreateEvaluation/
│   │   │   └── ScheduleVisit/
│   │   ├── Queries/                  # CQRS Queries
│   │   │   └── GetEvaluation/
│   │   ├── Common/
│   │   │   ├── Interfaces/           # Repository Interfaces
│   │   │   ├── Dtos/                 # Data Transfer Objects
│   │   │   └── Mappings/             # AutoMapper Profiles
│   │
│   ├── Simab.Domain/                 # Domain Layer
│   │   ├── Common/                   # Entity, ValueObject, IDomainEvent
│   │   ├── Entities/                 # Domain Entities
│   │   │   ├── Evaluation.cs
│   │   │   ├── Property.cs
│   │   │   ├── Evaluator.cs
│   │   │   ├── Visit.cs
│   │   │   ├── VisitMedia.cs
│   │   │   ├── Document.cs
│   │   │   └── Workflow.cs
│   │   ├── ValueObjects/             # Money, Location
│   │   ├── Enums/                    # Domain Enums
│   │   ├── Events/                   # Domain Events
│   │   └── Exceptions/               # Domain Exceptions
│   │
│   └── Simab.Infrastructure/          # Infrastructure Layer
│       ├── Persistence/
│       │   ├── SimabDbContext.cs     # EF Core DbContext
│       │   ├── Configurations/       # Entity Configurations
│       │   ├── Repositories/         # Repository Implementations
│       │   └── UnitOfWork.cs         # Unit of Work Pattern
│       └── DependencyInjection.cs    # DI Configuration
│
├── tests/
│   └── Simab.Tests/                  # Test Projects
│
├── scripts/
│   ├── build.ps1                     # Build Script
│   └── database.ps1                   # Database Migration Scripts
│
├── MercurySystem.sln                 # Solution File
├── README.md                         # راهنمای اصلی
└── SETUP.md                          # راهنمای راه‌اندازی
```

## 🗄️ دیتابیس

- **نام دیتابیس**: SimabDb
- **نوع**: SQL Server LocalDB
- **Connection String**: در `appsettings.json` تنظیم شده
- **ایجاد خودکار**: در Development mode به صورت خودکار ایجاد می‌شود

## 🏗️ معماری

### Clean Architecture
- ✅ Domain Layer مستقل از سایر لایه‌ها
- ✅ Application Layer فقط به Domain وابسته است
- ✅ Infrastructure Layer پیاده‌سازی Repository و DbContext
- ✅ API Layer فقط با Application کار می‌کند

### CQRS Pattern
- ✅ Commands برای تغییر وضعیت (CreateEvaluation, ScheduleVisit)
- ✅ Queries برای خواندن داده (GetEvaluation)
- ✅ استفاده از MediatR برای پیاده‌سازی

### DDD (Domain-Driven Design)
- ✅ Entities با رفتار (Evaluation, Property, Evaluator, etc.)
- ✅ Value Objects (Money, Location)
- ✅ Domain Events (EvaluationCreatedEvent, EvaluationCompletedEvent)
- ✅ Domain Exceptions

### SOLID Principles
- ✅ Single Responsibility: هر کلاس یک مسئولیت
- ✅ Dependency Inversion: استفاده از Interfaces
- ✅ Repository Pattern برای جداسازی Data Access

## 📦 NuGet Packages

### API Layer
- Microsoft.AspNetCore.OpenApi
- Swashbuckle.AspNetCore
- MediatR
- FluentValidation.AspNetCore
- AutoMapper.Extensions.Microsoft.DependencyInjection

### Application Layer
- MediatR
- FluentValidation
- AutoMapper

### Infrastructure Layer
- Microsoft.EntityFrameworkCore (8.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.0)
- Microsoft.EntityFrameworkCore.Design (8.0.0)
- Dapper

## ✅ استانداردهای رعایت شده

### Clean Code
- ✅ نام‌گذاری واضح و معنادار
- ✅ متدهای کوتاه و تک‌مسئولیتی
- ✅ بدون Magic Numbers
- ✅ کامنت‌های مناسب

### Naming Conventions
- ✅ PascalCase برای کلاس‌ها و متدها
- ✅ camelCase برای متغیرها
- ✅ I prefix برای Interfaces
- ✅ نام‌گذاری مطابق استاندارد NavaKit

### Exception Handling
- ✅ ExceptionHandlingMiddleware برای مدیریت خطاها
- ✅ Domain Exceptions برای خطاهای دامنه
- ✅ Response استاندارد برای خطاها

### Validation
- ✅ FluentValidation برای Command Validation
- ✅ Validation در Application Layer
- ✅ Domain Invariants در Entity Constructors

## 🚀 دستورات اجرا

### Build
```powershell
dotnet build -c Release
# یا
.\scripts\build.ps1
```

### Run
```powershell
dotnet run --project src\Simab.Api\Simab.Api.csproj
```

### Database Migration (اختیاری)
```powershell
.\scripts\database.ps1 add InitialCreate
.\scripts\database.ps1 update
```

## 📝 API Endpoints

### POST /api/v1/Evaluations
ایجاد ارزیابی جدید

### GET /api/v1/Evaluations/{id}
دریافت اطلاعات ارزیابی

### POST /api/v1/Visits
زمان‌بندی بازدید

## ✨ ویژگی‌های پیاده‌سازی شده

1. ✅ نگهداری سوابق ارزیابی
2. ✅ مدیریت اسناد و مدارک
3. ✅ کارتابل ارجاع امور (Workflow)
4. ✅ کنترل محدودیت‌های ارجاع
5. ✅ ثبت موقعیت مکانی
6. ✅ محاسبه فاصله بین مکان‌ها
7. ✅ مدیریت بازدیدها
8. ✅ نگهداری رسانه‌های بازدید (عکس، فیلم، صدا)
9. ✅ بازخورد مشتری
10. ✅ امضای دیجیتال
11. ✅ رمزگذاری گزارش‌ها

## 🔄 مراحل بعدی (برای توسعه)

1. پیاده‌سازی سرویس‌های خارجی (SMS, Email)
2. پیاده‌سازی Authentication/Authorization
3. پیاده‌سازی Event-Driven Architecture (Kafka)
4. پیاده‌سازی نسخه موبایل
5. پیاده‌سازی سینک آفلاین
6. پیاده‌سازی سرویس اطلاعات شهرداری
7. پیاده‌سازی لایه‌های نقشه

## 📊 Build Status

✅ **Build Successful** - 0 Errors, 0 Warnings

تمام پروژه‌ها با موفقیت Build شده‌اند و آماده اجرا هستند.
