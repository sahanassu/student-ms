# 🎓 Student Management System

A full-stack **Student Management System** developed using **ASP.NET Core Web API** and **React.js**, designed to efficiently manage student records with secure authentication and a clean user interface.

---

## 📌 Project Overview

This application allows users to perform complete **CRUD operations** (Create, Read, Update, Delete) on student data. It follows a **layered architecture** to ensure scalability, maintainability, and clean code practices.

---

## 🛠️ Tech Stack

### 🔹 Backend

* ASP.NET Core Web API (.NET 8)
* Entity Framework Core
* SQLite Database
* JWT Authentication

### 🔹 Frontend

* React.js (JavaScript)
* Tailwind CSS
* Axios
* React Router

### 🔹 Tools & Others

* Git & GitHub
* Swagger (API Testing)
* Docker 

---

## ✨ Key Features

* 🔐 Secure **JWT-based Authentication**
* 📋 Manage student records (Add, Edit, Delete, View)
* 🔄 RESTful API integration
* 📊 Clean and responsive UI
* ⚙️ Layered Architecture (Controller → Service → Repository)
* 📄 API documentation using Swagger

---

## 📂 Project Structure

```
Student-MS/
├── src/
│   ├── StudentManagement.API/
│   ├── StudentManagement.Core/
│   └── StudentManagement.Infrastructure/
├── frontend/
├── tests/
└── docker-compose.yml
```

---

## ⚡ Getting Started

### 🔹 Prerequisites

* Node.js (v18+)
* .NET 8 SDK
* Git

---

### 🔹 Installation & Setup

#### 1. Clone Repository

```bash
git clone https://github.com/YOUR-USERNAME/student-management-system.git
cd student-management-system
```

---

#### 2. Run Backend

```bash
cd src/StudentManagement.API
dotnet restore
dotnet run
```

Backend runs on:
👉 http://localhost:5000

---

#### 3. Run Frontend

```bash
cd frontend
npm install
npm start
```

Frontend runs on:
👉 http://localhost:3001

---

## 🔐 Login Credentials (Demo)

```
Username: admin
Password: password123
```

---

## 📡 API Endpoints (Sample)

* `POST /api/auth/login` → User login
* `GET /api/students` → Fetch all students
* `POST /api/students` → Add student
* `PUT /api/students/{id}` → Update student
* `DELETE /api/students/{id}` → Delete student

---

## 🗄️ Database

* Uses **SQLite** (lightweight and file-based)
* Automatically created on first run
* No manual configuration required

---

## 🧪 Testing

```bash
dotnet test
```

---

## 🚀 Deployment

### Backend

```bash
dotnet publish -c Release
```

### Frontend

```bash
npm run build
```

---

## 💡 Highlights

* Clean architecture with separation of concerns
* Industry-level authentication using JWT
* Scalable and maintainable code structure
* Suitable for learning full-stack development

---

## 👩‍💻 Author

**Sahana S Udikeri**
MCA Student | Aspiring Software Developer

---

## 📬 Note

This project was developed as part of a technical assignment and demonstrates full-stack development skills, API design, and authentication mechanisms.

---

