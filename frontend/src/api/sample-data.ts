import type { PagedRegistrations, Registration, RegistrationQuery, RegistrationSummary } from '../types';

const wait = (milliseconds: number) => new Promise((resolve) => setTimeout(resolve, milliseconds));

const registrations: Registration[] = [
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0001', registrationNumber: 'CS27-0001', registrationDate: '2026-10-05T08:15:00Z', participantName: 'Juan Dela Cruz', contactNumber: '09171234567', email: 'juan.delacruz@example.com', address: '123 Rizal St., Naga City', carMakeModel: 'Toyota Supra MK4', plateNumber: 'ABC 1234', registrationType: 'Competitor', status: 'Pending', registrationFee: 2500, amountPaid: 1000, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0002', registrationNumber: 'CS27-0002', registrationDate: '2026-10-05T17:15:00Z', participantName: 'Ana Reyes', contactNumber: '09182223344', email: 'ana.reyes@example.com', address: '45 Burgos Ave., Legazpi City', carMakeModel: 'Honda Civic Type R', plateNumber: 'NCR 4821', registrationType: 'Exhibitor', status: 'Confirmed', registrationFee: 1500, amountPaid: 1500, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0003', registrationNumber: 'CS27-0003', registrationDate: '2026-10-06T02:15:00Z', participantName: 'Mark Villanueva', contactNumber: '09195556677', email: 'mark.villanueva@example.com', address: '8 Peñafrancia Ave., Naga City', carMakeModel: 'Nissan Skyline R34', plateNumber: 'DVO 7712', registrationType: 'VIP', status: 'CheckedIn', registrationFee: 5000, amountPaid: 5000, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0004', registrationNumber: 'CS27-0004', registrationDate: '2026-10-06T11:15:00Z', participantName: 'Grace Domingo', contactNumber: '09173334455', email: 'grace.domingo@example.com', address: '17 Magsaysay St., Sorsogon City', carMakeModel: 'Mazda MX-5', plateNumber: 'SOR 1188', registrationType: 'Exhibitor', status: 'Cancelled', registrationFee: 1500, amountPaid: 0, paymentStatus: 'Unpaid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0005', registrationNumber: 'CS27-0005', registrationDate: '2026-10-06T20:15:00Z', participantName: 'Rico Salazar', contactNumber: '09187778899', email: 'rico.salazar@example.com', address: '22 Bonifacio St., Albay', carMakeModel: 'Mitsubishi Lancer Evo IX', plateNumber: 'ALB 5521', registrationType: 'VIP', status: 'Pending', registrationFee: 5000, amountPaid: 2500, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0006', registrationNumber: 'CS27-0006', registrationDate: '2026-10-07T05:15:00Z', participantName: 'Liza Fernandez', contactNumber: '09198889911', email: 'liza.fernandez@example.com', address: '91 Quezon Blvd., Sorsogon City', carMakeModel: 'Ford Mustang GT', plateNumber: 'SOR 2244', registrationType: 'Exhibitor', status: 'Confirmed', registrationFee: 1500, amountPaid: 1500, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0007', registrationNumber: 'CS27-0007', registrationDate: '2026-10-07T14:15:00Z', participantName: 'Paolo Mendoza', contactNumber: '09171112233', email: 'paolo.mendoza@example.com', address: '3 Luna St., Bulan', carMakeModel: 'Subaru WRX STI', plateNumber: 'SOR 3399', registrationType: 'Competitor', status: 'CheckedIn', registrationFee: 2500, amountPaid: 800, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0008', registrationNumber: 'CS27-0008', registrationDate: '2026-10-07T23:15:00Z', participantName: 'Carla Bautista', contactNumber: '09174445566', email: 'carla.bautista@example.com', address: '56 Mabini St., Matnog', carMakeModel: 'Chevrolet Camaro SS', plateNumber: 'SOR 4410', registrationType: 'VIP', status: 'Cancelled', registrationFee: 5000, amountPaid: 5000, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0009', registrationNumber: 'CS27-0009', registrationDate: '2026-10-08T08:15:00Z', participantName: 'Dennis Ocampo', contactNumber: '09176667788', email: 'dennis.ocampo@example.com', address: '12 Del Pilar St., Irosin', carMakeModel: 'Toyota Hilux Conquest', plateNumber: 'SOR 5512', registrationType: 'Competitor', status: 'Pending', registrationFee: 2500, amountPaid: 0, paymentStatus: 'Unpaid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0010', registrationNumber: 'CS27-0010', registrationDate: '2026-10-08T17:15:00Z', participantName: 'Sheila Navarro', contactNumber: '09179990011', email: 'sheila.navarro@example.com', address: '78 Roxas St., Casiguran', carMakeModel: 'Hyundai Elantra N', plateNumber: 'SOR 6631', registrationType: 'VIP', status: 'Confirmed', registrationFee: 5000, amountPaid: 5000, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0011', registrationNumber: 'CS27-0011', registrationDate: '2026-10-09T02:15:00Z', participantName: 'Andres Lim', contactNumber: '09172223311', email: 'andres.lim@example.com', address: '5 Zamora St., Gubat', carMakeModel: 'BMW M3 Competition', plateNumber: 'SOR 7745', registrationType: 'Exhibitor', status: 'CheckedIn', registrationFee: 1500, amountPaid: 1500, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0012', registrationNumber: 'CS27-0012', registrationDate: '2026-10-09T11:15:00Z', participantName: 'Bianca Torres', contactNumber: '09183334422', email: 'bianca.torres@example.com', address: '34 Aguinaldo St., Prieto Diaz', carMakeModel: 'Audi RS5', plateNumber: 'SOR 8850', registrationType: 'Exhibitor', status: 'Pending', registrationFee: 1500, amountPaid: 500, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0013', registrationNumber: 'CS27-0013', registrationDate: '2026-10-09T20:15:00Z', participantName: 'Enrico Padilla', contactNumber: '09194445533', email: 'enrico.padilla@example.com', address: '67 Rizal St., Barcelona', carMakeModel: 'Mercedes-AMG C63', plateNumber: 'SOR 9962', registrationType: 'Competitor', status: 'Confirmed', registrationFee: 2500, amountPaid: 2500, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0014', registrationNumber: 'CS27-0014', registrationDate: '2026-10-10T05:15:00Z', participantName: 'Trisha Gomez', contactNumber: '09175556644', email: 'trisha.gomez@example.com', address: '19 Mabini St., Castilla', carMakeModel: 'Porsche 911 Carrera', plateNumber: 'SOR 1074', registrationType: 'VIP', status: 'CheckedIn', registrationFee: 5000, amountPaid: 3000, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0015', registrationNumber: 'CS27-0015', registrationDate: '2026-10-10T14:15:00Z', participantName: 'Ferdinand Cruz', contactNumber: '09186667755', email: 'ferdinand.cruz@example.com', address: '88 Bonifacio St., Magallanes', carMakeModel: 'Toyota Corolla Altis', plateNumber: 'SOR 2186', registrationType: 'Competitor', status: 'Cancelled', registrationFee: 2500, amountPaid: 1000, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0016', registrationNumber: 'CS27-0016', registrationDate: '2026-10-10T23:15:00Z', participantName: 'Kristine Aquino', contactNumber: '09197778866', email: 'kristine.aquino@example.com', address: '27 Luna St., Donsol', carMakeModel: 'Kia Stinger GT', plateNumber: 'SOR 3297', registrationType: 'VIP', status: 'Pending', registrationFee: 5000, amountPaid: 0, paymentStatus: 'Unpaid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0017', registrationNumber: 'CS27-0017', registrationDate: '2026-10-11T08:15:00Z', participantName: 'Miguel Rosales', contactNumber: '09178889977', email: 'miguel.rosales@example.com', address: '41 Del Pilar St., Pilar', carMakeModel: 'Honda Integra Type S', plateNumber: 'SOR 4318', registrationType: 'Exhibitor', status: 'Confirmed', registrationFee: 1500, amountPaid: 750, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0018', registrationNumber: 'CS27-0018', registrationDate: '2026-10-11T17:15:00Z', participantName: 'Jasmine Beltran', contactNumber: '09189990088', email: 'jasmine.beltran@example.com', address: '63 Rizal St., Juban', carMakeModel: 'Mazda RX-7 FD', plateNumber: 'SOR 5429', registrationType: 'Competitor', status: 'CheckedIn', registrationFee: 2500, amountPaid: 2500, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0019', registrationNumber: 'CS27-0019', registrationDate: '2026-10-12T02:15:00Z', participantName: 'Rafael Sarmiento', contactNumber: '09171113355', email: 'rafael.sarmiento@example.com', address: '14 Mabini St., Bulusan', carMakeModel: 'Nissan 350Z', plateNumber: 'SOR 6530', registrationType: 'Competitor', status: 'Pending', registrationFee: 2500, amountPaid: 1200, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0020', registrationNumber: 'CS27-0020', registrationDate: '2026-10-12T11:15:00Z', participantName: 'Camille Dizon', contactNumber: '09182224466', email: 'camille.dizon@example.com', address: '52 Burgos St., Sta. Magdalena', carMakeModel: 'Mini Cooper JCW', plateNumber: 'SOR 7641', registrationType: 'VIP', status: 'Confirmed', registrationFee: 5000, amountPaid: 2500, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0021', registrationNumber: 'CS27-0021', registrationDate: '2026-10-12T20:15:00Z', participantName: 'Victor Manalo', contactNumber: '09193335577', email: 'victor.manalo@example.com', address: '9 Quezon St., Bulan', carMakeModel: 'Toyota 86', plateNumber: 'SOR 8752', registrationType: 'Exhibitor', status: 'CheckedIn', registrationFee: 1500, amountPaid: 0, paymentStatus: 'Unpaid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0022', registrationNumber: 'CS27-0022', registrationDate: '2026-10-13T05:15:00Z', participantName: 'Nadine Perez', contactNumber: '09174446688', email: 'nadine.perez@example.com', address: '36 Luna St., Sorsogon City', carMakeModel: 'Mitsubishi Eclipse GSX', plateNumber: 'SOR 9863', registrationType: 'Exhibitor', status: 'Pending', registrationFee: 1500, amountPaid: 1500, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0023', registrationNumber: 'CS27-0023', registrationDate: '2026-10-13T14:15:00Z', participantName: 'Gabriel Soriano', contactNumber: '09185557799', email: 'gabriel.soriano@example.com', address: '71 Mabini St., Naga City', carMakeModel: 'Subaru BRZ', plateNumber: 'SOR 1974', registrationType: 'Competitor', status: 'Confirmed', registrationFee: 2500, amountPaid: 500, paymentStatus: 'Partial' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0024', registrationNumber: 'CS27-0024', registrationDate: '2026-10-13T23:15:00Z', participantName: 'Isabel Marquez', contactNumber: '09196668800', email: 'isabel.marquez@example.com', address: '25 Del Pilar St., Legazpi City', carMakeModel: 'Lexus RC F', plateNumber: 'SOR 2085', registrationType: 'VIP', status: 'CheckedIn', registrationFee: 5000, amountPaid: 5000, paymentStatus: 'Paid' },
  { id: 'a1f0c9d2-0f5b-4a11-8f1c-9c1b6a0d0025', registrationNumber: 'CS27-0025', registrationDate: '2026-10-14T08:15:00Z', participantName: 'Ramon Estrada', contactNumber: '09177779911', email: 'ramon.estrada@example.com', address: '48 Rizal St., Sorsogon City', carMakeModel: 'Toyota Supra A80', plateNumber: 'SOR 3196', registrationType: 'Competitor', status: 'Pending', registrationFee: 2500, amountPaid: 2500, paymentStatus: 'Paid' },
];

