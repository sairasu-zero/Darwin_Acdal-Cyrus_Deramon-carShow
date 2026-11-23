import type { PaymentStatus } from '../types';

const styles: Record<PaymentStatus, string> = {
  Unpaid: 'bg-[#f7dcd6] text-[#9c2b16] ring-[#e3b3a8]',
  Partial: 'bg-[#fbeacb] text-[#7a5210] ring-[#e7cd93]',
  Paid: 'bg-[#dcecdb] text-[#2f6b3a] ring-[#b9d8b6]',
};

interface PaymentBadgeProps {
  status: PaymentStatus;
}

export function PaymentBadge({ status }: PaymentBadgeProps) {
  return (
    <span className={`inline-flex items-center rounded-full px-2.5 py-1 text-xs font-medium ring-1 ring-inset ${styles[status]}`}>
      {status}
    </span>
  );
}
