interface EmptyStateProps {
  onClear: () => void;
}

interface ErrorStateProps {
  message: string;
  onRetry: () => void;
}

export function LoadingNotice() {
  return (
    <div className="flex items-center gap-3 rounded-xl border border-beige-300 bg-[#fffdf8] px-4 py-3 text-sm text-beige-700 shadow-sm">
      <span className="h-4 w-4 animate-spin rounded-full border-2 border-beige-300 border-t-beige-700" />
      Loading registrations…
    </div>
  );
}

export function EmptyState({ onClear }: EmptyStateProps) {
  return (
    <div className="rounded-xl border border-dashed border-beige-400 bg-[#fffdf8] px-6 py-14 text-center shadow-sm">
      <p className="text-lg font-semibold text-beige-900">No registrations match your filters</p>
      <p className="mt-1 text-sm text-beige-600">Try a different participant name, type or status.</p>
      <button
        type="button"
        onClick={onClear}
        className="mt-5 rounded-lg border border-beige-500 bg-beige-700 px-4 py-2 text-sm font-semibold text-beige-50 transition hover:bg-beige-800"
      >
        Clear filters
      </button>
    </div>
  );
}

export function ErrorState({ message, onRetry }: ErrorStateProps) {
  return (
    <div className="rounded-xl border border-[#e2bbb5] bg-[#fdf3f1] px-6 py-12 text-center shadow-sm">
      <p className="text-lg font-semibold text-[#8a3232]">Cannot reach the API</p>
      <p className="mx-auto mt-2 max-w-xl text-sm text-[#7c4a42]">{message}</p>
      <button
        type="button"
        onClick={onRetry}
        className="mt-5 rounded-lg border border-[#b4544c] bg-[#9c2b16] px-4 py-2 text-sm font-semibold text-white transition hover:bg-[#7f2212]"
      >
        Try again
      </button>
    </div>
  );
}
