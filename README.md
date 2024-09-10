# ASP.NET Core MVC Product Management Application

## 1. Understanding the Requirements

The goal of this project is to build an ASP.NET Core MVC web application to manage a list of products and perform the following tasks:

- **Product List**: Display all products.
- **CRUD Operations**: Support for creating, reading, updating, and deleting products with attributes such as `ProductID`, `Name`, `Category`, `Price`, and `Stock`.
- **Average Price Calculation**: Calculate and display the average product price by category.
- **Highest Stock Category**: Determine and display the category with the highest stock.
- **Caching**: Implement caching to optimize product listing and calculations.
- **Stored Procedures**: Utilize stored procedures for calculations (e.g., average price per category, highest stock category).
- **Post-CRUD Operation Delegates**: Use delegates for actions like logging or notifications after CRUD operations.
- **Unit Testing**: Write unit tests to validate the CRUD logic in the service layer.

## 2. Plan to Solve the Problem Using Clean Architecture

### Clean Architecture Breakdown

- **Presentation Layer (Web)**: MVC Controllers and Views to handle user interface interactions.
- **Application Layer**: Service logic, DTOs (Data Transfer Objects), and business rules.
- **Domain Layer**: Core business entities like `Product` along with repository interfaces.
- **Infrastructure Layer**: Database interactions and repository implementations.

### Step-by-Step Breakdown

#### Create ASP.NET Core MVC Project

- Use Clean Architecture to organize the project into layers (Presentation (Web), Application, Domain, and Infrastructure).

#### Define Domain Layer

- **Entities**: Create `Product` with attributes like `ProductID`, `Name`, `Category`, `Price`, and `Stock`.
- **Interfaces**: Define `IProductRepository` to abstract product CRUD operations and calculation methods.

#### Define Application Layer

- **DTOs**: Create data transfer objects for moving data between layers.
- **Service Layer**: Implement `ProductService` to handle:
  - CRUD operations.
  - Calculate average price by category.
  - Determine category with the highest stock.
  - Use ASP.NET Core’s caching mechanisms for product listings and calculations.
  - Implement delegates for post-CRUD actions such as logging.

#### Define Infrastructure Layer

- **EF Core & Repository Pattern**: Implement EF Core with code-first migrations for database management.
- **Stored Procedures**: Use MSSQL stored procedures for product-related calculations.
- **Caching**: Implement caching in repository methods using ASP.NET Core’s `IMemoryCache`.

#### Define Presentation Layer

- **Controller**: `ProductController` to handle HTTP requests and interact with the Service Layer.
- **Views**: Use Razor Pages to display product lists and reports.
- **Styling**: Apply Bootstrap for layout and styling, and jQuery for interactivity.

## 3. Pseudo Code (Clean Architecture Approach)

### Domain Layer

- **Entity**: `Product` (ProductID, Name, Category, Price, Stock).
- **Repository Interface**: `IProductRepository` defines abstract methods for CRUD operations and calculations.

### Application Layer

- **DTO**: `ProductDTO`.
- **Service Logic**:
  - CRUD operations.
  - Average price calculation by category.
  - Determine the category with the highest stock.
  - Caching for product listings and calculations.
- **Delegates**: Implement delegates for post-CRUD operations such as logging.

### Infrastructure Layer

- **Repository Implementation**: Use EF Core for database CRUD operations and call stored procedures for calculations.
- **Caching**: Implement caching using `IMemoryCache`.

### Presentation Layer (MVC)

- **Controller**: `ProductController` for handling requests and interacting with the service layer.
- **Views**: Use Razor views for product display and reports.

## 4. Project Development

### Database Setup

- Use EF Core for database schema generation and management through code-first migrations.

### Repository Setup

- Implement repository logic for CRUD operations and stored procedure calls.

### Service Layer Setup

- Implement business logic for CRUD and product calculations.
- Integrate caching using `IMemoryCache`.

### Controller and Views

- Build controllers to handle HTTP requests and interact with the service layer.
- Create views to display product lists and reports, with enhanced UI using Bootstrap and jQuery.

### Unit Testing

- Write unit tests using MSTest to validate service logic.
- Use mocking to isolate service layer tests from repository and database logic.

---
