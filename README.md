# Reactivities: Learning .NET and TypeScript with React

This project, **Reactivities**, is a personal learning initiative to improve my skills in building modern web applications using **.NET** for the backend and **TypeScript with React** for the frontend. The goal is to create a full-stack application while exploring best practices, modern tools, and frameworks.

---

## Project Overview

Reactivities is a simple activity management application where users can create, edit, view, and delete activities. It serves as a playground to experiment with various technologies and concepts, including:

- **Backend**: .NET 9 with Entity Framework Core for database management and MediatR for CQRS pattern implementation.
- **Frontend**: React with TypeScript, Material-UI for styling, and React Hook Form for form handling.
- **Validation**: FluentValidation on the backend and Zod on the frontend for schema validation.
- **State Management**: React Query for data fetching and caching.
- **Routing**: React Router for navigation.

---

## Learning Objectives

### Backend (.NET)
1. **Entity Framework Core**: Learn how to manage database migrations, seed data, and query data efficiently.
2. **CQRS with MediatR**: Implement the Command-Query Responsibility Segregation pattern to separate read and write operations.
3. **Validation**: Use FluentValidation to enforce business rules and ensure data integrity.
4. **Middleware**: Understand how to handle errors and implement custom middleware for cross-cutting concerns.
5. **Dependency Injection**: Leverage .NET's built-in DI container to manage services and dependencies.

### Frontend (React + TypeScript)
1. **TypeScript**: Write strongly-typed React components and manage complex types effectively.
2. **React Hook Form**: Simplify form handling and integrate with Zod for validation.
3. **Material-UI**: Build a modern, responsive UI using Material-UI components.
4. **React Query**: Manage server state and caching for efficient data fetching.
5. **Routing**: Use React Router to implement dynamic routing and navigation.
6. **Error Handling**: Display user-friendly error messages and handle edge cases gracefully.

---

## Technologies Used

### Backend
- **.NET 9**: The core framework for building the API.
- **Entity Framework Core**: ORM for database interactions.
- **SQLite**: Lightweight database for development.
- **MediatR**: For implementing CQRS.
- **FluentValidation**: For validating incoming requests.

### Frontend
- **React 19**: The core library for building the UI.
- **TypeScript**: For type safety and better developer experience.
- **Material-UI**: For pre-styled components and responsive design.
- **React Hook Form**: For form state management.
- **Zod**: For schema validation.
- **React Query**: For managing server state and caching.
- **React Router**: For navigation and routing.

---

## Features

1. **Activity Management**:
   - Create, edit, view, and delete activities.
   - Activities include fields like title, description, date, category, city, and venue.

2. **Validation**:
   - Backend: FluentValidation ensures data integrity.
   - Frontend: Zod validates user input before submission.

3. **Error Handling**:
   - Backend: Custom middleware for handling exceptions.
   - Frontend: User-friendly error messages with `react-toastify`.

4. **Responsive UI**:
   - Material-UI ensures the application looks great on all devices.

---

## Folder Structure

### Backend
- **API**: Contains controllers and middleware.
- **Application**: Business logic, commands, queries, and validators.
- **Domain**: Core domain models.
- **Persistence**: Database context and migrations.

### Frontend
- **src/app**: Core application setup, including routing and shared components.
- **src/features**: Feature-specific components (e.g., activities, errors).
- **src/lib**: Shared utilities like hooks, schemas, and API agents.

---

## How to Run the Project

### Prerequisites
- **.NET SDK**: Version 8 or higher.
- **Node.js**: Version 18 or higher.
- **SQLite**: For database management.

### Backend
1. Navigate to the `API` folder.
2. Run `dotnet restore` to install dependencies.
3. Apply migrations and seed the database:
   ```bash
   dotnet ef database update
   ```
4. Start the API
    ```bash
    dotnet run
    ```

### Frontend
1. Navigate to the client folder
2. Install dependencies:
    ```bash
    pnpm install
    ```
4. Start the development server:
    ```bash
    pnpm dev
    ```

