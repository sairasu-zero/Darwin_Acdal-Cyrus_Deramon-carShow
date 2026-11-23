import type { Registration } from '../types';
import { formatDate, formatPeso } from '../utils/format';
import { PaymentBadge } from './PaymentBadge';
import { StatusBadge } from './StatusBadge';

const headers = [
  'Registration No.',
  'Date',
  'Participant',
  'Car',
  'Type',
  'Status',
  'Fee',
  'Paid',
  'Payment',
];

interface RegistrationTableProps {
  items: Registration[];
  loading: boolean;
}

export function RegistrationTable({ items, loading }: RegistrationTableProps) {
  return (
    <div className="overflow-x-auto rounded-xl border border-beige-300 bg-[#fffdf8] shadow-sm">
      <table className="min-w-[64rem] w-full border-collapse text-left text-sm">
        <thead>
          <tr className="border-b border-beige-300 bg-beige-100/70 text-xs uppercase tracking-wide text-beige-700">
            {headers.map((header, index) => (
              <th
                key={header}
                scope="col"
                className={`whitespace-nowrap px-4 py-3 font-semibold ${index >= 6 && index <= 7 ? 'text-right' : ''}`}
              >
                {header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className={loading ? 'opacity-50 transition-opacity' : 'transition-opacity'}>
          {items.map((registration) => (
            <tr key={registration.id} className="border-b border-beige-200 last:border-0 hover:bg-beige-50">
              <td className="whitespace-nowrap px-4 py-3 font-semibold text-beige-900">{registration.registrationNumber}</td>
              <td className="whitespace-nowrap px-4 py-3 text-beige-700">{formatDate(registration.registrationDate)}</td>
              <td className="px-4 py-3">
                <p className="font-medium text-beige-900">{registration.participantName}</p>
                <p className="text-xs text-beige-500">{registration.contactNumber}</p>
                <p className="text-xs text-beige-500">{registration.email}</p>
              </td>
              <td className="px-4 py-3">
                <p className="text-beige-900">{registration.carMakeModel}</p>
                <p className="text-xs uppercase tracking-wide text-beige-500">{registration.plateNumber}</p>
              </td>
              <td className="whitespace-nowrap px-4 py-3 text-beige-700">{registration.registrationType}</td>
              <td className="px-4 py-3">
                <StatusBadge status={registration.status} />
              </td>
              <td className="whitespace-nowrap px-4 py-3 text-right tabular-nums text-beige-800">
                {formatPeso(registration.registrationFee)}
              </td>
              <td className="whitespace-nowrap px-4 py-3 text-right tabular-nums text-beige-800">
                {formatPeso(registration.amountPaid)}
              </td>
              <td className="px-4 py-3">
                <PaymentBadge status={registration.paymentStatus} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export function RegistrationTableSkeleton() {
  return (
    <div className="overflow-hidden rounded-xl border border-beige-300 bg-[#fffdf8] shadow-sm">
      <div className="h-11 border-b border-beige-300 bg-beige-100/70" />
      {Array.from({ length: 6 }).map((_, index) => (
        <div key={index} className="flex animate-pulse gap-6 border-b border-beige-200 px-4 py-4 last:border-0">
          <div className="h-4 w-20 rounded bg-beige-200" />
          <div className="h-4 w-32 rounded bg-beige-200" />
          <div className="h-4 w-40 rounded bg-beige-200" />
          <div className="h-4 w-32 rounded bg-beige-100" />
          <div className="h-4 w-20 rounded bg-beige-100" />
        </div>
      ))}
    </div>
  );
}
