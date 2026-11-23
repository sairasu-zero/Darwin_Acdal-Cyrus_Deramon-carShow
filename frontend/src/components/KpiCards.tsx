import type { RegistrationSummary } from '../types';
import { formatCount, formatPercent, formatPeso } from '../utils/format';
import { KpiCard, KpiCardSkeleton } from './KpiCard';

interface KpiCardsProps {
  summary: RegistrationSummary | null;
  loading: boolean;
}

export function KpiCards({ summary, loading }: KpiCardsProps) {
  if (loading && !summary) {
    return (
      <section className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-6">
        {Array.from({ length: 6 }).map((_, index) => (
          <KpiCardSkeleton key={index} />
        ))}
      </section>
    );
  }

  if (!summary) return null;

  return (
    <section className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-6">
      <KpiCard
        label="Total Registrations"
        value={formatCount(summary.totalRegistrations)}
        caption={`${summary.pending} pending`}
        emphasis
      />
      <KpiCard label="Confirmed" value={formatCount(summary.confirmed)} caption="Ready for the hall" />
      <KpiCard label="Checked In" value={formatCount(summary.checkedIn)} caption="Already on site" />
      <KpiCard label="Total Payments" value={formatPeso(summary.totalPayments)} caption="Collected so far" />
      <KpiCard label="Uncollected" value={formatPeso(summary.uncollectedPayments)} caption="Still to collect" />
      <KpiCard
        label="Collection Rate"
        value={formatPercent(summary.totalPayments, summary.totalFees)}
        caption={`of ${formatPeso(summary.totalFees)} in fees`}
      />
    </section>
  );
}
