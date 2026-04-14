# 🕌 Salati — Prayer Times & Adhan Alerts

> Desktop application for Muslim prayer times, Adhan alerts, and daily Islamic reminders.

---

## 📋 About

**Salati** is a Windows desktop application built with **C# / .NET 10 / WinForms** that provides:
- 🕐 Daily prayer times (via [Aladhan API](https://aladhan.com/prayer-times-api) or manual input)
- 🔔 Customizable Adhan alerts before each prayer
- 🌙 Dark & Light themes (Midnight Serenity + Golden Dawn)
- 🌐 Arabic & English language support
- 📌 Widget mode (compact always-on-top)
- 🔕 System tray integration

---

## 🏗️ Architecture

```
3-Tier Architecture (Same as DVLD pattern)
┌──────────┐    ┌──────────┐    ┌──────────┐
│    UI    │ →  │   BLL    │ →  │   DAL    │
│ WinForms │    │  Logic   │    │ ADO.NET  │
│  Guna2   │    │  NAudio  │    │ SQL Srv  │
└──────────┘    └──────────┘    └──────────┘
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| UI | WinForms + Guna2 UI Framework |
| BLL | C# + NAudio (sound playback) |
| DAL | ADO.NET + Stored Procedures |
| DB | SQL Server (SalatiDB) |
| API | Aladhan Prayer Times API |

---

## 📁 Project Structure

```
Salati/
├── DAL/          ← Data Access Layer
├── BLL/          ← Business Logic Layer
├── UI/           ← Presentation Layer (WinForms)
│   ├── Forms/    ← frmSplash, frmMain, frmAlert
│   ├── Controls/ ← 14 custom UserControls
│   ├── Core/     ← Theme, Language, Engine
│   └── Assets/   ← Sounds, Icons
└── doc/          ← Documentation
```

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022+ (or Rider)
- .NET 10 SDK
- SQL Server (LocalDB or Express)

### Setup
1. Clone this repo
2. Run the SQL scripts from `doc/DB/DATABASE.md`
3. Open `Salati.slnx` in Visual Studio
4. Build & Run

---

## 📊 Current Status

| Layer | Status |
|-------|--------|
| ✅ UI | Complete (3 forms + 14 controls) |
| ✅ Theme System | Dark + Light themes |
| ✅ Language System | Arabic + English |
| 🔲 Database | Ready (scripts in docs) |
| 🔲 DAL | TODO |
| 🔲 BLL | TODO |
| 🔲 Integration | TODO |

---

## 👥 Team

| Role | Member |
|------|--------|
| UI / Architecture | Mahmoud |
| Database | TBD |
| DAL | TBD |
| BLL | TBD |
| Integration | TBD |

---

## 📝 License

This project is for educational purposes.
