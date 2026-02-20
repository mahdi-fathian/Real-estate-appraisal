# سامانه سیماب (Simab System)

سامانه سیماب در راستای مکانیزه کردن ارزیابی املاک و ساختمان‌های بانک، وثایق ملکی تسهیلات اعطایی به مشتریان بانک و همچنین امکان ارائه اطلاعات ارزیابی بین سرویس‌های مختلف بانک را فراهم می‌سازد.

## ویژگی‌های کلیدی

- نگهداری کلیه سوابق، اسناد و مدارک مربوط به هر ارزیابی بطور جداگانه
- دارا بودن کارتابل ارجاع امور
- تعریف سلسله مراتب جهت ارجاع کار
- کنترل محدودیت‌های ارجاع (مکانی – تعدادی – زمانی)
- دریافت اطلاعات به صورت سرویس از سامانه‌های بانک
- ارسال اطلاعات به صورت سرویس به سامانه‌های بانک
- ورود اطلاعات تمامی انواع گزارش‌های ارزیابی رایج در بانک با تمام جزئیات
- دارا بودن نسخه تحت وب و موبایل
- ورود اطلاعات به صورت آفلاین در نسخه موبایل و سینک کردن در زمان اتصال به اینترنت
- ثبت موقعیت مکانی محل بازدید با امکان کنترل صحت مکان بر اساس سند ملک
- اعلام وضعیت گردش کار به مشتری به صورت پیامک و نموداری در پرتال بانک
- تنظیم زمان بازدید، اعلام به مشتری و اخذ تاییدیه مشتری
- اخذ بازخورد رفتار ارزیاب از مشتری
- محاسبه فاصله محل ارزیابی با محل اعزام ارزیاب
- نگهداری عکس و فیلم بازدید و قابلیت یادداشت صوتی
- امضای دیجیتال گزارش ارزیابی
- رمزگذاری گزارش‌های ارزیابی
- اخذ اطلاعات شهرداری و ثبت به صورت سرویس
- تعریف لایه‌های مختلف نقشه به عنوان مثال بافت فرسوده و ناکارآمد شهری برای تشخیص وقوع املاک

## معماری پروژه

این پروژه بر اساس استانداردهای **NavaKit** و **Clean Architecture** طراحی شده است:

### ساختار پروژه

```
src/
├── Simab.Api/              # API Layer (Presentation)
├── Simab.Application/      # Application Layer (Use Cases)
├── Simab.Domain/           # Domain Layer (Business Logic)
└── Simab.Infrastructure/   # Infrastructure Layer (Data Access, External Services)

tests/
└── Simab.Tests/            # Unit & Integration Tests
```

### لایه‌ها

#### Domain Layer
- **Entities**: Evaluation, Property, Evaluator, Visit, Document, Workflow
- **Value Objects**: Money, Location
- **Domain Events**: EvaluationCreatedEvent, EvaluationCompletedEvent
- **Enums**: EvaluationStatus, PropertyType, VisitStatus, etc.

#### Application Layer
- **Commands**: CreateEvaluation, ScheduleVisit
- **Queries**: GetEvaluation
- **Handlers**: Command/Query Handlers
- **Validators**: FluentValidation validators
- **DTOs**: Data Transfer Objects

#### Infrastructure Layer
- **Persistence**: EF Core DbContext, Repositories
- **Configurations**: Entity configurations

#### API Layer
- **Controllers**: RESTful API endpoints
- **Middlewares**: Exception handling, logging

## تکنولوژی‌ها

- **.NET 8.0**
- **Entity Framework Core 8.0**
- **MediatR** (CQRS Pattern)
- **FluentValidation**
- **AutoMapper**
- **SQL Server** (LocalDB)

## راه‌اندازی

### پیش‌نیازها

- .NET 8.0 SDK
- SQL Server LocalDB
- Visual Studio 2022 یا VS Code

### نصب وابستگی‌ها

```bash
dotnet restore
```

### تنظیمات دیتابیس

Connection String در فایل `appsettings.json` تنظیم شده است. نام دیتابیس: **SimabDb**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimabDb;Integrated Security=True;..."
  }
}
```

**نکته**: دیتابیس به صورت خودکار در اولین اجرای برنامه ایجاد می‌شود (Development mode).

### اجرای Migration (اختیاری - برای Production)

برای استفاده از Migrations در Production:

```bash
# نصب ابزار EF Core (یک بار)
dotnet tool install --global dotnet-ef

# ایجاد Migration
dotnet ef migrations add InitialCreate --project src/Simab.Infrastructure --startup-project src/Simab.Api

# اعمال Migration به دیتابیس
dotnet ef database update --project src/Simab.Infrastructure --startup-project src/Simab.Api
```

یا استفاده از اسکریپت‌های PowerShell:

```powershell
# Build پروژه
.\scripts\build.ps1

# ایجاد Migration جدید
.\scripts\database.ps1 add MigrationName

# اعمال Migration
.\scripts\database.ps1 update
```

### اجرای پروژه

```bash
dotnet run --project src/Simab.Api
```

API در آدرس `https://localhost:5001` یا `http://localhost:5000` در دسترس خواهد بود.

### Swagger UI

پس از اجرای پروژه، می‌توانید از Swagger UI استفاده کنید:
- `https://localhost:5001/swagger`

## استانداردهای کدنویسی

این پروژه از استانداردهای زیر پیروی می‌کند:

- **Clean Code**: نام‌گذاری واضح، متدهای کوتاه، مسئولیت واحد
- **SOLID Principles**: Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion
- **DDD**: Domain-Driven Design با Entities, Value Objects, Domain Events
- **CQRS**: جداسازی Command و Query
- **Clean Architecture**: جداسازی لایه‌ها و Dependency Rule

## تست‌ها

```bash
dotnet test
```

## اسکریپت‌های کمکی

در پوشه `scripts` اسکریپت‌های PowerShell موجود است:

- **build.ps1**: Build کامل پروژه
- **database.ps1**: مدیریت Migration‌ها و دیتابیس

## ساختار دیتابیس

پس از اجرای برنامه، دیتابیس **SimabDb** با جداول زیر ایجاد می‌شود:

- **Evaluations**: ارزیابی‌های ملک
- **Properties**: اطلاعات املاک
- **Evaluators**: اطلاعات ارزیابان
- **Visits**: بازدیدهای انجام شده
- **VisitMedia**: فایل‌های رسانه‌ای بازدیدها (عکس، فیلم، صدا)
- **Documents**: اسناد و مدارک ارزیابی
- **Workflows**: گردش کار و ارجاع امور



.
