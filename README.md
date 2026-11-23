# Car Show Registration System — Frontend

React + TypeScript dashboard for **Car Show 2027** (AutoVista Events, Sorsogon City Coliseum). Event staff use it to
watch the KPI cards, search registrations, filter by type and status, and page through the master list.
All data comes from the C# Vertical Slice API.

Course: Integrative Programming 2 / API Backend Development
School: Computer Communication Development Institute — Sorsogon Campus

## Team

| Member | Role |
| --- | --- |
| Cyrus Deramon | Backend Developer |
| Darwin Acdal | Frontend Developer |

## Repository layout

```
CarShow_Frontend_Acdal/
├── frontend/                 React + TypeScript app (Vite, Tailwind, ESLint)
│   ├── src/
│   │   ├── api/              typed REST client and sample data
│   │   ├── components/       KPI cards, filters, table, pagination, badges, states
│   │   ├── hooks/            debounce and dashboard data hooks
│   │   ├── pages/            RegistrationsPage layout
│   │   ├── types.ts          API contract types
│   └── utils/            currency, date and percentage formatting
│   └── .env.development      API base URL and data source switch
├── docs/                     documentation screenshots and reflection PDF
└── README.md
```

## How to run

1. Start the backend API (it listens on `http://localhost:5205`).
2. In `frontend/`:

```
npm install
npm run dev
```

3. Open `http://localhost:5173`.

## Dashboard behaviour

- KPI cards read `GET /api/registrations/summary`; they never add up the rows on screen.
- Search is debounced at 400 ms and sent to the API as `participantName`; type and status are sent as
  `type` and `status`. Any filter change returns to page 1.
- Paging sends `page` and `pageSize`, shows the `Showing x–y of N` summary and disables buttons while
  loading or at the first/last page.
- Loading, empty and error states are always visible; the error state has a Try again button.
- The base URL is read from `VITE_API_BASE_URL` in `frontend/.env.development`.
