# Customer Management System

## Project Overview
This is a **Customer Management System** built using **C# (Windows Forms) and MS SQL Server**. The system allows users to:
- **Add, Update, Delete Customers**
- **Assign Customers to Categories**
- **Search Customers**
- **Manage Categories** (Add new categories dynamically)
- **Store customer details in an SQL database**

## Technologies Used
- **C# (.NET Framework, Windows Forms)**
- **Microsoft SQL Server**
- **ADO.NET for database interaction**
- **Git for version control**

## 📂 Project Structure
```
CustomerManagementSystem/
│── Models/         # Contains Customer & Category classes
│── DAL/            # Data Access Layer (Handles SQL operations)
│── UI/             # Windows Forms UI components
│── App.config      # Database connection settings
│── Program.cs      # Application entry point
```

## Setup 
### Database Setup
1. Open **SQL Server Management Studio (SSMS)**.
2. Run the following script to **create the database**:

```sql
CREATE DATABASE CustomerManagement;
USE CustomerManagement;

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerCode VARCHAR(50) NOT NULL,
    CustomerName NVARCHAR(50) NOT NULL,
    CustomerCategory INT NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdateDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL,
    CategoryDescription NVARCHAR(255) NOT NULL
);

INSERT INTO Categories (CategoryName, CategoryDescription) VALUES
('Customer', 'Regular Customers'),
('Supplier', 'Suppliers of Products'),
('Distributor', 'Distributors or Agents'),
('VIP', 'High-value Customers');
```

### Application
1. **Clone the Repository**
   ```bash
   git clone https://github.com/Zamir-00/Customer-Management-System.git
   ```
2. **Open the Project in Visual Studio**
3. **Update Database Connection** in `App.config`:
   ```xml
   <connectionStrings>
       <add name="CustomerDB"
            connectionString="Server=YOUR_SERVER;Database=CustomerManagement;Trusted_Connection=True;"
            providerName="System.Data.SqlClient"/>
   </connectionStrings>
   ```
4. **Build & Run the Application**
   - Press `Ctrl + F5` to run the project.
   - Ensure the database is set up before testing.

## Features & How to Use
### **Managing Customers**
- Customers can be **added, updated, deleted**.
- Assign each customer to a **category**.
- Use the **search bar** to filter customers.

### **Managing Categories**
- Click **"Manage Categories"** to add new categories.
- Categories are **stored in the database** dynamically.

### **Searching Customers**
- Type in the **search bar** to filter by **name or email**.
- Results update automatically.