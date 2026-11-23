
# Car Show Registration System

A registration management system for AutoVista Events' Car Show 2027 at Sorsogon City Coliseum. It manages participant and vehicle registrations, registration types and statuses, registration fees, and payment status.

## Team

| Member | Role |
|---|---|
| Cyrus Deramon | Backend Developer |
| Darwin Acdal | Frontend Developer |

## Technology Stack

### Backend
- C# and .NET 10
- ASP.NET Core Minimal API
- Vertical Slice Architecture
- Entity Framework Core
- SQLite
- FluentValidation
- Scalar API Documentation

### Frontend
- React
- TypeScript
- Vite
- Tailwind CSS
- ESLint

## Project Structure

```text
Darwin_Acdal-Cyrus_Deramon-carShow/
├── Backend/
│   ├── Verticalslice/
│   │   ├── Api/
│   │   ├── Application/
│   │   ├── Domain/
│   │   └── Infrastructure/
│   ├── Verticalslice.sln
│   ├── Api.http
│   └── carshow.db
├── frontend/
│   ├── src/
│   │   ├── api/
│   │   ├── components/
│   │   ├── hooks/
│   │   ├── pages/
│   │   ├── types.ts
│   │   └── utils/
│   └── README.md
├── docs/
├── .gitignore
└── README.md
```

## Prerequisites

Install the following:

1. [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
2. [Node.js LTS and npm](https://nodejs.org/)
3. [Git](https://git-scm.com/downloads)
4. Visual Studio 2026 or Visual Studio Code

Verify your installations:

```powershell
dotnet --version
node --version
npm --version
git --version
```

## Clone the Repository

```powershell
git clone https://github.com/sairasu-zero/Darwin_Acdal-Cyrus_Deramon-carShow.git
cd Darwin_Acdal-Cyrus_Deramon-carShow
```

## Backend Setup

Restore the backend dependencies:

```powershell
cd Backend
dotnet restore Verticalslice.sln
```

### Database

The backend uses Entity Framework Core and SQLite.

Database file:

```text
Backend/carshow.db
```

To install the EF Core command-line tool if needed:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.15
```

Apply migrations:

```powershell
dotnet ef database update --project Verticalslice\Infrastructure\Infrastructure.csproj --startup-project Verticalslice\Api\Api.csproj
```

### Run the Backend API

```powershell
dotnet run --project Verticalslice\Api\Api.csproj --launch-profile http
```

Check the actual listening URL in the API startup output.

### Scalar API Documentation

When the API is running, open the corresponding local URL:

```text
/scalar/v1
/openapi/v1.json
```

## API Endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/registrations` | Create a registration |
| GET | `/api/registrations` | List registrations with search, filters and pagination |
| GET | `/api/registrations/{id:guid}` | Get registration by ID |
| GET | `/api/registrations/summary` | Get registration and payment KPI summary |

Supported list query parameters include:

- `participantName`
- `type`
- `status`
- `page`
- `pageSize`

## Frontend Setup

Open a separate terminal at the repository root:

```powershell
cd frontend
npm install
npm run dev
```

Open the frontend development URL shown by Vite, normally:

```text
http://localhost:5173
```

### Frontend Features

- Registration KPI cards
- Registration search
- Registration type and status filters
- Paginated registration table
- Registration and payment status badges
- Loading, empty and error states
- API integration using a typed REST client

The frontend reads its API configuration from:

```text
frontend/.env.development
```

Configure `VITE_API_BASE_URL` to match the actual backend API address.

## Dashboard Behaviour

- KPI cards retrieve data from the registration summary endpoint.
- Search is debounced.
- Type and status filters are sent to the API.
- Filter changes return pagination to page 1.
- Loading, empty and error states are displayed.
- Pagination supports a page-size selector.

## GitHub Repository

https://github.com/sairasu-zero/Darwin_Acdal-Cyrus_Deramon-carShow
