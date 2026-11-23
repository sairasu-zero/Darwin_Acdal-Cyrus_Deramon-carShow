const pageSizes = [5, 10, 20];

function pageNumbers(current: number, total: number): number[] {
  if (total <= 7) return Array.from({ length: total }, (_, index) => index + 1);

  const numbers = new Set<number>([1, total, current, current - 1, current + 1]);
  if (current <= 3) [2, 3, 4].forEach((page) => numbers.add(page));
  if (current >= total - 2) [total - 3, total - 2, total - 1].forEach((page) => numbers.add(page));

  return [...numbers].filter((page) => page >= 1 && page <= total).sort((left, right) => left - right);
}

interface PaginationProps {
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  loading: boolean;
  onPageChange: (page: number) => void;
  onPageSizeChange: (pageSize: number) => void;
}

export function Pagination({
  page,
  pageSize,
  totalCount,
  totalPages,
  loading,
  onPageChange,
  onPageSizeChange,
}: PaginationProps) {
  const firstRow = totalCount === 0 ? 0 : (page - 1) * pageSize + 1;
  const lastRow = Math.min(page * pageSize, totalCount);

  return (
    <div className="flex flex-col gap-3 rounded-xl border border-beige-300 bg-[#fffdf8] px-4 py-3 shadow-sm sm:flex-row sm:items-center sm:justify-between">
      <div className="flex flex-wrap items-center gap-3 text-sm text-beige-700">
        <span className="font-medium text-beige-800">
          Showing {firstRow}–{lastRow} of {totalCount}
        </span>
        <label className="flex items-center gap-2 text-xs text-beige-600">
          Rows per page
          <select
            value={pageSize}
            onChange={(event) => onPageSizeChange(Number(event.target.value))}
            disabled={loading}
            className="rounded-md border border-beige-300 bg-[#fffdf8] px-2 py-1 text-sm text-beige-900 disabled:opacity-60"
          >
            {pageSizes.map((size) => (
              <option key={size} value={size}>
                {size}
              </option>
            ))}
          </select>
        </label>
      </div>

      <nav aria-label="Pagination" className="flex flex-wrap items-center gap-1">
        <button
          type="button"
          onClick={() => onPageChange(page - 1)}
          disabled={loading || page <= 1}
          className="rounded-md border border-beige-300 px-3 py-1.5 text-sm text-beige-800 transition hover:bg-beige-100 disabled:cursor-not-allowed disabled:border-beige-200 disabled:text-beige-300 disabled:hover:bg-transparent"
        >
          Prev
        </button>

        {pageNumbers(page, totalPages).map((number, index, list) => {
          const previous = list[index - 1];
          const gap = previous && number - previous > 1;

          return (
            <span key={number} className="flex items-center gap-1">
              {gap ? <span className="px-1 text-beige-400">…</span> : null}
              <button
                type="button"
                onClick={() => onPageChange(number)}
                disabled={loading}
                aria-current={number === page ? 'page' : undefined}
                className={`min-w-[2.25rem] rounded-md border px-2.5 py-1.5 text-sm transition disabled:cursor-not-allowed ${
                  number === page
                    ? 'border-beige-700 bg-beige-700 font-semibold text-beige-50'
                    : 'border-beige-300 text-beige-800 hover:bg-beige-100'
                }`}
              >
                {number}
              </button>
            </span>
          );
        })}

        <button
          type="button"
          onClick={() => onPageChange(page + 1)}
          disabled={loading || totalPages === 0 || page >= totalPages}
          className="rounded-md border border-beige-300 px-3 py-1.5 text-sm text-beige-800 transition hover:bg-beige-100 disabled:cursor-not-allowed disabled:border-beige-200 disabled:text-beige-300 disabled:hover:bg-transparent"
        >
          Next
        </button>
      </nav>
    </div>
  );
}
