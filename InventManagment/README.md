# Inventory Management System

## Overview
This is an ASP.NET Core MVC web application for managing inventory items. The system includes user authentication, CRUD operations for inventory items, and API endpoints for external integration.

## Features
- **User Authentication**: Register and login functionality with secure password hashing
- **Inventory Management**: Create, read, update, and delete inventory items
- **Image Upload**: Support for uploading images for inventory items
- **API Endpoints**: RESTful API for external applications
- **Session Management**: Secure user sessions with automatic timeout

## Database Schema
The application uses Entity Framework Core with SQL Server and includes two main tables:

### Items Table
- `Id` (Primary Key): Unique identifier for each item
- `Name`: Name of the inventory item
- `Quantity`: Current stock quantity
- `ImagePath`: Optional path to item image

### Users Table
- `Id` (Primary Key): Unique identifier for each user
- `Username`: Unique username for login
- `Email`: User's email address
- `PasswordHash`: Securely hashed password
- `CreatedAt`: Account creation timestamp
- `IsActive`: Account status flag

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation
1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run database migrations:
   ```
   dotnet ef database update
   ```
4. Build and run the application:
   ```
   dotnet build
   dotnet run
   ```

### Usage
1. Navigate to the application URL (typically `https://localhost:5001`)
2. Register a new user account
3. Login with your credentials
4. Start managing your inventory items

## API Endpoints
The application provides RESTful API endpoints for external integration:

- `GET /api/inventoryapi` - Get all inventory items
- `GET /api/inventoryapi/{id}` - Get specific item by ID
- `POST /api/inventoryapi` - Create new inventory item
- `PUT /api/inventoryapi/{id}` - Update existing item
- `DELETE /api/inventoryapi/{id}` - Delete item

## Security Features
- Password hashing using SHA256
- Session-based authentication
- CSRF protection with anti-forgery tokens
- Input validation and sanitization

## File Structure
```
InventManagment/
├── Controllers/          # MVC and API controllers
├── Models/              # Data models
├── Views/               # Razor views
├── Data/                # Database context and migrations
├── wwwroot/             # Static files (CSS, JS, images)
└── Program.cs           # Application startup configuration
```

## Technologies Used
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Font Awesome Icons 