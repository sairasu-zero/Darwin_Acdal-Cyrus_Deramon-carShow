import { useEffect, useState } from 'react';
import { dataSource } from '../api';
import { KpiCards } from '../components/KpiCards';
import { Pagination } from '../components/Pagination';
import { RegistrationFilters } from '../components/RegistrationFilters';
import { RegistrationTable, RegistrationTableSkeleton } from '../components/RegistrationTable';
import { EmptyState, ErrorState, LoadingNotice } from '../components/States';
import { useDashboardData } from '../hooks/useDashboardData';
import { useDebouncedValue } from '../hooks/useDebouncedValue';

export function RegistrationsPage() {
  const [search, setSearch] = useState('');
  const [type, setType] = useState('');
  const [status, setStatus] = useState('');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const debouncedSearch = useDebouncedValue(search, 400);

  useEffect(() => {
    setPage(1);
  }, [debouncedSearch, type, status, pageSize]);

  const { registrations, summary, loading, error, retry } = useDashboardData({
    participantName: debouncedSearch,
    type,
    status,
    page,
    pageSize,
  });

  const items = registrations?.items ?? [];
  const totalCount = registrations?.totalCount ?? 0;
  const totalPages = registrations?.totalPages ?? 0;

  function clearFilters() {
    setSearch('');
    setType('');
    setStatus('');
    setPage(1);
  }

  const firstLoad = loading && !registrations && !error;

  return (
    <div className="min-h-screen bg-[#f4ecdd]">
      <header className="border-b border-beige-300 bg-[#fffaf0]">
        <div className="mx-auto flex max-w-7xl flex-col gap-2 px-5 py-5 sm:flex-row sm:items-end sm:justify-between">
          <div>
            <p className="text-xs font-semibold uppercase tracking-[0.18em] text-beige-500">AutoVista Events</p>
            <h1 className="text-2xl font-semibold text-beige-900">Car Show 2027 · Registration Desk</h1>
            <p className="mt-1 text-sm text-beige-600">
              Sorsogon City Coliseum · three-day exhibit, Show &amp; Shine and VIP passes
            </p>
          </div>
          <div className="flex items-center gap-3 text-xs text-beige-600">
            <span className="rounded-full border border-beige-300 bg-beige-100 px-3 py-1 font-medium text-beige-700">
              {dataSource === 'sample' ? 'Sample data' : 'Live API'}
            </span>
            <span>{new Date().toLocaleDateString('en-PH', { month: 'long', day: 'numeric', year: 'numeric' })}</span>
          </div>
        </div>
      </header>

      <main className="mx-auto max-w-7xl space-y-4 px-5 py-6">
        <KpiCards summary={summary} loading={firstLoad || loading} />

        <RegistrationFilters
          search={search}
          type={type}
          status={status}
          disabled={loading}
          onSearchChange={setSearch}
          onTypeChange={setType}
          onStatusChange={setStatus}
          onClear={clearFilters}
        />

        {error ? (
          <ErrorState message={error} onRetry={retry} />
        ) : (
          <>
            <div className="flex items-center justify-between gap-3">
              <h2 className="text-base font-semibold text-beige-900">Registrations</h2>
              {loading && !firstLoad ? <LoadingNotice /> : null}
            </div>

            {firstLoad ? (
              <div className="space-y-3">
                <LoadingNotice />
                <RegistrationTableSkeleton />
              </div>
            ) : items.length === 0 ? (
              <EmptyState onClear={clearFilters} />
            ) : (
              <RegistrationTable items={items} loading={loading} />
            )}

            {firstLoad ? null : (
              <Pagination
                page={registrations?.page ?? 1}
                pageSize={pageSize}
                totalCount={totalCount}
                totalPages={totalPages}
                loading={loading}
                onPageChange={setPage}
                onPageSizeChange={setPageSize}
              />
            )}
          </>
        )}
      </main>

      <footer className="mx-auto max-w-7xl px-5 pb-8 text-xs text-beige-500">
        Registration fees: Exhibitor ₱1,500.00 · Competitor ₱2,500.00 · VIP ₱5,000.00. Payment totals exclude cancelled
        registrations.
      </footer>
    </div>
  );
}
