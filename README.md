# library-management-app

## Overview

This README provides an overview of the ASP.NET MVC application, highlighting its structure, features, and implementation details.

### Application Structure

The application follows a standard ASP.NET MVC architecture, consisting of the following components:

- **Controllers:** Responsible for handling user requests and returning appropriate responses.
- **Models:** Includes Entity Framework classes representing application entities.
- **Views:** Rendered to the user and structured using Razor syntax for dynamic content generation.

### Key Features

The application incorporates the following features to ensure functionality and usability:

1. **Entity Framework Classes**: Includes a minimum of four classes (excluding User) to model application entities.

2. **Data Types**: Ensures appropriate use of data types (e.g., dates, nullable types, integers vs strings) in entity properties.

3. **Object Relationships**: Defines correct relationships among objects (e.g., 1-N, N-N, inheritance) based on business logic.

4. **Menu Navigation**: Implements a navigational menu excluding standard pages like About and Contact.

5. **Custom Routing**: Defines custom routes in [Program.cs](LibraryManagementApp.Web/Program.cs) for specific functionalities (e.g., `/Place/Add`).

6. **Attribute Routing**: Uses attribute-based routing for defining routes within controllers and actions.

7. **Data Modification**: Allows data modification for at least two entities, respecting business rules and constraints.

8. **Shared Partial View**: Utilizes a shared partial view for edit and create forms to enhance code reusability and maintainability.

9. **AJAX Search Form**: Implements a search form that utilizes AJAX for fetching results dynamically.

10. **Server-Side Validation**: Implements server-side validation to ensure data integrity and adherence to business rules.

11. **Client-Side Validation**: Includes client-side validation to enhance user experience and reduce server requests.

12. **Dropdown Lists**: Uses dropdown lists for selecting related values to enforce data integrity.

13. **Database Seeding**: Implements seeding for initial data (e.g., cities) to populate the database.

14. **Migration Scripts**: Includes initial and additional migration scripts to manage database schema changes effectively.

15. **Tag Helpers**: Utilizes Tag Helpers for at least three interface elements to simplify view creation and management.

16. **Date Control**: Implements date controls that function in at least two languages with different date formats.

17. **Bootstrap Principles**: Designs the user interface following basic Bootstrap principles for responsiveness and consistency.

18. **AJAX Delete**: Implements deletion functionality using AJAX calls for enhanced user interaction and performance.

19. **DAL and Model Layer**: Separates Data Access Layer (DAL) and Model layers for clean and organized code architecture.

20. **API Integration**: Provides API endpoints for fetching, adding, modifying, and deleting entities.

### Cypress Tests

The application includes automated tests using Cypress to ensure functionality and reliability:

- **Cypress Tests**: Includes comprehensive end-to-end tests written in Cypress to validate user workflows, data validation, and interactions within the application.

### Getting Started

To run the application locally or deploy it to a server, follow these steps:

1. **Clone Repository**: Clone the repository from Git using `git clone https://github.com/bornasinovcic/library-management-app.git`.

2. **Setup Database**: Configure the database connection string in [appsettings.json](LibraryManagementApp.Web/appsettings.json) and ensure Entity Framework migrations are applied.

3. **Build and Run**: Build the solution in Visual Studio and run the application to ensure all dependencies are resolved.

4. **Explore Functionality**: Navigate through the application to explore different features, forms, and validations.

### Dependencies

Ensure the following dependencies are installed and configured:

- **Visual Studio**: For development and debugging.
- **Entity Framework**: ORM framework for data access and management.
- **Bootstrap**: Front-end framework for responsive design and UI components.

### Contributing

Contributions to the project are welcome. Please follow standard Git flow practices:

- Fork the repository.
- Create a new branch (`git checkout -b feature/your-feature-name`).
- Commit your changes (`git commit -am 'Add new feature'`).
- Push to the branch (`git push origin feature/your-feature-name`).
- Create a new Pull Request.
