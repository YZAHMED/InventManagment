# Inventory Management System - User Flow Diagram

## Application Navigation Flow
This diagram shows how users move through the different pages of the application.

## ASCII User Flow Diagram
```
┌─────────────────────────────────────────────────────────────────┐
│                        USER FLOW DIAGRAM                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─────────────┐                                               │
│  │   START     │                                               │
│  │  (Browser)  │                                               │
│  └─────┬───────┘                                               │
│        │                                                       │
│        ▼                                                       │
│  ┌─────────────┐                                               │
│  │    HOME     │                                               │
│  │   PAGE      │                                               │
│  └─────┬───────┘                                               │
│        │                                                       │
│        ▼                                                       │
│  ┌─────────────┐         ┌─────────────┐                      │
│  │   LOGIN     │◄────────┤  REGISTER   │                      │
│  │   PAGE      │         │   PAGE      │                      │
│  └─────┬───────┘         └─────┬───────┘                      │
│        │                       │                               │
│        │                       │                               │
│        ▼                       ▼                               │
│  ┌─────────────┐         ┌─────────────┐                      │
│  │  VALIDATE   │         │  VALIDATE   │                      │
│  │  LOGIN      │         │  REGISTER   │                      │
│  └─────┬───────┘         └─────┬───────┘                      │
│        │                       │                               │
│        │                       │                               │
│        ▼                       ▼                               │
│  ┌─────────────┐         ┌─────────────┐                      │
│  │   SUCCESS   │         │   SUCCESS   │                      │
│  │   LOGIN     │         │  REGISTER   │                      │
│  └─────┬───────┘         └─────┬───────┘                      │
│        │                       │                               │
│        └───────┬───────────────┘                               │
│                │                                               │
│                ▼                                               │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │              INVENTORY LIST PAGE                        │   │
│  │  (Main Dashboard - Shows all items)                     │   │
│  └─────┬───────┬───────┬───────┬───────┬───────┬───────────┘   │
│        │       │       │       │       │       │               │
│        ▼       ▼       ▼       ▼       ▼       ▼               │
│  ┌─────────┐ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────────────┐   │
│  │   ADD   │ │VIEW │ │EDIT │ │EDIT │ │DEL  │ │   LOGOUT    │   │
│  │   NEW   │ │ITEM │ │ITEM │ │ITEM │ │ITEM │ │             │   │
│  │   ITEM  │ │DETAIL│ │ 1   │ │ 2   │ │ 1   │ │             │   │
│  └─────┬───┘ └──┬──┘ └──┬──┘ └──┬──┘ └──┬──┘ └─────┬───────┘   │
│        │        │       │       │       │           │           │
│        ▼        ▼       ▼       ▼       ▼           ▼           │
│  ┌─────────┐ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────────┐ ┌─────────┐   │
│  │   ADD   │ │SHOW │ │EDIT │ │EDIT │ │ DELETE  │ │  CLEAR  │   │
│  │   FORM  │ │ITEM │ │FORM │ │FORM │ │CONFIRM  │ │ SESSION │   │
│  │   PAGE  │ │INFO │ │ 1   │ │ 2   │ │  PAGE   │ │         │   │
│  └─────┬───┘ └──┬──┘ └──┬──┘ └──┬──┘ └─────┬──┘ └─────┬───┘   │
│        │        │       │       │           │           │       │
│        ▼        │       ▼       ▼           ▼           ▼       │
│  ┌─────────┐    │  ┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────┐ │
│  │  SAVE   │    │  │  SAVE   │ │  SAVE   │ │ CONFIRM │ │GO TO│ │
│  │   NEW   │    │  │  CHANGES│ │  CHANGES│ │  DELETE │ │LOGIN│ │
│  │   ITEM  │    │  │    TO   │ │    TO   │ │   ITEM  │ │PAGE │ │
│  └─────┬───┘    │  │  DATABASE│ │  DATABASE│ │         │ └─────┘ │
│        │        │  └─────┬───┘ └─────┬───┘ └─────┬───┘         │
│        │        │        │           │           │               │
│        └────────┴────────┴───────────┴───────────┘               │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Detailed User Journey

### 1. New User Journey
```
┌─────────────────────────────────────────────────────────────────┐
│                        NEW USER JOURNEY                         │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  1. User opens browser and goes to application URL             │
│     └─► Home page displays welcome message                      │
│                                                                 │
│  2. User clicks "Register" button                               │
│     └─► Registration form appears                               │
│                                                                 │
│  3. User fills out registration form:                          │
│     • Username: "john_doe"                                      │
│     • Email: "john@email.com"                                   │
│     • Password: "password123"                                   │
│     └─► System validates input and creates account             │
│                                                                 │
│  4. Registration successful                                     │
│     └─► User is redirected to Login page                       │
│                                                                 │
│  5. User enters login credentials                               │
│     └─► System authenticates user                              │
│                                                                 │
│  6. Login successful                                            │
│     └─► User is redirected to Inventory List page              │
│                                                                 │
│  7. User can now manage inventory items                        │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

