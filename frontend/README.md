# Car Show 2027 — Registration Dashboard (React + TypeScript)

Vite + React + TypeScript + Tailwind CSS dashboard for the Car Show Registration System, checked with ESLint.

## Scripts

| Command | Purpose |
| --- | --- |
| `npm install` | Install dependencies |
| `npm run dev` | Start the dev server on http://localhost:5173 |
| `npm run build` | Type-check with `tsc -b` and build into `dist/` |
| `npm run typecheck` | Type-check the app without emitting files |
| `npm run lint` | Run ESLint over the whole project |
| `npm run preview` | Preview the production build |

## Environment

`frontend/.env.development`:

```
VITE_API_BASE_URL=http://localhost:5205
VITE_DATA_SOURCE=api
```

`VITE_API_BASE_URL` is the backend address used by every fetch call.

## Switching between sample data and the real API

The dashboard reads through `src/api/index.ts`, which exports `getRegistrations` and `getSummary`.
Both implementations return the same typed shapes, so no component changes are needed.

- **Real API:** set `VITE_DATA_SOURCE=api` (default) in `.env.development`. Requests go to
  `src/api/registrationsApi.ts` and reach `http://localhost:5205/api/registrations`.
- **Sample data:** set `VITE_DATA_SOURCE=sample`, then restart `npm run dev`. Requests go to
  `src/api/sample-data.ts`, which holds 25 registrations and filters, pages and totals them in the browser.

The header shows a `Live API` or `Sample data` pill so the current source is always visible.

## Folder map

```
src/
├── api/
│   ├── index.ts              picks the data source
│   ├── registrationsApi.ts   fetch calls to the C# API
│   └── sample-data.ts        25 local registrations
├── components/
│   ├── KpiCard.tsx           one card and its skeleton
│   ├── KpiCards.tsx          the row of six cards
│   ├── RegistrationFilters.tsx
│   ├── RegistrationTable.tsx table and skeleton rows
│   ├── Pagination.tsx
│   ├── States.tsx            loading, empty and error panels
│   ├── StatusBadge.tsx
│   └── PaymentBadge.tsx
├── hooks/
│   ├── useDashboardData.ts
│   └── useDebouncedValue.ts
├── pages/
│   └── RegistrationsPage.tsx
├── utils/format.ts
├── types.ts                  API contract types
├── App.tsx
├── index.css
├── main.tsx
└── vite-env.d.ts
```

## Tooling

- TypeScript strict mode, `tsc -b` runs before every production build.
- ESLint flat config with `typescript-eslint`, `eslint-plugin-react-hooks` and `eslint-plugin-react-refresh`.
- The `Registration`, `PagedRegistrations` and `RegistrationSummary` types in `src/types.ts` mirror the API contract.

## Screens

- KPI cards: total registrations, confirmed, checked in, total payments, uncollected, collection rate.
- Filters: participant search, registration type, status, clear filters.
- Table: registration number, date, participant, car, type, status, fee, paid, payment badge.
- Pagination: prev/next, page numbers, page-size selector, `Showing x–y of N`.
- States: loading message with skeleton rows, empty state with clear filters, error state with try again.
