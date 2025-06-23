# Inventory Management System - Entity Relationship Diagram (ERD)

## Database Schema Overview
This ERD shows how the database tables are connected and what data they store.

## ASCII ERD Diagram
```
┌─────────────────────────────────────────────────────────────────┐
│                        DATABASE SCHEMA                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │                        USERS                            │    │
│  ├─────────────────────────────────────────────────────────┤    │
│  │                                                         │    │
│  │  +----------------+-----------------------------------+  │    │
│  │  │ Field Name     │ Data Type        │ Description   │  │    │
│  │  +----------------+-----------------------------------+  │    │
│  │  │ Id             │ INT (PK)         │ Unique ID     │  │    │
│  │  │ Username       │ NVARCHAR(MAX)    │ Login name    │  │    │
│  │  │ Email          │ NVARCHAR(MAX)    │ Email address │  │    │
│  │  │ PasswordHash   │ NVARCHAR(MAX)    │ Hashed pwd    │  │    │
│  │  │ CreatedAt      │ DATETIME2        │ Signup date   │  │    │
│  │  │ IsActive       │ BIT              │ Account status│  │    │
│  │  +----------------+-----------------------------------+  │    │
│  │                                                         │    │
│  └─────────────────────────────────────────────────────────┘    │
│                              │                                 │
│                              │                                 │
│                              │                                 │
│                              ▼                                 │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │                        ITEMS                            │    │
│  ├─────────────────────────────────────────────────────────┤    │
│  │                                                         │    │
│  │  +----------------+-----------------------------------+  │    │
│  │  │ Field Name     │ Data Type        │ Description   │  │    │
│  │  +----------------+-----------------------------------+  │    │
│  │  │ Id             │ INT (PK)         │ Unique ID     │  │    │
│  │  │ Name           │ NVARCHAR(MAX)    │ Item name     │  │    │
│  │  │ Quantity       │ INT              │ Stock count   │  │    │
│  │  │ ImagePath      │ NVARCHAR(MAX)    │ Image file    │  │    │
│  │  +----------------+-----------------------------------+  │    │
│  │                                                         │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Detailed Table Descriptions

### 1. USERS Table
```
┌─────────────────────────────────────────────────────────────────┐
│                           USERS TABLE                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Purpose: Stores user account information for authentication   │
│                                                                 │
│  ┌─────────────┬──────────────┬─────────────┬─────────────────┐ │
│  │ Column Name │ Data Type    │ Constraints │ Description     │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ Id          │ INT          │ PK, IDENTITY│ Auto-generated  │ │
│  │             │              │             │ unique ID       │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ Username    │ NVARCHAR(MAX)│ NOT NULL    │ Login username  │ │
│  │             │              │ UNIQUE      │ (must be unique)│ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ Email       │ NVARCHAR(MAX)│ NOT NULL    │ User's email    │ │
│  │             │              │ UNIQUE      │ (must be unique)│ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ PasswordHash│ NVARCHAR(MAX)│ NOT NULL    │ SHA256 hashed   │ │
│  │             │              │             │ password        │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ CreatedAt   │ DATETIME2    │ NOT NULL    │ Account creation│ │
│  │             │              │             │ timestamp       │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ IsActive    │ BIT          │ NOT NULL    │ Account status  │ │
│  │             │              │ DEFAULT(1)  │ (1=active, 0=inactive)│ │
│  └─────────────┴──────────────┴─────────────┴─────────────────┘ │
│                                                                 │
│  Sample Data:                                                   │
│  ┌─────┬──────────┬─────────────────┬──────────┬─────────────┐ │
│  │ Id  │ Username │ Email           │ CreatedAt│ IsActive    │ │
│  ├─────┼──────────┼─────────────────┼──────────┼─────────────┤ │
│  │ 1   │ john_doe │ john@email.com  │ 2024-01-15│ 1          │ │
│  │ 2   │ jane_smith│ jane@email.com │ 2024-01-16│ 1          │ │
│  └─────┴──────────┴─────────────────┴──────────┴─────────────┘ │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

