import { useState } from 'react';
import { useFetch } from './useFetch';
import { transacaoService } from '../services/transacaoService';
import type { CriarTransacaoRequest } from '../types/transacao';

export function useTransacoes() {
  const { data: transacoes, loading, error, refetch } = useFetch(() => transacaoService.listar(), []);
  const [salvando, setSalvando] = useState(false);
  const [erroSalvar, setErroSalvar] = useState<string | null>(null);

  const criarTransacao = async (request: CriarTransacaoRequest) => {
    setSalvando(true);
    setErroSalvar(null);

    try {
      await transacaoService.criar(request);
      refetch();
      return true;
    } catch (err) {
      setErroSalvar((err as Error).message);
      return false;
    } finally {
      setSalvando(false);
    }
  };

  return { transacoes: transacoes ?? [], loading, error, salvando, erroSalvar, criarTransacao };
}