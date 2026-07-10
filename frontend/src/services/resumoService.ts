import { httpClient } from './api';
import type { ResumoGeral } from '../types/resumo';

export const resumoService = {
  obter: () => httpClient.get<ResumoGeral>('/total'),
};