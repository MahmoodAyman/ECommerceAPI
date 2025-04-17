# ECommerce API

## Overview

ECommerce API is a robust backend solution for managing an online store. It provides features for product management, user authentication, shopping cart operations, and error handling. The API is designed with scalability and maintainability in mind, leveraging modern technologies like ASP.NET Core, Entity Framework Core, and Redis.

## Features

- **Product Management**: Add, update, delete, and retrieve products with filtering, sorting, and pagination.
- **User Authentication**: Secure user registration, login, and role-based authorization using ASP.NET Identity.
- **Shopping Cart**: Manage shopping carts with Redis for fast and scalable storage.
- **Error Handling**: Centralized exception handling with detailed error responses.
- **CORS Support**: Configured to allow requests from trusted front-end origins.
- **Upcoming Features**:
  - **Order Management**: Seamless order placement and tracking.
  - **Checkout & Payments**: Integration with Stripe for secure payment processing.

## Technologies Used

- **ASP.NET Core 9.0**: For building a high-performance RESTful API.
- **Entity Framework Core**: For database interactions with SQL Server.
- **Redis**: For caching and shopping cart management.
- **Docker**: For containerized deployment of SQL Server and Redis.
- **Stripe**: (Planned) For handling payments.

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Docker
- SQL Server and Redis (via Docker Compose)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/ecommerce-api.git
   cd ecommerce-api
   ```
2. Start dependencies using Docker Compose
   ```bash
   docker-compose up -d
   ```
3. Update the database:
   ```bash
   dotnet ef database update -p Infrastructure
   ```
4. Run the application
   ```bash
   dotnet run -p API
   ```

### API endpoints

    - Products: /api/products
    - Cart: /api/cart
    - Account: /auth/account
    - Buggy (Testing): /api/buggy

### Configuration

- Update connection strings in appsettings.Development.json for SQL Server and Redis.

### Testing

    - Use Postman or any HTTP client to test the API.
    - Example Postman script for CORS testing:

```JavaScript
    pm.test("CORS header is present", function () {
    pm.response.to.have.header("Access-Control-Allow-Origin");
    pm.expect(pm.response.headers.get("Access-Control-Allow-Origin")).to.eql("https://localhost:4200");

});
```
