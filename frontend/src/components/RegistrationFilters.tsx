interface Option {
  value: string;
  label: string;
}

const typeOptions: Option[] = [
  { value: '', label: 'All types' },
  { value: 'Exhibitor', label: 'Exhibitor' },
  { value: 'Competitor', label: 'Competitor' },
  { value: 'VIP', label: 'VIP' },
];

const statusOptions: Option[] = [
  { value: '', label: 'All statuses' },
  { value: 'Pending', label: 'Pending' },
  { value: 'Confirmed', label: 'Confirmed' },
  { value: 'CheckedIn', label: 'Checked In' },
  { value: 'Cancelled', label: 'Cancelled' },
];

const controlClass =
  'mt-1 w-full rounded-lg border border-beige-300 bg-[#fffdf8] px-3 py-2 text-sm text-beige-900 shadow-sm transition hover:border-beige-400 focus:border-beige-500 focus:outline-none';

interface RegistrationFiltersProps {
  search: string;
  type: string;
  status: string;
  disabled: boolean;
  onSearchChange: (value: string) => void;
  onTypeChange: (value: string) => void;
  onStatusChange: (value: string) => void;
  onClear: () => void;
}

export function RegistrationFilters({
  search,
  type,
  status,
  disabled,
  onSearchChange,
  onTypeChange,
  onStatusChange,
  onClear,
}: RegistrationFiltersProps) {
  const hasFilters = Boolean(search || type || status);

  return (
    <section className="rounded-xl border border-beige-300 bg-[#fffdf8] p-4 shadow-sm">
      <div className="grid gap-3 md:grid-cols-2 xl:grid-cols-[1.6fr_1fr_1fr_auto]">
        <label className="block text-sm font-medium text-beige-800">
          Search participant name
          <input
            type="search"
            value={search}
            placeholder="Search participant name..."
            onChange={(event) => onSearchChange(event.target.value)}
            className={controlClass}
          />
        </label>

        <label className="block text-sm font-medium text-beige-800">
          Registration type
          <select value={type} onChange={(event) => onTypeChange(event.target.value)} className={controlClass}>
            {typeOptions.map((option) => (
              <option key={option.value || 'all-types'} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </label>

        <label className="block text-sm font-medium text-beige-800">
          Status
          <select value={status} onChange={(event) => onStatusChange(event.target.value)} className={controlClass}>
            {statusOptions.map((option) => (
              <option key={option.value || 'all-statuses'} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </label>

        <div className="flex items-end">
          <button
            type="button"
            onClick={onClear}
            disabled={disabled || !hasFilters}
            className="w-full rounded-lg border border-beige-400 bg-beige-100 px-4 py-2 text-sm font-semibold text-beige-800 transition hover:bg-beige-200 disabled:cursor-not-allowed disabled:border-beige-200 disabled:bg-beige-50 disabled:text-beige-400 xl:w-auto"
          >
            Clear filters
          </button>
        </div>
      </div>
    </section>
  );
}
