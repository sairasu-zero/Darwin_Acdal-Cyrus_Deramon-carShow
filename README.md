# Car Show Registration System

A registration management system for **AutoVista Events’ Car Show 2027**, held at the Sorsogon City Coliseum over three days. It manages participant and vehicle registrations, registration types and statuses, registration fees, and payment status.

## Team

| Team member | Role | Responsibilities |
|---|---|---|
| `[Backend Developer name]` | Backend Developer | ASP.NET Core API, application features, validation, and SQLite persistence |
| `[Frontend Developer name]` | Frontend Developer | React application, user interface, and API integration |

Replace the bracketed names with the project team members’ names.

## Technology stack

- C# and .NET 10
- ASP.NET Core Minimal API
- Vertical Slice Architecture
- Entity Framework Core and SQLite
- FluentValidation
- Scalar API documentation
- React (frontend)
- Git and GitHub

## Project structure

```text
car-show-registration/
├── Backend/
│   ├── Verticalslice/
│   │   ├── Api/             # Minimal API and HTTP endpoints
│   │   ├── Application/     # Registration features and validation
│   │   ├── Domain/          # Registration entity and enums
│   │   └── Infrastructure/  # EF Core, SQLite, migrations, and seed data
│   ├── Verticalslice.sln
│   ├── Api.http
│   └── carshow.db           # Persistent SQLite database (when present)
├── Frontend/                # React application
├── docs/
│   └── screenshots/
├── .gitignore
└── README.md
```

## Prerequisites

Install the following before starting:

1. [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
2. [Node.js LTS and npm](https://nodejs.org/) (for the frontend)
3. [Git](https://git-scm.com/downloads)
4. A code editor such as Visual Studio 2026 or Visual Studio Code

Check your installations in a terminal:

```powershell
dotnet --version
node --version
npm --version
git --version
```

## Get the project

Clone the repository, then enter its root folder:

```powershell
git clone https://github.com/<owner>/<repository>.git
cd <repository>
```

Replace the repository URL placeholder with the project’s GitHub URL. The current project repository is `https://github.com/sairasu-zero/Darwin_Acdal-Cyrus_Deramon-carShow`.

## Set up the database

The API uses Entity Framework Core with SQLite. The persistent database file is `Backend/carshow.db`; it is resolved relative to the backend solution, not the terminal’s current directory. The API applies pending migrations at startup and does not recreate the database on each run.

From the `Backend` directory, install the EF Core command-line tool if needed and apply the existing migration:

```powershell
cd Backend
dotnet tool install --global dotnet-ef --version 10.0.15
dotnet ef database update --project Verticalslice\Infrastructure\Infrastructure.csproj --startup-project Verticalslice\Api\Api.csproj
```

If `dotnet-ef` is already installed, skip the install command. To create a migration after making a model change, use a new migration name (do not add `InitialCreate` again):

```powershell
dotnet ef migrations add <MigrationName> --project Verticalslice\Infrastructure\Infrastructure.csproj --startup-project Verticalslice\Api\Api.csproj --output-dir Persistence\Migrations
dotnet ef database update --project Verticalslice\Infrastructure\Infrastructure.csproj --startup-project Verticalslice\Api\Api.csproj
```

## Run the backend

From the repository root:

```powershell
cd Backend
dotnet restore Verticalslice.sln
dotnet run --project Verticalslice\Api\Api.csproj --launch-profile http
```

The API is available at `http://localhost:5231`. CORS permits the React development origin `http://localhost:5173`.

## Scalar API documentation

With the backend running, open:

- Scalar: <http://localhost:5231/scalar/v1>
- OpenAPI document: <http://localhost:5231/openapi/v1.json>

## API endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| `POST` | `/api/registrations` | Create a registration |
| `GET` | `/api/registrations` | List registrations; supports `participantName`, `type`, `status`, `page`, and `pageSize` query parameters |
| `GET` | `/api/registrations/{id:guid}` | Get a registration by GUID |
| `GET` | `/api/registrations/summary` | Get registration and payment summary KPIs |

Example create request (the server generates the registration number, date, fee, initial status, and payment status):

```json
{
  "participantName": "Juan Dela Cruz",
  "contactNumber": "09171234567",
  "email": "juan@example.com",
  "address": "123 Rizal St., Naga City",
  "carMakeModel": "Toyota Supra MK4",
  "plateNumber": "ABC 1234",
  "registrationType": "Competitor",
  "amountPaid": 1000.00
}
```

## Run the frontend

The `Frontend` folder is reserved for the React application. Once its developer has added the React project and `package.json`, open a second terminal and run:

```powershell
cd Frontend
npm install
npm run dev
```

The current API CORS policy allows `http://localhost:5173`; configure the frontend development server to use that address. If the React project has not yet been added, these npm commands will not work until its `package.json` is available.

## GitHub repository

Repository URL: `https://github.com/<owner>/<repository>.git`

Current project repository: <https://github.com/sairasu-zero/Darwin_Acdal-Cyrus_Deramon-carShow>
