
# Online Exam API With JWT's

# JWTsDEMO - Sales Management Using JWTs

## Overview
The **JWTsDEMO - Sales Management Using JWTs** project is a **Sales Management System** built with **ASP.NET Core** that demonstrates **JWT-based authentication and role-based access control**. The system allows admins, sales managers, and employees to manage sales records, generate reports, and perform CRUD operations securely.

## Features
- **JWT Authentication** for secure API access.
- **Role-based access control (RBAC)** with Admin, Sales Manager, and Employee roles.
- **CRUD operations** for sales records and product management.
- **MSSQL Database** integration using **Entity Framework Core**.
- **Swagger API Documentation** for easy testing.

## Technologies Used
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Microsoft SQL Server (MSSQL)**
- **JWT Authentication**
- **Swagger (for API documentation)**

## Installation & Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/JWTsDEMO-Sales-Management.git
   cd JWTsDEMO-Sales-Management
   ```
2. Install dependencies:
   ```bash
   dotnet restore
   ```
3. Configure **appsettings.json**:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=SalesManagementDB;User Id=your-user;Password=your-password;"
     },
     "Jwt": {
       "Key": "your-secret-key",
       "Issuer": "your-issuer",
       "Audience": "your-audience"
     }
   }
   ```
4. Apply migrations and update the database:
   ```bash
   dotnet ef database update
   ```
5. Run the project:
   ```bash
   dotnet run
   ```

## JWT Authentication & Role-Based Access Control
### How JWT Works in ASP.NET Core
1. **User Logs In:** A user submits credentials (email & password) to the API.
2. **Token Generation:** If valid, the API generates a JWT token with user claims (including roles).
3. **Token Usage:** The token is included in the **Authorization** header of subsequent API requests.
4. **Role Validation:** The API checks user roles before allowing access to protected endpoints.

### Setting Up JWT Authentication in `Program.cs`
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
```

### Defining Roles in `UserRoles.cs`
```csharp
public static class UserRoles
{
    public const string Admin = "Admin";
    public const string SalesManager = "SalesManager";
    public const string Employee = "Employee";
}
```

### Protecting Endpoints Based on Roles
```csharp
[Authorize(Roles = UserRoles.Admin)]
[HttpPost("/create-product")]
public IActionResult CreateProduct(ProductDto productDto)
{
    // Only Admins can create products
    return Ok("Product created successfully");
}

[Authorize(Roles = UserRoles.SalesManager)]
[HttpPost("/create-sale")]
public IActionResult CreateSale(SaleDto saleDto)
{
    // Only Sales Managers can create sales records
    return Ok("Sale recorded successfully");
}

[Authorize(Roles = UserRoles.Employee)]
[HttpGet("/sales-report")]
public IActionResult GetSalesReport()
{
    // Employees can view sales reports
    return Ok("Sales report retrieved");
}
```

## API Endpoints
### Authentication
| Method | Endpoint        | Description |
|--------|----------------|-------------|
| POST   | `/api/auth/login` | User login & JWT generation |
| POST   | `/api/auth/register` | Register new users |

### Products
| Method | Endpoint        | Role  | Description |
|--------|----------------|------|-------------|
| POST   | `/api/products` | Admin | Create a new product |
| GET    | `/api/products` | Any  | Get all products |

### Sales Management
| Method | Endpoint        | Role  | Description |
|--------|----------------|------|-------------|
| POST   | `/api/sales` | SalesManager | Create a new sale |
| GET    | `/api/sales` | Employee | View sales records |
| GET    | `/api/sales-report` | Employee | Get sales reports |

## Conclusion
The **JWTsDEMO - Sales Management Using JWTs** project provides a secure and scalable solution for handling sales transactions with **JWT authentication** in **ASP.NET Core**. 🚀



