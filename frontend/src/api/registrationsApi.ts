import type {
  PagedRegistrations,
  ProblemDetails,
  RegistrationQuery,
  RegistrationSummary,
} from '../types';

export const apiBaseUrl = (import.meta.env.VITE_API_BASE_URL || 'http://localhost:5205').replace(/\/+$/, '');

async function readJson<T>(response: Response): Promise<T | null> {
  try {
    return (await response.json()) as T;
  } catch {
    return null;
  }
}

async function request<T>(path: string, signal?: AbortSignal): Promise<T> {
  let response: Response;

  try {
    response = await fetch(`${apiBaseUrl}${path}`, { signal, headers: { Accept: 'application/json' } });
  } catch (error) {
    if (error instanceof DOMException && error.name === 'AbortError') throw error;
    throw new Error(`Cannot reach the API at ${apiBaseUrl}. Start the backend and try again.`);
  }

  const body = await readJson<ProblemDetails>(response);

  if (!response.ok) {
    const detail = body?.detail || body?.title || `The API answered with status ${response.status}.`;
    throw new Error(detail);
  }

  return body as T;
}

function buildQuery(filters: RegistrationQuery): string {
  const params = new URLSearchParams();
  params.set('page', String(filters.page));
  params.set('pageSize', String(filters.pageSize));
  if (filters.participantName.trim()) params.set('participantName', filters.participantName.trim());
  if (filters.type) params.set('type', filters.type);
  if (filters.status) params.set('status', filters.status);
  return params.toString();
}

export function getRegistrations(filters: RegistrationQuery, signal?: AbortSignal): Promise<PagedRegistrations> {
  return request<PagedRegistrations>(`/api/registrations?${buildQuery(filters)}`, signal);
}

export function getSummary(signal?: AbortSignal): Promise<RegistrationSummary> {
  return request<RegistrationSummary>('/api/registrations/summary', signal);
}