### 2. ITEMS Table
```
┌─────────────────────────────────────────────────────────────────┐
│                           ITEMS TABLE                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Purpose: Stores inventory item information                    │
│                                                                 │
│  ┌─────────────┬──────────────┬─────────────┬─────────────────┐ │
│  │ Column Name │ Data Type    │ Constraints │ Description     │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ Id          │ INT          │ PK, IDENTITY│ Auto-generated  │ │
│  │             │              │             │ unique ID       │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ Name        │ NVARCHAR(MAX)│ NOT NULL    │ Item name       │ │
│  │             │              │             │ (e.g., "Laptop")│ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ Quantity    │ INT          │ NOT NULL    │ Current stock   │ │
│  │             │              │ DEFAULT(0)  │ quantity        │ │
│  ├─────────────┼──────────────┼─────────────┼─────────────────┤ │
│  │ ImagePath   │ NVARCHAR(MAX)│ NULL        │ Path to image   │ │
│  │             │              │             │ file (optional) │ │
│  └─────────────┴──────────────┴─────────────┴─────────────────┘ │
│                                                                 │
│  Sample Data:                                                   │
│  ┌─────┬──────────┬──────────┬─────────────────────────────┐   │
│  │ Id  │ Name     │ Quantity │ ImagePath                   │   │
│  ├─────┼──────────┼──────────┼─────────────────────────────┤   │
│  │ 1   │ Laptop   │ 5        │ /uploads/laptop.jpg         │   │
│  │ 2   │ Mouse    │ 12       │ /uploads/mouse.jpg          │   │
│  │ 3   │ Keyboard │ 8        │ NULL                        │   │
│  │ 4   │ Monitor  │ 3        │ /uploads/monitor.jpg        │   │
│  └─────┴──────────┴──────────┴─────────────────────────────┘   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Relationships and Constraints

### Primary Keys
- **USERS.Id**: Auto-incrementing integer, unique identifier for each user
- **ITEMS.Id**: Auto-incrementing integer, unique identifier for each item

### Unique Constraints
- **USERS.Username**: Each username must be unique across all users
- **USERS.Email**: Each email address must be unique across all users

### Data Types Explained

#### INT (Integer)
- Used for: IDs, quantities, counts
- Range: -2,147,483,648 to 2,147,483,647
- Examples: User ID = 1, Item Quantity = 5

#### NVARCHAR(MAX)
- Used for: Text fields with variable length
- Can store up to 2GB of Unicode text
- Examples: Username = "john_doe", Item Name = "Laptop"

#### DATETIME2
- Used for: Date and time values
- Precision: 100 nanoseconds
- Examples: CreatedAt = "2024-01-15 10:30:45.1234567"

#### BIT
- Used for: Boolean values (true/false)
- Storage: 1 bit (0 or 1)
- Examples: IsActive = 1 (true), IsActive = 0 (false)

## Database Operations

### Common Queries

#### 1. User Authentication
```sql
SELECT * FROM Users 
WHERE Username = 'john_doe' AND IsActive = 1
```

#### 2. Get All Inventory Items
```sql
SELECT Id, Name, Quantity, ImagePath 
FROM Items 
ORDER BY Name
```

#### 3. Add New User
```sql
INSERT INTO Users (Username, Email, PasswordHash, CreatedAt, IsActive)
VALUES ('newuser', 'user@email.com', 'hashedpassword', GETDATE(), 1)
```

#### 4. Update Item Quantity
```sql
UPDATE Items 
SET Quantity = 10 
WHERE Id = 1
```

#### 5. Delete Item
```sql
DELETE FROM Items 
WHERE Id = 1
```

## Security Considerations

### Password Security
- Passwords are never stored in plain text
- SHA256 hashing algorithm is used
- Example: "password123" → "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f"

### Data Validation
- Username and email must be unique
- Quantity cannot be negative
- ImagePath is optional (NULL allowed)

### Session Management
- User sessions are managed in application memory
- Sessions expire after 30 minutes of inactivity
- No sensitive data stored in database sessions

## Future Enhancements

### Potential Additional Tables
1. **Categories**: To organize items by type
2. **Suppliers**: To track item suppliers
3. **Transactions**: To log inventory changes
4. **UserRoles**: To implement different access levels

### Example Enhanced Schema
```
USERS (1) ──── (Many) ITEMS
   │
   └── (Many) USER_ROLES
              │
              └── (Many) ROLES

ITEMS (Many) ──── (1) CATEGORIES
   │
   └── (Many) TRANSACTIONS
              │
              └── (1) SUPPLIERS
```

This ERD provides a solid foundation for the inventory management system with room for future expansion. 