import { useFetch } from './useFetch';
import { pessoaService } from '../services/pessoaService';

export function usePessoaDetalhe(id: string) {
  const pessoaFetch = useFetch(() => pessoaService.obterPorId(id), [id]);
  const transacoesFetch = useFetch(() => pessoaService.listarTransacoes(id), [id]);

  return {
    pessoa: pessoaFetch.data,
    transacoes: transacoesFetch.data ?? [],
    loading: pessoaFetch.loading || transacoesFetch.loading,
    error: pessoaFetch.error ?? transacoesFetch.error,
  };
}