### 2. Returning User Journey
```
┌─────────────────────────────────────────────────────────────────┐
│                      RETURNING USER JOURNEY                     │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  1. User opens browser and goes to application URL             │
│     └─► Home page displays welcome message                      │
│                                                                 │
│  2. User clicks "Login" button                                  │
│     └─► Login form appears                                      │
│                                                                 │
│  3. User enters existing credentials:                          │
│     • Username: "john_doe"                                      │
│     • Password: "password123"                                   │
│     └─► System validates credentials                           │
│                                                                 │
│  4. Login successful                                            │
│     └─► User is redirected to Inventory List page              │
│                                                                 │
│  5. User can manage their inventory items                      │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

### 3. Inventory Management Journey
```
┌─────────────────────────────────────────────────────────────────┐
│                    INVENTORY MANAGEMENT JOURNEY                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │              INVENTORY LIST PAGE                        │    │
│  │  (User sees all their items in a table)                 │    │
│  └─────────────────────────────────────────────────────────┘    │
│                              │                                 │
│                              ▼                                 │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │  User can perform these actions:                        │    │
│  │                                                         │    │
│  │  1. ADD NEW ITEM                                        │    │
│  │     └─► Click "Add New Item" button                     │    │
│  │     └─► Fill out item form (name, quantity, image)      │    │
│  │     └─► Click "Create Item"                             │    │
│  │     └─► Return to inventory list                        │    │
│  │                                                         │    │
│  │  2. EDIT EXISTING ITEM                                  │    │
│  │     └─► Click "Edit" button next to item                │    │
│  │     └─► Modify item details in form                     │    │
│  │     └─► Click "Update Item"                             │    │
│  │     └─► Return to inventory list                        │    │
│  │                                                         │    │
│  │  3. DELETE ITEM                                         │    │
│  │     └─► Click "Delete" button next to item              │    │
│  │     └─► Confirm deletion on confirmation page           │    │
│  │     └─► Click "Delete Item"                             │    │
│  │     └─► Return to inventory list                        │    │
│  │                                                         │    │
│  │  4. LOGOUT                                              │    │
│  │     └─► Click "Logout" button in header                 │    │
│  │     └─► Session is cleared                              │    │
│  │     └─► Return to login page                            │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Page Transitions

### Navigation Rules
1. **Unauthenticated Users**:
   - Can only access: Home, Login, Register pages
   - Any attempt to access inventory pages → Redirect to Login

2. **Authenticated Users**:
   - Can access all pages except Login/Register
   - Accessing Home page → Redirect to Inventory List
   - Session timeout → Redirect to Login

3. **Form Submissions**:
   - Successful operations → Redirect to Inventory List
   - Validation errors → Stay on current page with error messages

### Error Handling Flow
```
┌─────────────────────────────────────────────────────────────────┐
│                        ERROR HANDLING FLOW                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─────────────┐                                               │
│  │    ERROR    │                                               │
│  │   OCCURS    │                                               │
│  └─────┬───────┘                                               │
│        │                                                       │
│        ▼                                                       │
│  ┌─────────────┐                                               │
│  │  ERROR TYPE │                                               │
│  └─────┬───────┘                                               │
│        │                                                       │
│        ▼                                                       │
│  ┌─────────────┐         ┌─────────────┐                      │
│  │ VALIDATION  │         │   SYSTEM    │                      │
│  │   ERROR     │         │   ERROR     │                      │
│  └─────┬───────┘         └─────┬───────┘                      │
│        │                       │                               │
│        ▼                       ▼                               │
│  ┌─────────────┐         ┌─────────────┐                      │
│  │  SHOW ERROR │         │  SHOW ERROR │                      │
│  │  MESSAGE ON │         │   PAGE      │                      │
│  │  SAME PAGE  │         │             │                      │
│  └─────────────┘         └─────────────┘                      │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## User Experience Notes

### Design Principles
- **Simple Navigation**: Clear paths between pages
- **Consistent Layout**: Same header and navigation across all pages
- **Clear Feedback**: Success/error messages for all actions
- **Confirmation Dialogs**: For destructive actions like delete
- **Responsive Design**: Works on desktop and mobile devices

### Accessibility Features
- Clear button labels and form fields
- Logical tab order for keyboard navigation
- Color-coded quantity indicators
- Descriptive error messages

### Performance Considerations
- Fast page loads with minimal server requests
- Efficient database queries
- Optimized image handling
- Session management for user state

This user flow ensures a smooth and intuitive experience for managing inventory items. 