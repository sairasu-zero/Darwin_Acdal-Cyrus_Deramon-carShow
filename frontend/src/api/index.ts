import * as liveApi from './registrationsApi';
import * as sampleApi from './sample-data';

export const dataSource: 'api' | 'sample' = import.meta.env.VITE_DATA_SOURCE === 'sample' ? 'sample' : 'api';

const source = dataSource === 'sample' ? sampleApi : liveApi;

export const getRegistrations = source.getRegistrations;
export const getSummary = source.getSummary;
