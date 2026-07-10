import { httpClient } from './api';
import type { Pessoa, CriarPessoaRequest } from '../types/pessoa';
import type { Transacao } from '../types/transacao';

export const pessoaService = {
  listar: () => httpClient.get<Pessoa[]>('/pessoa'),
  obterPorId: (id: string) => httpClient.get<Pessoa>(`/pessoa/${id}`),
  criar: (request: CriarPessoaRequest) => httpClient.post<Pessoa>('/pessoa', request),
  remover: (id: string) => httpClient.delete<void>(`/pessoa/${id}`),
  listarTransacoes: (id: string) => httpClient.get<Transacao[]>(`/pessoa/${id}/transacoes`),
};