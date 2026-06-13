# Build a simple Project & Task Management API

# Project & Task Management API

## 📋 Project Overview
A lightweight, secure ASP.NET Core Web API designed for managing projects and tasks.  
This system follows **Clean Architecture principles** and leverages **Entity Framework Core** with a **Code-First approach**.  
It provides JWT-based authentication, validation, and exception handling to ensure reliability and scalability.

---

## 🎯 Key Features
- **Authentication**: Register & Login with JWT tokens.
- **Project Management**: Full CRUD operations for projects.
- **Task Management**: Create tasks, update status, delete tasks, and list tasks by project.
- **Validation**: Input validation for all endpoints.
- **Exception Handling**: Centralized error handling.
- **Database Migrations**: EF Core migrations included in `DAL/Migrations`.
- **Swagger Documentation**: Auto-generated API documentation and testing UI.

---

## 🏗️ Architecture

### Code-First Approach (EF Core)
The application uses Entity Framework Core with the **Code-First approach**, generating migrations and database schema directly from entity models.

### Clean Architecture Layers (3-Tier Architecture)

```
┌────────────────────────────────────────────────────────┐
│          Presentation Layer (API)                      │
│  - Controllers (handle routing & HTTP requests)        │
│  - Swagger UI (API documentation & testing)            │
└────────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────────┐
│          Application Layer                              │
│  - Services (Business logic implementation)             │
│  - DTOs & ViewModels                                    │
│  - Validation                                           │
└─────────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────────┐
│          Data Access Layer (DAL)                        │
│  - Repositories (Data access)                           │
│  - Entity Models (Database entities)                    │
│  - DbContext                                            │
│  - EF Core Migrations                                   │
└─────────────────────────────────────────────────────────┘
```

### Dependency Injection
- All services and repositories are accessed via Interfaces.  
- Constructor injection is used throughout the application.  
- Follows Inversion of Control (IoC) principles.  

---

## 🔐 Security & Authentication
### User Roles & Access Control
####🔵 Employee Role (Standard Access)
- ✅ Read Access: View all projects and tasks assigned
- ✅Get Project By Id: Vieby idw project
- ❌ Create: Add new tasks under existing projects
- ❌ Update: Edit task details and update task status
- ❌ Delete Projects: Cannot delete projects (restricted to Manager/Both only)
- ❌ Delete Tasks: Allowed (can remove tasks they created)
- ✅ Authentication: Register/Login using JWT token

####🟠 Manager Role (Extended Access)  
- ❌ Read Access: View all tasks
- ✅ Create: Add new projects and tasks
- ✅ Update: Edit existing projects
- ✅ Delete Projects: Allowed with full control
- ✅ Delete Tasks: Allowed with full control
- ✅ System Management: Manage project assignments and team tasks
- ✅ Authentication: Register/Login using JWT token

####🟣 Both Role (Combined Access)  
- ✅ Get Task By Project: View all tasks by Project id
- ❌ Create: Add new tasks under existing projects
- ✅ Update: Edit task details and update task status
- ❌ Delete Projects: Cannot delete projects (restricted to Manager/Both only)
- ❌ Delete Tasks: Allowed (can remove tasks they created)
- ✅ Authentication: Register/Login using JWT token

---

## 📊 Database Schema
### Core Tables
1. **Users Table**
   - UserId (Primary Key)  
   - Username (Unique)  
   - PasswordHash   
   - Department  

2. **Projects Table**
   - ProjectId (Primary Key)  
   - Name  
   - Description  
   - CreatedDate  
   - UserId (Foreign Key to Users)
   - Tasks  (Foreign Key to User)

3. **Tasks Table**
   - TaskId (Primary Key)  
   - Title  
   - Description  
   - Status ("Pending", "InProgress", "Done")  
   - DueDate  
   - Priority  ("Low", "Medium", "High") 
   - ProjectId (Foreign Key to Projects)  

---

## 🚀 Core Functionality

### 1. Project Management
- Create new projects.  
- View all projects.  
- Get project by ID.  
- Update project details.  
- Delete project.  

### 2. Task Management
- Create new tasks under a project.  
- Update task status ("Pending", "InProgress", "Done").  
- Delete tasks.  
- View tasks by project.  

### 3. Authentication
- Register new users.  
- Login with JWT token.  
- Token required for all secured endpoints.  

---

## 📖 API Documentation (Swagger)
Swagger UI is enabled for this project.

- Run the project:
  ```bash
  dotnet run
  
Swagger UI provides:
- A full list of available endpoints (Authentication, Projects, Tasks).
- Request and response models.
- The ability to test endpoints directly from the browser.
