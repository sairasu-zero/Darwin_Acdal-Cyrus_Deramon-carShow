interface KpiCardProps {
  label: string;
  value: string;
  caption?: string;
  emphasis?: boolean;
}

export function KpiCard({ label, value, caption, emphasis = false }: KpiCardProps) {
  return (
    <article
      className={`rounded-xl border p-4 shadow-sm ${
        emphasis ? 'border-beige-500 bg-beige-800 text-beige-50' : 'border-beige-300 bg-[#fffdf8] text-beige-900'
      }`}
    >
      <p className={`text-xs font-medium uppercase tracking-wide ${emphasis ? 'text-beige-200' : 'text-beige-600'}`}>
        {label}
      </p>
      <p className="mt-2 text-2xl font-semibold leading-tight tabular-nums">{value}</p>
      {caption ? (
        <p className={`mt-1 text-xs ${emphasis ? 'text-beige-300' : 'text-beige-500'}`}>{caption}</p>
      ) : null}
    </article>
  );
}

export function KpiCardSkeleton() {
  return (
    <div className="animate-pulse rounded-xl border border-beige-300 bg-[#fffdf8] p-4 shadow-sm">
      <div className="h-3 w-24 rounded bg-beige-200" />
      <div className="mt-3 h-7 w-20 rounded bg-beige-200" />
      <div className="mt-2 h-3 w-16 rounded bg-beige-100" />
    </div>
  );
}
