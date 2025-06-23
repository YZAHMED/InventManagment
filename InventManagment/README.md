# Inventory Management System

## Project Overview
This is my passion project for the Content Management Systems course. I chose to build an inventory management system because I'm passionate about organization and helping businesses keep track of their stuff. This project shows how to use ASP.NET Core, Entity Framework, and LINQ to build a real web application.

## What This Project Does
This is a web application that lets users:
- Register and login to their own account
- Add inventory items with names, quantities, and pictures
- Organize items into categories (like "Electronics", "Office Supplies", etc.)
- Edit and delete items when needed
- Search through their inventory
- See all their items in a nice table format

## Technologies I Used
- **ASP.NET Core 8.0** - The main framework for building the web app
- **Entity Framework Core** - For talking to the database
- **SQL Server** - To store all the data
- **LINQ** - For querying the database in a nice way
- **Bootstrap** - To make the website look good
- **Razor Views** - For the web pages

## Database Design
I designed the database with 3 main tables:

### Users Table
- Stores user account information
- Has username, email, and hashed password
- Tracks when accounts were created

### Items Table  
- Stores all the inventory items
- Has name, quantity, and optional image path
- Can belong to multiple categories

### Categories Table
- Helps organize items into groups
- Has name and description
- Can contain many items

### Relationships
- **One-to-Many**: Users can have many items, Categories can have many items
- **Many-to-Many**: Items can belong to multiple categories (using a junction table)

## How I Built It

### 1. Database Setup
I used Entity Framework's "Code First" approach, which means I wrote the C# classes first and then let Entity Framework create the database tables. I created 3 migrations:
- Initial migration for the Items table
- Added Users table for authentication  
- Added Categories table with the many-to-many relationship

### 2. Models and DTOs
I created C# classes (models) for each database table:
- `User.cs` - for user accounts
- `Item.cs` - for inventory items  
- `Category.cs` - for organizing items

I also made DTOs (Data Transfer Objects) to safely pass data between the web pages and the database without exposing sensitive information.

### 3. Services Layer
I created a service layer to handle all the business logic:
- `IItemService` - interface defining what operations are available
- `ItemService` - actual implementation using LINQ queries

This makes the code more organized and easier to test.

### 4. Controllers
I built both MVC controllers (for web pages) and API controllers (for data):
- `HomeController` - handles the main pages
- `InventoryController` - manages inventory operations
- `AuthController` - handles user login/registration
- `InventoryApiController` - provides API endpoints

### 5. Views
I created web pages using Razor syntax:
- Login and registration forms
- Inventory list with table view
- Add/edit forms for items
- Confirmation pages for deletions

## Key Features I Implemented

### User Authentication
- Users can register with username, email, and password
- Passwords are hashed using SHA256 for security
- Session-based authentication keeps users logged in
- Protected routes that require login

### Inventory Management
- Full CRUD operations (Create, Read, Update, Delete)
- Image upload for items
- Quantity tracking with color-coded badges
- Search functionality

### Category System
- Items can belong to multiple categories
- Categories help organize inventory
- Many-to-many relationship implemented

### API Endpoints
- RESTful API for external applications
- JSON responses for all operations
- Swagger documentation for testing

## How to Run This Project

### Prerequisites
- Visual Studio 2022 or VS Code
- .NET 8.0 SDK
- SQL Server (LocalDB works fine)

### Setup Steps
1. Clone or download this project
2. Open the project in Visual Studio
3. Update the connection string in `appsettings.json` if needed
4. Open Package Manager Console and run:
   ```
   Update-Database
   ```
5. Press F5 to run the application

### First Time Setup
1. Go to the website (usually https://localhost:5001)
2. Click "Register" to create an account
3. Login with your credentials
4. Start adding inventory items!

## Code Examples

### LINQ Queries I Used
```csharp
// Get all items with their categories
var items = await _context.Items
    .Include(i => i.Categories)
    .ToListAsync();

// Find items in a specific category
var itemsInCategory = await _context.Items
    .Where(i => i.Categories.Any(c => c.Id == categoryId))
    .ToListAsync();

// Search items by name
var searchResults = await _context.Items
    .Where(i => i.Name.Contains(searchTerm))
    .ToListAsync();
```

### Service Layer Example
```csharp
public class ItemService : IItemService
{
    private readonly InventoryContext _context;
    
    public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
    {
        var items = await _context.Items
            .Include(i => i.Categories)
            .ToListAsync();
            
        return items.Select(item => new ItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            CategoryIds = item.Categories.Select(c => c.Id).ToList()
        });
    }
}
```

## What I Learned
This project taught me a lot about:
- Building real web applications with ASP.NET Core
- Using Entity Framework for database operations
- Writing LINQ queries for complex data retrieval
- Implementing authentication and security
- Creating a service layer for better code organization
- Using DTOs to protect data
- Building both MVC and API controllers
- Managing many-to-many relationships in databases

## Challenges I Faced
- **Many-to-Many Relationships**: Figuring out how to properly set up the relationship between Items and Categories was tricky at first
- **File Uploads**: Learning how to handle image uploads and store them securely
- **Authentication**: Implementing proper user authentication with session management
- **Service Layer**: Understanding when and how to use services vs controllers

## Future Improvements
If I had more time, I would add:
- User roles and permissions
- Inventory history tracking
- Barcode scanning
- Email notifications for low stock
- Export to Excel functionality
- Mobile app version

## Project Structure
```
InventManagment/
├── Controllers/          # Web page and API controllers
├── Models/              # Database entity classes
├── DTOs/                # Data transfer objects
├── Services/            # Business logic services
├── Data/                # Database context and migrations
├── Views/               # Web page templates
├── wwwroot/             # CSS, JavaScript, and images
└── README.md           # This file
```

## Screenshots
The application includes:
- Clean, responsive web interface
- User authentication pages
- Inventory management dashboard
- Add/edit forms with image upload
- Category management system

## Conclusion
This project demonstrates my understanding of ASP.NET Core, Entity Framework, LINQ, and web development best practices. I'm proud of how it turned out and learned a lot about building real-world applications.

---

**Student Name**: [Your Name]  
**Course**: Content Management Systems  
**Date**: [Current Date] 