function matches(registration: Registration, filters: RegistrationQuery): boolean {
  const search = filters.participantName.trim().toLowerCase();
  if (search && !registration.participantName.toLowerCase().includes(search)) return false;
  if (filters.type && registration.registrationType !== filters.type) return false;
  if (filters.status && registration.status !== filters.status) return false;
  return true;
}

export async function getRegistrations(
  filters: RegistrationQuery,
  signal?: AbortSignal,
): Promise<PagedRegistrations> {
  await wait(450);
  if (signal?.aborted) throw new DOMException('Aborted', 'AbortError');

  const filtered = registrations
    .filter((registration) => matches(registration, filters))
    .sort((left, right) => new Date(right.registrationDate).getTime() - new Date(left.registrationDate).getTime());

  const start = (filters.page - 1) * filters.pageSize;
  const items = filtered.slice(start, start + filters.pageSize);

  return {
    items,
    page: filters.page,
    pageSize: filters.pageSize,
    totalCount: filtered.length,
    totalPages: filtered.length === 0 ? 0 : Math.ceil(filtered.length / filters.pageSize),
  };
}

export async function getSummary(signal?: AbortSignal): Promise<RegistrationSummary> {
  await wait(300);
  if (signal?.aborted) throw new DOMException('Aborted', 'AbortError');

  const billable = registrations.filter((registration) => registration.status !== 'Cancelled');
  const totalFees = billable.reduce((sum, registration) => sum + registration.registrationFee, 0);
  const totalPayments = billable.reduce((sum, registration) => sum + registration.amountPaid, 0);

  return {
    totalRegistrations: registrations.length,
    confirmed: registrations.filter((registration) => registration.status === 'Confirmed').length,
    checkedIn: registrations.filter((registration) => registration.status === 'CheckedIn').length,
    pending: registrations.filter((registration) => registration.status === 'Pending').length,
    cancelled: registrations.filter((registration) => registration.status === 'Cancelled').length,
    totalFees,
    totalPayments,
    uncollectedPayments: totalFees - totalPayments,
  };
}
