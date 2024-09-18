# Customer Support System

This project is developed for the **Systems Analysis and Design** course as part of the Computer Science degree at **UNIFRAN**. It provides a comprehensive customer support platform where users can manage support tickets, submit feedback, and access knowledge base articles.

## Features
- **Ticket Management**: Create, update, delete, and view tickets with categories and priorities.
- **Feedback System**: Submit feedback for closed tickets.
- **Knowledge Base**: Access categorized support articles.
- **Comment System**: Add comments to tickets.
- **User Roles**: Role-based access control (Admin, User).
  
## Technologies
- **C#** with **ASP.NET Core MVC**
- **Entity Framework Core** for database handling
- **SQL Server** for the relational database
- **Session Management** using `ISessionService`

## Project Structure

### Models
- `TicketModel`: Manages ticket data (`Id`, `Title`, `Description`, `StatusEnum`, `CategoryEnum`, `PriorityEnum`, `UserId`).
- `FeedbackModel`: Handles feedback for closed tickets (linked to `TicketModel`).
- `KnowledgeBaseModel`: Stores articles with `Title`, `Content`, and `CategoryEnum`.
- **Enums**: Define `StatusEnum`, `CategoryEnum`, and `PriorityEnum`.

### Repositories
- **GenericRepository**: Common CRUD operations for entities.
- **TicketRepository**, **FeedbackRepository**, and **KnowledgeBaseRepository**: Specific business logic.

### Controllers
- `TicketController`: Manages tickets (CRUD).
- `UserController`: Manage users (CRUD).
- `FeedbackController`: Handles feedback submission.
- `KnowledgeBaseController`: Manages knowledge base articles (CRUD).

## Getting Started

1. **Clone the repository**:
   ```bash
   git clone https://github.com/kaiquefreire05/CustomerSupportSystem.git
   ```
2. **Navigate to the project**:
   ```bash
   cd CustomerSupportSystem
   ```
3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```
4. **Set up the database**:
   Update the connection string in `appsettings.json` and run migrations:
   ```bash
   dotnet ef database update
   ```
5. **Run the project**:
   ```bash
   dotnet run
   ```

## Endpoints

### Tickets
- `GET /tickets`: List all tickets.
- `POST /tickets`: Create a new ticket.
- `PUT /tickets/{id}`: Update a ticket.
- `DELETE /tickets/{id}`: Delete a ticket.

### Feedback
- `POST /feedback`: Submit feedback for a closed ticket.

### Knowledge Base
- `GET /knowledgebase`: Retrieve articles.
- `POST /knowledgebase`: Create an article.

## Future Improvements
- **Search Functionality**: Advanced search for knowledge base articles.
- **Notifications**: Email alerts for ticket updates.
- **Reporting**: Admin dashboards with detailed ticket reports.

## License
Licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
