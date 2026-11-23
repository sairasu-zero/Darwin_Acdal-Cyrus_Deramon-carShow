import { useCallback, useEffect, useState } from 'react';
import { getRegistrations, getSummary } from '../api';
import type { PagedRegistrations, RegistrationQuery, RegistrationSummary } from '../types';

interface DashboardState {
  registrations: PagedRegistrations | null;
  summary: RegistrationSummary | null;
  loading: boolean;
  error: string | null;
}

const emptyState: DashboardState = {
  registrations: null,
  summary: null,
  loading: true,
  error: null,
};

export function useDashboardData(filters: RegistrationQuery): DashboardState & { retry: () => void } {
  const [state, setState] = useState<DashboardState>(emptyState);
  const [attempt, setAttempt] = useState(0);

  const { participantName, type, status, page, pageSize } = filters;

  useEffect(() => {
    const controller = new AbortController();
    let active = true;

    setState((current) => ({ ...current, loading: true, error: null }));

    Promise.all([
      getRegistrations({ participantName, type, status, page, pageSize }, controller.signal),
      getSummary(controller.signal),
    ])
      .then(([registrations, summary]) => {
        if (!active) return;
        setState({ registrations, summary, loading: false, error: null });
      })
      .catch((error: unknown) => {
        if (!active) return;
        if (error instanceof DOMException && error.name === 'AbortError') return;
        const message = error instanceof Error ? error.message : 'The API request failed.';
        setState({ registrations: null, summary: null, loading: false, error: message });
      });

    return () => {
      active = false;
      controller.abort();
    };
  }, [participantName, type, status, page, pageSize, attempt]);

  const retry = useCallback(() => setAttempt((value) => value + 1), []);

  return { ...state, retry };
}
