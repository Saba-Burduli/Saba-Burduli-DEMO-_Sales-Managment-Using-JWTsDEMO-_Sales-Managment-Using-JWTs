
<h1>🚀 JWTsDEMO - Sales Management Using JWTs</h1>

 ## ✅ Overview
The **JWTsDEMO - Sales Management Using JWTs** project is a **Sales Management System** built with **ASP.NET Core** that demonstrates **JWT-based authentication and role-based access control**. The system allows admins, sales managers, and employees to manage sales records, generate reports, and perform CRUD operations securely.

## ⁉️ How I Built This Project
I developed this project using **ASP.NET Core Web API** with **Entity Framework Core** for data management and **MSSQL Server** as the database. The authentication mechanism relies on **JWT (JSON Web Token)** to ensure secure access control. The project follows **RESTful API** principles and implements **role-based access control (RBAC)** for different user roles.

### 📚 Websites & Tools Used:
- **Microsoft Docs** – For learning best practices in ASP.NET Core.
- **Entity Framework Documentation** – For database migrations and setup.
- **Stack Overflow & GitHub Discussions** – For troubleshooting and optimizing the code.
- **Postman** – For API testing and debugging.
- **Swagger (NSwag)** – To generate API documentation and make API testing easier.
- **Visual Studio 2022** – For coding and debugging.
- **MSSQL Server Management Studio (SSMS)** – To manage and query the database.
- **JWT.io** – To decode and verify JWT tokens.

 ## 🔜 Features
- **JWT Authentication** for secure API access.
- **Role-based access control (RBAC)** with Admin, Sales Manager, and Employee roles.
- **CRUD operations** for sales records and product management.
- **MSSQL Database** integration using **Entity Framework Core**.
- **Swagger API Documentation** for easy testing.

## 💻 Technologies Used
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Microsoft SQL Server (MSSQL)**
- **JWT Authentication**
- **Swagger (for API documentation)**

## ↙️ Installation & Setup
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

## ✅ JWT Authentication & Role-Based Access Control
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

## ✅ API Endpoints
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


# ✅ ASP.NET Web API - Main Fields

ASP.NET Web API is a framework for building **RESTful services** that allow applications to communicate over HTTP. It is widely used in **modern web, mobile, and cloud-based applications**.

## **1. Routing & Controllers**
- Uses **attribute-based routing** (`[Route]`) to define API endpoints.
- Controllers handle HTTP requests (`GET, POST, PUT, DELETE`).
- Example:
  ```csharp
  [Route("api/products")]
  public class ProductsController : ControllerBase
  {
      [HttpGet("{id}")]
      public IActionResult GetProduct(int id) { ... }
  }
  ```

## 💻 **2. HTTP Methods & Status Codes**
- Supports standard HTTP methods:  
  - `GET` → Retrieve data  
  - `POST` → Create data  
  - `PUT` → Update data  
  - `DELETE` → Remove data  
- Returns proper **HTTP status codes** like `200 OK`, `201 Created`, `400 Bad Request`, etc.

## 💻 **3. Model Binding & Validation**
- Automatically maps HTTP request data (JSON, query parameters, etc.) to C# objects.
- Supports **validation attributes** (`[Required]`, `[MaxLength]`, `[Range]`, etc.).
- Example:
  ```csharp
  public class ProductDto
  {
      [Required] public string Name { get; set; }
      [Range(1, 10000)] public decimal Price { get; set; }
  }
  ```

## **💻4. Dependency Injection (DI)**
- Supports **built-in dependency injection** for service and repository patterns.
- Example:
  ```csharp
  public class ProductsController : ControllerBase
  {
      private readonly IProductService _productService;

      public ProductsController(IProductService productService)
      {
          _productService = productService;
      }
  }
  ```

## 💻 **5. Authentication & Authorization**
- Supports **JWT (JSON Web Token)**, OAuth, and API Key authentication.
- Uses `[Authorize]` attribute to protect API endpoints.
- Example:
  ```csharp
  [Authorize]
  [HttpGet("secure-data")]
  public IActionResult GetSecureData() { ... }
  ```

## 💻 **6. Entity Framework & Database Operations**
- Uses **Entity Framework Core (EF Core)** as an ORM for database interactions.
- Supports **LINQ queries**, migrations, and database seeding.
- Example:
  ```csharp
  public class ProductService : IProductService
  {
      private readonly AppDbContext _context;
      
      public ProductService(AppDbContext context) { _context = context; }

      public async Task<List<Product>> GetAllProductsAsync()
      {
          return await _context.Products.ToListAsync();
      }
  }
  ```

## 💻 **7. Middleware & Filters**
- Uses middleware for **error handling, logging, and request processing**.
- Filters like `[ExceptionFilter]`, `[ActionFilter]` allow request customization.
- Example:
  ```csharp
  public class CustomExceptionFilter : ExceptionFilterAttribute
  {
      public override void OnException(ExceptionContext context)
      {
          context.Result = new ObjectResult("An error occurred") { StatusCode = 500 };
      }
  }
  ```

## 💻 **8. API Documentation (Swagger / OpenAPI)**
- Uses **Swashbuckle** to generate API documentation automatically.
- Enables **interactive testing** of endpoints.
- Example:
  ```csharp
  services.AddSwaggerGen();
  app.UseSwagger();
  app.UseSwaggerUI();
  ```

## 💻 **9. CORS (Cross-Origin Resource Sharing)**
- Enables API access from different origins (frontend apps, mobile clients).
- Example:
  ```csharp
  services.AddCors(options =>
  {
      options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
  });
  ```

---


<h1>🌀Here is Operations :</h1>


<h3>1. Person Operations:</h3>

<ul>
 <li>Add a new customer/ deliever.</li>
  <li>Update customer/deliever details (e.g., name, address, contact info).</li>
  <li>Delete a customer/deliever.</li>
  <li>View all customers/delievers.</li>
</ul>

<h3>2. Product Operations:</h3>
<ul>
 <li>Add a new product.</li>
  <li>Update product details (e.g., name, price, stock quantity).</li>
  <li>Delete a product.</li>
  <li>View all products or filter by category.</li>
</ul>


<h3>3. Order Operations:</h3>
<ul>
 <li>Create a new order (add order and related order items).</li>
  <li>Update Order status (e.g., mark as completed).</li>
  <li>Delete an Order (remove all associated order items).</li>
  <li>View all Orders by customer, deliever or statuses.</li>
</ul>


<h3>4. OrderItem Operations:</h3>
<ul>
  <li>Add products to an order.</li>
  <li>Update order item quantities or prices.</li>
  <li>Remove items from an order.</li>
</ul>


<h3>5. Payment Operations</h3>
<ul>
  <li>Add a payment to an order.</li>
  <li>Update payment details (e.g., amount, payment method).</li>
  <li>View payments by order.</li>
</ul>


<h3>6. Category Operations (optional):</h3>
<ul>
 <li>Add, update, or delete categories.</li>
 <li>Assign products to categories.</li>
</ul>

<br>

### 💻 **Conclusion**
**ASP.NET Web API** provides a robust framework for **building, securing, and deploying** RESTful services. It integrates well with **Entity Framework, authentication mechanisms, and modern web technologies**, making it a key tool in backend development.

 📥  If you want to learn more about This Project you can actually contact me on Mail : **sabagg790@gmail.com**


