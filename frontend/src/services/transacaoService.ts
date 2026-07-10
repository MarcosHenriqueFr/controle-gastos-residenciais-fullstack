import { httpClient } from './api';
import type { Transacao, CriarTransacaoRequest } from '../types/transacao';

export const transacaoService = {
  listar: () => httpClient.get<Transacao[]>('/transacao'),
  obterPorId: (id: string) => httpClient.get<Transacao>(`/transacao/${id}`),
  criar: (request: CriarTransacaoRequest) => httpClient.post<Transacao>('/transacao', request),
};