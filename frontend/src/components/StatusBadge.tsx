import type { RegistrationStatus } from '../types';

const styles: Record<RegistrationStatus, string> = {
  Pending: 'bg-[#fbeacb] text-[#7a5210] ring-[#e7cd93]',
  Confirmed: 'bg-[#dcecdb] text-[#2f6b3a] ring-[#b9d8b6]',
  CheckedIn: 'bg-[#d9e7f3] text-[#1f4f7a] ring-[#b4cee6]',
  Cancelled: 'bg-[#f3dedb] text-[#8a3232] ring-[#e2bbb5]',
};

const labels: Record<RegistrationStatus, string> = {
  Pending: 'Pending',
  Confirmed: 'Confirmed',
  CheckedIn: 'Checked In',
  Cancelled: 'Cancelled',
};

interface StatusBadgeProps {
  status: RegistrationStatus;
}

export function StatusBadge({ status }: StatusBadgeProps) {
  return (
    <span className={`inline-flex items-center rounded-full px-2.5 py-1 text-xs font-medium ring-1 ring-inset ${styles[status]}`}>
      {labels[status]}
    </span>
  );
}
