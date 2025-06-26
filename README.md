# Unknown Camp Server

Server-side backend for the multiplayer game **Unknown Camp**.

Developed for a bachelor degree project by [your name here].

Game repository: https://github.com/AlexAT-dev/Unknown-Camp

## Overview

This server implements a REST API for account management, treasure chests, email verification, email delivery, and MongoDB database interaction.  
Built with .NET 8, using ASP.NET Core Web API, MongoDB, and JWT authentication.

## Requirements

- .NET 8 SDK
- MongoDB (cloud or local)
- SMTP server for email delivery

## Features

- User registration, login, and email verification
- JWT authentication
- Opening treasure chests with random rewards
- Purchasing chests with in-game currency
- Mass and individual email delivery
- Swagger UI for API testing

## Project Structure

- `UnknownCampServer.API/` — ASP.NET Core Web API (controllers, DI, startup)
- `UnknownCampServer.Core/` — core entities, DTOs, interfaces
- `UnknownCampServer.Infrastructure/` — repository implementations, services, MongoDB integration

## Configuration

Settings are stored in [`appsettings.json`](UnknownCampServer.API/appsettings.json):

- `ConnectionStrings:MongoDb` — MongoDB connection string
- `SmtpSettings` — SMTP parameters for email delivery
- `Jwt` — JWT token settings
- `AppConfig` — server, game, and resource versioning

## Getting Started

1. Install .NET 8 SDK.
2. Set your configuration in [`appsettings.json`](UnknownCampServer.API/appsettings.json).
3. Run the server:

   ```sh
   dotnet run --project UnknownCampServer.API
   ```

   or with Docker:

   ```sh
   docker build -f UnknownCampServer.API/Dockerfile -t unknowncampserver .
   docker run -d -p 5000:5000 --name unknowncampserver unknowncampserver
   ```

4. The API will be available at http://localhost:5000  
   Swagger UI: http://localhost:5000/swagger

## Main Endpoints

- `POST /api/Account/Create` — user registration
- `POST /api/Account/Login` — user login
- `GET /Verification/verify-email?token=...` — email verification
- `POST /api/Account/OpenTreasure/{treasureId}` — open a chest (JWT required)
- `POST /api/Account/BuyMatchBox` — purchase a chest (JWT required)
- `POST /api/Account/AddMatches/{matches}` — add matches (JWT required)
- `POST /api/Email/SendTo` — send email to user
- `POST /api/Email/SendToAll` — mass email delivery

## Security

- JWT (Bearer Token) authentication
- Passwords stored as PBKDF2 hashes with salt

## Additional

- Swagger UI for API testing
- Docker support for deployment

## License

MIT or specify your license

---

**See also:**  
- [`UnknownCampServer.API/Program.cs`](UnknownCampServer.API/Program.cs) — entry point and DI  
- [`UnknownCampServer.Core/Entities/Account.cs`](UnknownCampServer.Core/Entities/Account.cs) — account structure  
- [`UnknownCampServer.Infrastructure/Services/AccountService.cs`](UnknownCampServer.Infrastructure/Services/AccountService.cs) — account logic  
- [`UnknownCampServer.API/Controllers/AccountController.cs`](UnknownCampServer.API/Controllers/AccountController.cs) — account controller