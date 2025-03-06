# Saba-Burduli-DEMO-_Sales-Managment-Using-JWTsDEMO-_Sales-Managment-Using-JWTs
Online Exam API

Overview

The Online Exam API is a RESTful web service built using ASP.NET Core and Entity Framework. It allows users to take exams online, manage questions, evaluate results, and handle authentication/authorization through JWT tokens with role-based access control.

Features

User authentication and authorization using JWT Tokens.

Role-based access control (Admin, Instructor, Student).

CRUD operations for exams, questions, and users.

Secure API endpoints.

Database integration using Entity Framework Core and MSSQL.

Technologies Used

ASP.NET Core Web API

Entity Framework Core

Microsoft SQL Server (MSSQL)

JWT Authentication

Swagger (for API documentation)

Installation & Setup

Clone the repository:

git clone https://github.com/your-repo/OnlineExamAPI.git
cd OnlineExamAPI

Install dependencies:

dotnet restore

Configure appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=OnlineExamDB;User Id=your-user;Password=your-password;"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  }
}

Apply migrations and update the database:

dotnet ef database update

Run the project:

dotnet run

JWT Authentication & Role-Based Access Control

How JWT Works in ASP.NET Core

User Logs In: A user submits credentials (email & password) to the API.

Token Generation: If valid, the API generates a JWT token with user claims (including roles).

Token Usage: The token is included in the Authorization header of subsequent API requests.

Role Validation: The API checks user roles before allowing access to protected endpoints.

Setting Up JWT Authentication in Program.cs

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

Defining Roles in UserRoles.cs

public static class UserRoles
{
    public const string Admin = "Admin";
    public const string Instructor = "Instructor";
    public const string Student = "Student";
}

Protecting Endpoints Based on Roles

[Authorize(Roles = UserRoles.Admin)]
[HttpPost("/create-exam")]
public IActionResult CreateExam(ExamDto examDto)
{
    // Only Admins can create exams
    return Ok("Exam created successfully");
}

[Authorize(Roles = UserRoles.Instructor)]
[HttpPost("/add-question")]
public IActionResult AddQuestion(QuestionDto questionDto)
{
    // Only Instructors can add questions
    return Ok("Question added successfully");
}

[Authorize(Roles = UserRoles.Student)]
[HttpGet("/take-exam/{examId}")]
public IActionResult TakeExam(int examId)
{
    // Only Students can take exams
    return Ok("Exam started");
}

API Endpoints

Authentication

Method

Endpoint

Description

POST

/api/auth/login

User login & JWT generation

POST

/api/auth/register

Register new users

Exams

Method

Endpoint

Role

Description

POST

/api/exams

Admin

Create a new exam

GET

/api/exams

Any

Get all exams

GET

/api/exams/{id}

Any

Get exam details

Questions

Method

Endpoint

Role

Description

POST

/api/questions

Instructor

Add a new question

GET

/api/questions/{examId}

Any

Get questions for an exam

Conclusion

This Online Exam API provides a secure and scalable solution for conducting online exams with role-based access control using JWT authentication in ASP.NET Core. 🚀

