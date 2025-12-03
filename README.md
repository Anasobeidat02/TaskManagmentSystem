# Task Management System

نظام شامل لإدارة المهام يتكون من واجهة أمامية حديثة وخادم خلفي قوي.

## نظرة عامة

هذا المشروع عبارة عن تطبيق متكامل لإدارة المهام يسمح للمستخدمين بـ:

- إنشاء وتحديث وحذف المهام
- تعيين الأولويات والحالات للمهام
- إدارة حسابات المستخدمين
- تسجيل الدخول والتسجيل الآمن

## البنية الأساسية للمشروع

```
TaskManagmentSystem/
├── Client/          # التطبيق الأمامي (React + Vite)
└── Server/          # الخادم الخلفي (.NET)
```

---

##  Client (التطبيق الأمامي)

### التقنيات المستخدمة

- **React**: إطار عمل JavaScript
- **Vite**: أداة بناء حديثة
- **Tailwind CSS**: إطار عمل التصميم
- **Axios**: مكتبة لطلبات HTTP
- **ESLint**: أداة فحص الكود

### الهيكل الأساسي

```
Client/
├── src/
│   ├── pages/           # الصفحات الرئيسية
│   │   ├── Login.jsx    # صفحة تسجيل الدخول
│   │   ├── Register.jsx # صفحة التسجيل
│   │   └── Tasks.jsx    # صفحة المهام
│   ├── context/         # React Context
│   │   └── AuthContext.jsx
│   ├── api/             # خدمات API
│   │   └── axios.js
│   ├── App.jsx
│   └── main.jsx
├── index.html
├── package.json
├── vite.config.js
├── tailwind.config.js
├── postcss.config.js
└── eslint.config.js
```

### التثبيت والتشغيل

```bash
cd Client

# تثبيت المتطلبات
npm install

# تشغيل خادم التطوير
npm run dev


```

---

##  Server (الخادم الخلفي)

### التقنيات المستخدمة

- **.NET 10.0**: منصة التطوير
- **Entity Framework Core**: ORM لقاعدة البيانات
- **SQL Server**: قاعدة البيانات
- **JWT**: للمصادقة الآمنة

### البنية المعمارية

```
Server/
├── Domains/          # النماذج والفئات الأساسية
│   ├── Entities/     # كيانات قاعدة البيانات
│   ├── DTOs/         # Data Transfer Objects
│   ├── Enums/        # التعددات
│   └── Interfaces/   # الواجهات
├── Infrastractures/  # طبقة قاعدة البيانات
│   ├── TMSDbContext.cs
│   ├── Hash.cs
│   └── Migrations/
├── Services/         # خدمات العمل
│   ├── UserService.cs
│   ├── TaskItemService.cs
│   └── TokenService.cs
└── WebApi/          # التطبيق الرئيسي
    ├── Controllers/  # معالجات API
    ├── Program.cs
    └── appsettings.json
```

### الكيانات الرئيسية

#### User (المستخدم)

- `Id`: معرّف فريد
- `Email`: البريد الإلكتروني
- `Password`: كلمة المرور
- `FullName`: الاسم الكامل

#### TaskItem (المهمة)

- `Id`: معرّف فريد
- `Title`: عنوان المهمة
- `Description`: وصف المهمة
- `Status`: حالة المهمة
- `Priority`: أولوية المهمة
- `UserId`: معرّف المستخدم
- `CreatedAt`: تاريخ الإنشاء

#### TaskStatus (حالات المهام)

- `Pending`: قيد الانتظار
- `InProgress`: قيد التنفيذ
- `Completed`: مكتملة
- `Cancelled`: ملغاة

#### TaskPriority (أولويات المهام)

- `Low`: منخفضة
- `Medium`: متوسطة
- `High`: عالية
- `Urgent`: حرجة

### التثبيت والتشغيل

```bash
cd Server

# استعادة المتطلبات
dotnet restore

# تطبيق الهجرات على قاعدة البيانات
dotnet ef database update

# تشغيل الخادم
dotnet run

# البناء
dotnet build

# النشر
dotnet publish -c Release
```

### متطلبات البيئة

- **.NET SDK 10.0** أو أحدث
- **SQL Server** أو LocalDB
- تكوين Connection String في `appsettings.json`

---

##  API Endpoints

### المصادقة والمستخدمون

- `POST /api/users/register` - تسجيل مستخدم جديد
- `POST /api/users/login` - تسجيل الدخول
- `GET /api/users` - الحصول على جميع المستخدمين

### المهام

- `GET /api/taskitems` - الحصول على جميع المهام
- `POST /api/taskitems` - إنشاء مهمة جديدة
- `PUT /api/taskitems/{id}` - تحديث مهمة
- `DELETE /api/taskitems/{id}` - حذف مهمة

---

##  المصادقة والأمان

- يتم تشفير كلمات المرور باستخدام hashing آمن
- المصادقة تعتمد على JWT (JSON Web Tokens)
- جميع الطلبات المحمية تتطلب token صحيح

---

##  المتطلبات

### Client

- Node.js 16+ و npm

### Server

- .NET 10.0 SDK
- SQL Server (أو LocalDB)


---

##  الترخيص

هذا المشروع مفتوح المصدر ومتاح للاستخدام الحر.

---


---

**آخر تحديث**: ديسمبر 2025
